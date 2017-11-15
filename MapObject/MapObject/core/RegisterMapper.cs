using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MapObject.interfaces;
using MapObject.DataTypes;
namespace MapObject.core
{
    /// <summary>
    /// Mapping Class used just with static MappingsBuilder
    /// </summary>
    public class RegisterMapper
    {
        public object[] ConstructorParams { get; private set; }
        public object From { get; private set; }
        public Type To { get; private set; }

        public Dictionary<string, string> Mappings { get; private set; }
        public string MappingName { get; private set; }


        public MappingOptions _options { get; private set; }

        public RegisterMapper()
        {
            _options = new MappingOptions();
            MappingName = string.Empty;
        }

        public RegisterMapper(MappingOptions Options)
        {
            this._options = Options;
            MappingName = string.Empty;
        }

        public RegisterMapper MapFrom<TFrom>(TFrom From)
        {
            this.From = From;
            return this;
        }

        public RegisterMapper MapFrom(object From)
        {
            this.From = From;
            return this;

        }

       
        public RegisterMapper WithMappings(Dictionary<string, string> Mappings)
        {
            this.Mappings = Mappings;
            return this;
        }
        public RegisterMapper MapTo<TTo>(string MappingName="", object[] ConstructorParams = null)
        {
            Type toType = typeof(TTo);
            this.To = toType;
            this.MappingName = MappingName;
            this.ConstructorParams = ConstructorParams;
            return this;
        }
        public RegisterMapper MapTo(object To,string MappingName = "")
        {
            Type toType = To.GetType();
            this.To = toType;
            this.MappingName = MappingName;
            return this;
        }

    }
}
