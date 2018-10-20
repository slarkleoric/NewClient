﻿using ArcFace.Core.AppService;
using ArcFace.Core.Models.Entities;
using ArcFace.Core.Helper;
using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ArcFace.Core.Enums;
using ArcFace.Core.Dtos;
using ArcFace.Core.Models;

namespace ArcFace.Core.DbHelper
{
    public class AutoCreateTableHelper
    {
        private static readonly CacheHelper Cache = new CacheHelper("sql");

        /// <summary> 初始化数据库 </summary>
        public static void InitializeDb()
        {
            InitializeAdminDb();

            var adminService = new AdminDataService();
            var dbVersion = adminService.Query<int>(GlobalKeys.DbVersion);

            if (dbVersion < Const.DbVersion)
            {
                //活动数据库
                InitializeMeetingDb();
                adminService.InsertOrUpdate(GlobalKeys.DbVersion, Const.DbVersion);
            }
            //VersionUpdateSql();
        }

        #region 管理数据库
        /// <summary> 初始化全局配置表 </summary>
        public static void InitializeAdminDb()
        {
            //全局数据库
            var globalPath = Const.AdminDbName.DbPath();
            if (File.Exists(globalPath))
            {
                CheckAdminData();
                return;
            }
            var dir = Path.GetDirectoryName(globalPath);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var conn = new SQLiteConnection(string.Format(Const.ConnectionString, globalPath)))
            {
                conn.Open();
                if (!string.IsNullOrWhiteSpace(Const.AdminDbPassword))
                    conn.ChangePassword(Const.AdminDbPassword);
                conn.Execute(GlobalTables());
            }
            InitDataSql();
        }

        /// <summary>
        /// 创建admin数据库sql
        /// </summary>
        /// <returns></returns>
        public static string GlobalTables()
        {
            var sql = new StringBuilder();
            sql.AppendLine("create table \"global_data\" (\"key\" TEXT NOT NULL PRIMARY KEY,\"value\" TEXT NOT NULL);");
            sql.AppendLine(
                "CREATE TABLE \"synch_data\" (\"id\" TEXT NOT NULL PRIMARY KEY, \"table\" TEXT NOT NULL,\"state\" INT NOT NULL,\"data_id\" TEXT NOT NULL, \"data\" TEXT NULL,\"create_date\" DATETIME NOT NULL);");
            return sql.ToString();
        }
        
        /// <summary> 初始化数据 </summary>
        private static void InitDataSql()
        {
            var service = AdminDataService.Instance;
            //service.InsertOrUpdate(GlobalKeys.Host, ConfigHelper.Read<string>("api_host"));
            service.InsertOrUpdate(GlobalKeys.DataMode, DataMode.Online);
            service.InsertOrUpdate(GlobalKeys.MachineCode, MachineCodeHelper.Instance.MachineCode);
            service.InsertOrUpdate(GlobalKeys.ClientSecret, new ClientSecretDto { Id = "admin", Secret = "111" });
        }
        /// <summary>
        /// 检测admin数据库
        /// </summary>
        private static void CheckAdminData()
        {
            //var service = AdminDataService.Instance;
            //service.InsertOrUpdate(GlobalKeys.Menus, ConfigHelper.ReadList<MenuDto>("menus"));
        }
        #endregion


        #region 活动数据库
        
        /// <summary> 初始化活动数据库 </summary>
        public static void InitializeMeetingDb()
        {
            //活动数据库
            var dbPath = Const.MeetingDbName.DbPath();

            if (File.Exists(dbPath)) //如果升级 存在文件,转移文件在初始化数据
            {
                var backupPath = $"__old_{Const.AppName}_{DateTime.Now:yyyyMMddHHmm}".DbPath();
                var fi = new FileInfo(dbPath);
                fi.MoveTo(backupPath);
            }
            else
            {
                var dir = Path.GetDirectoryName(dbPath);
                if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            var sql = TablesSql();
            using (var conn = new SQLiteConnection(string.Format(Const.ConnectionString, dbPath)))
            {
                conn.Open();
                if (!string.IsNullOrWhiteSpace(Const.MeetingDbPassword))
                    conn.ChangePassword(Const.MeetingDbPassword);
                conn.Execute(sql);
            }
        }

        /// <summary> 组装sql语句 </summary>
        public static string TablesSql()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(EntityBase)));
            var sql = new StringBuilder();
            foreach (var type in types)
            {
                sql.AppendLine(GenerateTableSql(type));
            }
            return sql.ToString();
        }

        #endregion

        /// <summary> 备份数据库 </summary>
        public static string Backup()
        {
            var dbPath = Const.AppName.DbPath();
            if (!File.Exists(dbPath))
                return null;
            var backupPath = CommonHelper.Guid16.DbPath();
            var fi = new FileInfo(dbPath);
            fi.CopyTo(backupPath);
            return backupPath;
        }

        /// <summary> 还原数据库 </summary>
        public static void Restore(string path)
        {
            if (!File.Exists(path))
                return;
            var dbPath = Const.AppName.DbPath();
            var fi = new FileInfo(path);
            fi.MoveTo(dbPath);
        }

        

        

        /// <summary> 生成insert语句 </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string InsertSql<T>()
            where T : EntityBase
        {
            var type = typeof(T);
            var key = $"insert_{type.FullName}";
            var sql = Cache.Get<string>(key);
            if (!string.IsNullOrWhiteSpace(sql))
                return sql;
            //字段
            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO [{type.Name.ToLower()}]");
            var fieldSql = string.Join(",", fields.Select(t => $"[{t.Name.ToLower()}]"));
            var paramSql = string.Join(",", fields.Select(t => $"@{t.Name}"));
            sb.Append($" ({fieldSql}) VALUES ({paramSql})");
            sql = sb.ToString();
            Cache.Set(key, sql);
            return sql;
        }

        /// <summary> 生成修改SQL </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereField">根据哪个字段来修改</param>
        /// <returns></returns>
        public static string UpdateSql<T>(string whereField) where T : EntityBase
        {
            var type = typeof(T);
            var key = $"update_{type.FullName}";
            var sql = Cache.Get<string>(key);
            if (!string.IsNullOrWhiteSpace(sql))
                return sql;
            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            var sb = new StringBuilder();
            sb.Append(string.Format("UPDATE [{0}] SET ", type.Name.ToLower()));
            for (int i = 0; i < fields.Length; i++)
            {
                sb.Append(string.Format(("[{0}]=@{1},"), fields[i].Name.ToLower(), fields[i].Name.ToLower()));
            }
            sql = sb.ToString().Substring(0, sb.Length - 1);
            sql = sql + " where " + whereField + "=@" + whereField;
            Cache.Set(key, sql);
            return sql;
        }


       

        ///// <summary> 版本更新 </summary>
        //private static void VersionUpdateSql()
        //{
        //    var service = AdminDataService.Instance;
        //    var version = service.Query<string>(GlobalKeys.VersionUpdateSql);
        //    if (version == Const.Version) return;
        //    //版本更新Sql
        //    if (string.IsNullOrWhiteSpace(version))
        //    {
        //        service.InsertOrUpdate(GlobalKeys.Menus, ConfigHelper.ReadList<MenuDto>("menus"));
        //    }
        //    else if (new Version(version) < new Version("0.1.3"))
        //    {

        //    }
        //    service.InsertOrUpdate(GlobalKeys.VersionUpdateSql, Const.Version);
        //}

        /// <summary> 生成创建表sql语句 </summary>
        private static string GenerateTableSql(Type type)
        {
            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            var sql = new StringBuilder();
            sql.Append($"CREATE TABLE \"{type.Name.ToLower()}\" (");
            foreach (var field in fields)
            {
                var propType = field.PropertyType;
                if (propType.IsNullableType())
                    propType = propType.GetNonNummableType();
                sql.Append($"\"{field.Name.ToLower()}\" {GetDbType(propType)}");
                //不可为空
                var require = field.GetAttribute<RequireAttribute>();
                if (require != null)
                {
                    sql.Append(" NOT NULL");
                }
                //主键
                var attr = field.GetAttribute<KeyAttribute>();
                if (attr != null)
                {
                    sql.Append(" PRIMARY KEY");
                }
                //默认值
                var def = field.GetAttribute<DefaultValueAttribute>();
                if (def != null)
                {
                    var value = def.Value ?? field.PropertyType.Default();
                    sql.Append($" DEFAULT {value}");
                }
                sql.Append(",");
            }
            return string.Concat(sql.ToString().TrimEnd(','), ");");
        }

        private static string GetDbType(Type dbType)
        {
            if (dbType == typeof(int))
                return "INT";
            if (dbType == typeof(bool))
                return "BOOLEAN";
            if (dbType == typeof(DateTime))
                return "DATETIME";
            return "Text";
        }
    }
}
