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
    /// Alt registration with builder pattern Uses RegisterMapper for registration then build normal MappingsContainer which users Mapper.
    /// </summary>
    public class MappingsBuilder
    {
        private IStore _store;
        private RegisterMapper _mapper;

        public MappingsBuilder()
        {
            _store = new Store();
            _mapper = new RegisterMapper();

        }
        public MappingsBuilder(IStore Store)
        {
            _store = Store;
            _mapper = new RegisterMapper();
        }

        public MappingsBuilder RegisterMapFrom<TFrom>(TFrom From)
        {
            this._mapper.MapFrom<TFrom>(From);
            return this;
        }

        public MappingsBuilder RegisterMapFrom(object From)
        {
            this._mapper.MapFrom(From);
            return this;


        }

        public MappingsBuilder RegisterMapFrom(Dictionary<string, object> From, object[] ConstructorParams = null)
        {
            this._mapper = this._mapper.MapFrom(From);
            return this;
        }
        public  MappingsBuilder WithMappings(Dictionary<string, string> Mappings)
        {
            this._mapper = this._mapper.WithMappings(Mappings);
            return this;
        }
        public bool RegisterMapTo<TTo>(string MappingName = "", object[] ConstructorParams = null)
        {
            this._mapper = this._mapper.MapTo<TTo>(MappingName,ConstructorParams);
            return register();


        }
        
        public bool RegisterMapTo(object To)
        { 
            this._mapper = this._mapper.MapTo(To);
            return register();

        }

        private bool register()
        {
            bool suc = false;
            if (_mapper.From != null)
            {
                if (_mapper.To != null)
                {
                    _store.RegisterMapping(_mapper.From, _mapper.To, _mapper.MappingName, _mapper.Mappings, _mapper.ConstructorParams);
                    this._mapper = new core.RegisterMapper();
                    return true;
                }
            } else
            {
                _store.RegisterMapping( _mapper.To, _mapper.MappingName, _mapper.Mappings, _mapper.ConstructorParams);
                this._mapper = new core.RegisterMapper();
                return true;

            }
            this._mapper = new core.RegisterMapper();
            return suc; 
        }

        public IMappingContainer Build(MappingOptions MappingOptions=null)
        {

            return new MappingsContainer(this._store, new Mapper(), MappingOptions);
        }
    }
}
