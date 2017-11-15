using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapObject.interfaces;
using MapObject.DataTypes;
namespace MapObject.core
{
    public class Store : IStore
    {
        private ConcurrentDictionary<RegisteredMappingKey, RegisteredMapping> _mappings;
        public Store()
        {
            this._mappings = new ConcurrentDictionary<RegisteredMappingKey, RegisteredMapping>();
        }

        public RegisteredMapping GetMapping(string InstanceName = "")
        {


            KeyValuePair<RegisteredMappingKey, RegisteredMapping> entry= this._mappings.FirstOrDefault(n => n.Key.NamedMapping.Equals(InstanceName, StringComparison.InvariantCultureIgnoreCase));
            if (!entry.Equals(default(KeyValuePair<RegisteredMappingKey, RegisteredMapping>)))
            {
                return entry.Value;
            }
            return null;
        }
        public RegisteredMapping GetMapping(object From, object To, string InstanceName = "")
        {
            Type TTo = To.GetType();
            Type TFrom = From.GetType();
            RegisteredMappingKey key = new RegisteredMappingKey(TFrom, TTo, InstanceName);
            RegisteredMapping mapping;
            if (this._mappings.TryGetValue(key, out mapping))
            {
                return mapping;
            }
            return null;
        }
        public RegisteredMapping GetMapping<TFrom,TTo>(string InstanceName = "")
        {
            Type To = typeof(TTo);
            Type From = typeof(TFrom);
            RegisteredMappingKey key = new RegisteredMappingKey(From, To,  InstanceName);
            RegisteredMapping mapping;
            if (this._mappings.TryGetValue(key, out mapping))
            {
                return mapping;
            }
            return null;


        }
        public RegisteredMapping GetMapping(object To, string InstanceName = "")
        {
            Type TTo = To.GetType();
            RegisteredMappingKey key = new RegisteredMappingKey(TTo, InstanceName);
            RegisteredMapping mapping;
            if (this._mappings.TryGetValue(key, out mapping))
            {
                return mapping;
            }
            return null;
        }
        public RegisteredMapping GetMapping<TTo>(string InstanceName = "")
        {
            Type To = typeof(TTo);
            RegisteredMappingKey key = new RegisteredMappingKey(To, InstanceName);
            RegisteredMapping mapping;
            if (this._mappings.TryGetValue(key, out mapping))
            {
                return mapping;
            }
            return null;


        }
        public void RegisterMapping<TTo>(string InstanceName="", Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null)
        {
            Type To = typeof(TTo);
            RegisteredMappingKey key = new RegisteredMappingKey(To, InstanceName);
            RegisteredMapping mapping = new RegisteredMapping(key, To, Mappings, ConstructorArgs);
            this._mappings.TryAdd(key, mapping);
        }
        public void RegisterMapping<TFrom, TTo>(string InstanceName="", Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null)
        {
            Type To = typeof(TTo);
            Type From = typeof(TFrom);
            RegisteredMappingKey key = new RegisteredMappingKey(To, InstanceName);
            RegisteredMapping mapping = new RegisteredMapping(key, From, To, Mappings, ConstructorArgs);
            this._mappings.TryAdd(key, mapping);
        }

        public void RegisterMapping(object To,string InstanceName = "", Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null)
        {
            Type TTo = To.GetType();
            RegisteredMappingKey key = new RegisteredMappingKey(TTo, InstanceName);
            RegisteredMapping mapping = new RegisteredMapping(key, TTo, Mappings, ConstructorArgs);
            this._mappings.TryAdd(key, mapping);
        }
        public void RegisterMapping(object From, object To,string InstanceName = "", Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null)
        {
            Type TTo = To.GetType();
            Type TFrom = From.GetType();
            RegisteredMappingKey key = new RegisteredMappingKey(TTo, InstanceName);
            RegisteredMapping mapping = new RegisteredMapping(key, TFrom, TTo, Mappings, ConstructorArgs);
            this._mappings.TryAdd(key, mapping);
        }
    }
}
