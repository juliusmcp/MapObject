using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapObject.core;
namespace MapObject.interfaces
{
    public interface IMappingContainer
    {

        TTo Map<TFrom, TTo>(TFrom From, string MappingName = "");
       
        TTo Map<TTo>(object From, string MappingName = "");
      
        TTo Map<TTo>(string MappingName = "");
    
        Mapper MapFrom<TFrom>(TFrom From);
       

        Mapper MapFrom(object From);
        


        Mapper MapFrom(Dictionary<string, object> From);
        
        Mapper WithMappings(Dictionary<string, string> Mappings);
        
        TTo MapTo<TTo>(object[] ConstructorParams = null);
        

        TTo MapTo<TTo>(TTo To);
    }
}
