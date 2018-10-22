using ArcFace.Core.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.AppService
{
    public class MeetingGroupAppService : BaseService<MeetingGroupAppService>
    {
        private const string Table = "meeting";

        public List<Meeting_Group> Query(string meeting_id)
        {
            const string sql = "SELECT * FROM [Meeting_Group] where is_del = 0 and meeting_id= @meeting_id ORDER BY [create_date] DESC";

            return UseConn(conn => conn.Query<Meeting_Group>(sql,new { meeting_id }).ToList());
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="model">分组</param>
        /// <param name="errormsg">错误信息</param>
        /// <returns></returns>
        public int Insert(Meeting_Group model, ref string errormsg)
        {
            const string sql =
                "INSERT INTO [Meeting_Group] ([id],[meeting_id],[group_name],[create_date],[comment]) VALUES (@id,@meeting_id,@group_name,@create_date,@comment)";
            int result = 0;
            try
            {
                result = UseConn(conn => conn.Execute(sql, model));
                if (result > 0)
                {
                    //同步操作
                }
            }
            catch (Exception er)
            {
                errormsg = er.ToString();
            }
            return result;
        }

        public int UpdateGroupName(string id,string name, ref string errormsg)
        {
            const string sql =
                "UPDATE [Meeting_Group] SET [group_name]=@name WHERE [id]=@id";
            var result = UseConn(conn => conn.Execute(sql, new { id ,name}));
            return result;
        }


        /// <summary> 删除 </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            const string sql =
                "UPDATE [Meeting_Group] SET [is_del]= 1 WHERE [id]=@id";
            var result = UseConn(conn => conn.Execute(sql, new { id }));

            return result;
        }
    }
}
