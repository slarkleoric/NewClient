using ArcFace.Core.Dtos;
using ArcFace.Core.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.AppService
{
    public class UserAppService : BaseService<UserAppService>
    {
        private const string Table = "user";
        
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="psw">密码明文</param>
        /// <returns></returns>
        public User Login(string account, string psw)
        {
            const string sql =
                "SELECT * FROM [user] where [is_del]=0 and [account]=@account and password = @psw";
            return UseConn(conn => conn.Query<User>(sql,new { account, psw }).FirstOrDefault());
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model">用户</param>
        /// <param name="errormsg">错误信息</param>
        /// <returns></returns>
        public int Insert(User model,ref string errormsg)
        {
            const string sql =
                "INSERT INTO [user] ([id],[account],[password],[user_name],[create_date],[comment],[is_del]) VALUES (@id,@account,@password,@user_name,@create_date,@comment,@is_del)";

            int result = 0;
            try
            {
                result = UseConn(conn => conn.Execute(sql, model));
                if (result > 0)
                {
                    //同步操作
                }
            }
            catch(Exception er)
            {
                errormsg = er.ToString();
            }
            return result;
        }

        /// <summary> 删除 </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            const string sql =
                "DELETE FROM [user] WHERE [id]=@id";
            var result = UseConn(conn => conn.Execute(sql, new { id }));
            
            return result;
        }
    }
}
