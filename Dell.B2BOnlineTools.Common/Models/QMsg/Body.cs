
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dell.B2BOnlineTools.Common.Models.QMsg
{
    public class Body
    {
        public Body()
        {
            Items = new Items();
        }
        [Required]
        public Items Items { get; set; }
        public Dictionary<string, ConstantValue> Constants { get; set; }
    }

    public class Items
    {
        public Items()
        {
            Arguments = new List<ArgInfo>();
            Values = new List<object[]>();
        }
        public List<ArgInfo> Arguments { get; set; }
        public List<object[]> Values { get; set; }
    }

    public class ArgInfo
    {
        [Required]
        public ushort Index { get; set; }
        [Required]
        public string Name { get; set; }
        public string DataType { get; set; } = typeof(string).Name;
        public bool IsValueEncrypted { get; set; } = false;
    }

    public class ConstantValue
    {
        public object Value { get; set; }
        public string DataType { get; set; } = typeof(string).Name;
        public bool IsValueEncrypted { get; set; } = false;
    }
}
