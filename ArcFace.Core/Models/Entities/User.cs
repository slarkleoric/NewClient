using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models.Entities
{
   public  class User : EntityBase
    {
        [Key, Require]
        public Guid id { get; set; }
        [Require]
        public string account { get; set; }

        [Require]
        public string password { get; set; }

        public string user_name { get; set; }

        public DateTime create_date { get; set; }

        public string comment { get; set; }

        [DefaultValue(0)]
        /// <summary>
        /// 删除状态
        /// </summary>
        public bool is_del { get; set; }
    }
}
