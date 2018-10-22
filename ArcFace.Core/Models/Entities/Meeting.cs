using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models.Entities
{
    public class Meeting : EntityBase
    {
        [Key, Require]
        public Guid id { get; set; }
        /// <summary> 活动名称 </summary>
        [Require]
        public string meeting_name { get; set; }

        /// <summary>
        /// 会议编码
        /// </summary>
        [Require]
        public string meeting_code { get; set; }

        /// <summary>
        /// 活动开始日期
        /// </summary>
        [Require]
        public DateTime begin_date { get; set; }

        /// <summary>
        /// 活动结束日期
        /// </summary>
        [Require]
        public DateTime end_date { get; set; }

        /// <summary>
        /// 会议详情备注
        /// </summary>
        public string comment { get; set; }

        public bool is_del { get; set; }
       
    }
}
