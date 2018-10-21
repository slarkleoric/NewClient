using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models.Entities
{
    public class Sys_Log : EntityBase
    {
        [Key, Require]
        public Guid id { get; set; }

        [Require]
        public Guid user_id { get; set; }
        
        public string operation { get; set; }

        public DateTime operate_date { get; set; }

        public string comment { get; set; }

    }
}
