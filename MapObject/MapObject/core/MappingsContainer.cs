using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapObject.interfaces;
using MapObject.DataTypes;
namespace MapObject
{
    public class MappingsContainer : IMappingContainer
    {
        private IStore _store;
        private IMapper _mapper;
        private MappingOptions _options;
        public MappingsContainer(IStore Store, IMapper Mapper, MappingOptions Options=null)
        {
            this._store = Store;
            this._mapper = Mapper;
            this._options = Options;
            
        }
        public TTo Map<TFrom, TTo>(TFrom From, string MappingName = "")
        {
            RegisteredMapping mapping = getMapping<TFrom, TTo>(MappingName);     
            if (mapping != null)
            {
                if (!mapping.Mappings.Equals(default(Dictionary<string, string>)))
                {
                    return this._mapper.MapFrom(From).WithMappings(mapping.Mappings).MapTo<TTo>(mapping.ConstructorArgs);
                }
                return this._mapper.MapFrom(From).MapTo<TTo>(mapping.ConstructorArgs);
            } 
            

            return default(TTo);
        }
        public TTo Map<TTo>(object From, string MappingName = "")
        {

            RegisteredMapping mapping = getMapping<TTo>(MappingName);
            if (mapping != null)
            {
                if (mapping.Mappings!=null && !mapping.Mappings.Equals(default(Dictionary<string, string>)))
                {
                    return this._mapper.MapFrom(From).WithMappings(mapping.Mappings).MapTo<TTo>(mapping.ConstructorArgs);
                }
                return this._mapper.MapFrom(From).MapTo<TTo>(mapping.ConstructorArgs);
            }

            return default(TTo);
        }
        public TTo Map<TTo>(string MappingName = "")
        {
            RegisteredMapping mapping = getMapping<TTo>(MappingName);
            if (mapping != null)
            {
                if (mapping.From != null)
                {
               
                    if (mapping.Mappings != null && !mapping.Mappings.Equals(default(Dictionary<string, string>)))
                    {
                        return this._mapper.MapFrom(mapping.From).WithMappings(mapping.Mappings).MapTo<TTo>(mapping.ConstructorArgs);
                    }
                    return this._mapper.MapFrom(mapping.From).MapTo<TTo>(mapping.ConstructorArgs);
                } else
                {
                    if (mapping.Mappings != null && !mapping.Mappings.Equals(default(Dictionary<string, string>)))
                    {
                        return this.WithMappings(mapping.Mappings).MapTo<TTo>(mapping.ConstructorArgs);
                    }
                    return this.MapTo<TTo>(mapping.ConstructorArgs);
                }
            }


            return default(TTo);
        }

        private RegisteredMapping getMapping<TFrom, TTo>(string MappingName = "")
        {
            RegisteredMapping mapping = _store.GetMapping<TFrom, TTo>(MappingName);
            if (mapping == null)
            {
                return getMapping<TTo>(MappingName);
            }
            return mapping;
        }

        private RegisteredMapping getMapping<TTo>(string MappingName = "")
        {
            RegisteredMapping mapping = _store.GetMapping<TTo>(MappingName);
            if (mapping == null)
            {
                return getMapping(MappingName);
            }
            return mapping;
        }

        private RegisteredMapping getMapping(string MappingName = "")
        {
            return _store.GetMapping(MappingName);
        }
        public Mapper MapFrom<TFrom>(TFrom From)
        {
            return _mapper.MapFrom<TFrom>(From);
        }
        public Mapper MapFrom(object From)
        {
            return _mapper.MapFrom(From);
        }


        public Mapper MapFrom(Dictionary<string, object> From)
        {
            return _mapper.MapFrom(From);
        }
        public Mapper WithMappings(Dictionary<string, string> Mappings)
        {
            return _mapper.WithMappings(Mappings);
        }
        public TTo MapTo<TTo>(object[] ConstructorParams = null)
        {
            return _mapper.MapTo<TTo>(ConstructorParams);
        }

        public TTo MapTo<TTo>(TTo To)
        {
            return _mapper.MapTo<TTo>(To);
        }
       
    }
}
