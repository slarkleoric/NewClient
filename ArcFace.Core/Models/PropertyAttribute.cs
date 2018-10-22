using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Models
{
    public class KeyAttribute : Attribute { }

    public class RequireAttribute : Attribute { }

    public class DefaultValueAttribute : Attribute
    {
        public object Value { get; }

        public DefaultValueAttribute(object value = null)
        {
            Value = value;
        }
    }

    public class UniqueAttribute : Attribute { }
}
