using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapObject.DataTypes;
namespace MapObject.interfaces
{
    public interface IStore
    {
        void RegisterMapping<TFrom, TTo>(string InstanceName, Dictionary<string, string> Mappings = null, object[] ConstructorParams = null);
        void RegisterMapping<TTo>(string InstanceName, Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null);
        void RegisterMapping(object To, string InstanceName = "", Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null);
        void RegisterMapping(object From, object To, string InstanceName = "", Dictionary<string, string> Mappings = null, object[] ConstructorArgs = null);
        

        RegisteredMapping GetMapping<TFrom, TTo>(string InstanceName = "");
        RegisteredMapping GetMapping<TTo>(string InstanceName = "");
        RegisteredMapping GetMapping(object From, object To, string InstanceName = "");
        RegisteredMapping GetMapping(object To,string InstanceName = "");

        RegisteredMapping GetMapping(string InstanceName = "");
    }
}
