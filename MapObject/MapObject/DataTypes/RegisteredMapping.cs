using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapObject.DataTypes
{
    public class RegisteredMapping
    {
        public RegisteredMappingKey MappingKey { get; private set; }
        public Type To { get; private set; }

        public Type From { get; private set; }
        public Dictionary<string, string> Mappings { get; private set; }

        public object[] ConstructorArgs { get; private set; }

        public RegisteredMapping(RegisteredMappingKey MappingKey, Type To, Dictionary<string, string> Mappings=null, object[] ConstructorArgs =null)
        {
            this.MappingKey = MappingKey;
            this.To = To;
            this.Mappings = Mappings;
            this.ConstructorArgs = ConstructorArgs;
        }
        public RegisteredMapping(RegisteredMappingKey MappingKey, Type From, Type To, Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null)
        {
            this.MappingKey = MappingKey;
            this.To = To;
            this.From = From;
            this.Mappings = Mappings;
            this.ConstructorArgs = ConstructorArgs;
        }

    }
}
