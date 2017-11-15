using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
namespace MapObject.DataTypes
{
    public class MappingOptions
    {
        public bool DateTimeMinMaxToNull { get; set; }
        public DateTime DateTimeMin { get; set; }
        public DateTime DateTimeMax { get; set; }

        public MappingOptions()
        {
            this.DateTimeMinMaxToNull = true;
            this.DateTimeMin = SqlDateTime.MinValue.Value;
            this.DateTimeMax = SqlDateTime.MaxValue.Value;
        }
    }
}
