using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapObject.interfaces;
using MapObject.DataTypes;
using MapObject.core;
namespace MapObject
{
    /// <summary>
    /// Non static container builder. Verbose Registrations. Builds MappingsContainer which uses Mapper
    /// </summary>

    public class MappingsContainerBuilder 
    {
        private static IStore _store;

        static MappingsContainerBuilder()
        {
            _store = new Store();
        }
        public static void SetStore(IStore Store)
        {
            _store = Store;
        }
       
        public static void RegisterMapping(object From, object To,Dictionary<string, string> Mappings = null, string MappingName = "", object[] ConstructorArgs = null)
        {
            
            _store.RegisterMapping(From,To,MappingName, Mappings, ConstructorArgs);
        }
        public static void RegisterMapping(object To,Dictionary<string, string> Mappings = null, string MappingName = "", object[] ConstructorArgs = null)
        {
            
            _store.RegisterMapping(To,MappingName, Mappings, ConstructorArgs);
        }

        public static void RegisterMapping<TFrom, TTo>(Dictionary<string, string> Mappings = null, string MappingName = "", object[] ConstructorArgs = null)
        {
          
            _store.RegisterMapping<TFrom, TTo>(MappingName, Mappings, ConstructorArgs);
        }
        public static void RegisterMapping<TTo>(Dictionary<string, string> Mappings = null, string MappingName = "", object[] ConstructorArgs = null)
        {
   
            _store.RegisterMapping<TTo>(MappingName, Mappings, ConstructorArgs);
        }

        public static IMappingContainer Build(MappingOptions MappingOptions = null)
        {
         
            return new MappingsContainer(_store, new Mapper(),MappingOptions);
        }
    }
}
