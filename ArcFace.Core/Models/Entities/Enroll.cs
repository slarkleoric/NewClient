using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models.Entities
{
    public class Enroll : EntityBase
    {
        [Key, Require]
        public Guid id { get; set; }
        /// <summary> 活动ID </summary>
        [Require]
        public Guid meeting_id { get; set; }
        /// <summary>
        /// 分组id
        /// </summary>
        [Require]
        public Guid group_id { get; set; }

        /// <summary>
        /// 嘉宾姓名
        /// </summary>
        public string enroll_name { get; set; }

        /// <summary>
        /// 嘉宾电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string job { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_date { get; set; }
        /// <summary>
        /// 签到次数
        /// </summary>
        [Require, DefaultValue]
        public int sign_count { get; set; }
        /// <summary>
        /// 用户详情备注
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 手机号后4位
        /// </summary>
        public string phone_last_number { get; set; }

        /// <summary>
        /// 人脸地址路径
        /// </summary>
        public byte[] face_img { get; set; }
        /// <summary>
        /// 人脸特征值
        /// </summary>
        public byte[] face_arcnum { get; set; }
        /// <summary>
        /// 签到码
        /// </summary>
        public string sign_qr { get; set; }
    }
}
