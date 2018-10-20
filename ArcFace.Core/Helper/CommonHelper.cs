using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Helper
{
    public static class CommonHelper
    {
        /// <summary> 获取Guid </summary>
        /// <returns></returns>
        public static string Guid32 => Guid.NewGuid().ToString();

        /// <summary>
        /// 16位Guid
        /// </summary>
        /// <returns></returns>
        public static string Guid16
        {
            get
            {
                var i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current * (b + 1));
                return $"{i - DateTime.Now.Ticks:x}";
            }
        }

        /// <summary>
        /// 获取对象指定属性值
        /// </summary>
        /// <param name="info">对象</param>
        /// <param name="field">属性名</param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null)
            {
                return null;
            }
            var t = info.GetType();
            var property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.FirstOrDefault()?.GetValue(info, null);
        }

        ///// <summary> 获取7位签到码 </summary>
        ///// <param name="meetingId"></param>
        ///// <param name="identity">自增</param>
        ///// <returns></returns>
        //public static string SignInCode(string meetingId, bool identity = true)
        //{
        //    var dto = AdminDataService.Instance.Query<SignerToCodeDto>(GlobalKeys.MeetingSignPrefix);
        //    if (dto == null)
        //        throw new Exception("没有签到码信息");
        //    if (dto.Enroll_Count > 99999)
        //        throw new Exception("签到码已超出范围");
        //    var code =
        //        $"{dto.Signer_Code}{(dto.Enroll_Count + 1).ToString().PadLeft(5, '0')}";
        //    if (!identity)
        //        return code;
        //    dto.Enroll_Count++;
        //    AdminDataService.Instance.InsertOrUpdate(GlobalKeys.MeetingSignPrefix, dto);
        //    return code;
        //}
    }
}
