using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models.Entities
{
   public class Meeting_Group : EntityBase
    {
        [Key, Require]
        public Guid id { get; set; }

        [Require]
        public Guid meeting_id { get; set; }

        /// <summary> 分组名称 </summary>
        [Require]
        public string group_name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string comment { get; set; }
    }
}
