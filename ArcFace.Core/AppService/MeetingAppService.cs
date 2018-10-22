using ArcFace.Core.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.AppService
{
    public class MeetingAppService : BaseService<MeetingAppService>
    {
        private const string Table = "meeting";

        public List<Meeting> Query()
        {
            const string sql = "SELECT * FROM [meeting] ORDER BY [begin_date] DESC";
            return UseConn(conn => conn.Query<Meeting>(sql).ToList());
        }

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="model">活动</param>
        /// <param name="errormsg">错误信息</param>
        /// <returns></returns>
        public int Insert(Meeting model, ref string errormsg)
        {
            const string sql =
                "INSERT INTO [meeting] ([id],[meeting_name],[meeting_code],[begin_date],[end_date],[comment]) VALUES (@id,@meeting_name,@meeting_code,@begin_date,@end_date,@comment)";
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

        public int UpdateMeeting(Meeting model, ref string errormsg)
        {
            const string sql =
                "UPDATE [meeting] SET [meeting_name]=@meeting_name,[begin_date]=@begin_date,[end_date]=@end_date,[comment]=@comment WHERE [id]=@id";
            var result = UseConn(conn => conn.Execute(sql, model));
            return result;
        }


        /// <summary> 删除 </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            const string sql =
                "UPDATE [meeting] SET [is_del]=1 WHERE [id]=@id";
            var result = UseConn(conn => conn.Execute(sql, new { id }));

            return result;
        }
    }
}
