using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models.Entities
{
    public class Sign_Record : EntityBase
    {
        [Key, Require]
        public Guid id { get; set; }
        /// <summary>
        /// 参会者ID
        /// </summary>
        [Require]
        public Guid enroll_id { get; set; }

        [Require]
        public Guid meeting_id { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime sign_date { get; set; }
    }
}
