using System;
using System.Data;
using System.Data.SQLite;

namespace ArcFace.Core.AppService
{
    public abstract class BaseService
    {
        private readonly string _dbName;

        protected BaseService(string dbName = null)
        {
            _dbName = dbName ?? Const.MeetingDbName;
        }

        /// <summary> 获取数据库连接 </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        protected IDbConnection Conn(string dbName = null)
        {
            dbName = string.IsNullOrWhiteSpace(dbName) ? _dbName : dbName;
            var path = dbName.DbPath();
            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = path,
                Version = 3,
                FailIfMissing = false,
                JournalMode = SQLiteJournalModeEnum.Off,
                Pooling = true,
                Password = Const.AdminDbPassword
            };
            return new SQLiteConnection(builder.ConnectionString);
            //            var conn = new SQLiteConnection(string.Format(Const.ConnectionString, path));
            //#if !DEBUG
            //            conn.SetPassword(Const.DbPassword);
            //#endif
            //return conn;
        }

        /// <summary> 使用数据库 </summary>
        /// <param name="connAction"></param>
        /// <param name="name"></param>
        protected T UseConn<T>(Func<IDbConnection, T> connAction, string name = null)
        {
            using (var conn = Conn(name))
            {
                return connAction.Invoke(conn);
            }
        }

        /// <summary> 使用数据库 </summary>
        /// <param name="connAction"></param>
        /// <param name="name"></param>
        protected void UseConn(Action<IDbConnection> connAction, string name = null)
        {
            using (var conn = Conn(name))
            {
                connAction.Invoke(conn);
            }
        }

        protected T UseConn<T>(Func<IDbConnection, IDbTransaction, T> connAction, string name = null)
        {
            using (var conn = Conn(name))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    var result = connAction.Invoke(conn, trans);
                    trans.Commit();
                    return result;
                }
                catch
                {
                    trans.Rollback();
                    return default(T);
                }
            }
        }
    }

    public class BaseService<T> : BaseService where T : BaseService, new()
    {
        protected BaseService(string dbName = null)
            : base(dbName)
        {
        }

        public static T Instance => Singleton<T>.Instance ?? (Singleton<T>.Instance = new T());
    }
}
