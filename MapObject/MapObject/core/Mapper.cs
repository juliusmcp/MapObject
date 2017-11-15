using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MapObject.interfaces;
using MapObject.DataTypes;
namespace MapObject
{
  
    /// <summary>
    /// Mapping class
    /// </summary>
    public class Mapper : IMapper
    {

        private object _from;
        private IList<PropertyInfo> _fromProperties;
        private Dictionary<string, string> _mappings;
        private Dictionary<string, object> _fromDictionary;
        private string _mappingName = string.Empty;

        private delegate object getProp(PropertyInfo toProperty, string AltPropertyName = "");
        private MappingOptions _options;

        public Mapper()
        {
            _options = new MappingOptions();
        }

        public Mapper(MappingOptions Options)
        {
            this._options = Options;
        }

        public Mapper MapFrom<TFrom>(TFrom From)
        {
            this._from = From;
            _fromProperties = new List<PropertyInfo>(From.GetType().GetProperties());
          
            return this;
        }

        public Mapper MapFrom(object From)
        {
            this._from = From;
            _fromProperties = new List<PropertyInfo>(From.GetType().GetProperties());
          
            return this;



        }

        public Mapper MapFrom(Dictionary<string,object> From)
        {
            this._fromDictionary = From;
         
            return this;
        }
        public Mapper WithMappings(Dictionary<string, string> Mappings)
        {
            this._mappings = Mappings;
            return this;
        }
        public TTo MapTo<TTo>(object[] ConstructorParams = null)
        {
            Type toType = typeof(TTo);
            object toObject = Activator.CreateInstance(toType, ConstructorParams);
            return MapTo<TTo>((TTo)conversion(toObject, toType));
        }

        public TTo MapTo<TTo>(object To)
        {
            Type toType = typeof(TTo);
            if (To != null)
            {
                TTo converted = mapToInstance<TTo>(To);
                if (converted!=null)
                {
                    return converted;
                }
                else
                {
                    _from = To;
                    _fromProperties = new List<PropertyInfo>(To.GetType().GetProperties());
                    object toObject = Activator.CreateInstance(toType);
                    return mapToInstance<TTo>(toObject);

                }

            }
                
              return default(TTo);


         }

        private TTo mapToInstance<TTo>(object To)
        {
            Type toType = To.GetType();
            if (To != null)
            {
                IList<PropertyInfo> toProperties = new List<PropertyInfo>(toType.GetProperties());
                foreach (PropertyInfo propertyInfo in toProperties)
                {
                    object toValue = GetFromValue(propertyInfo);
                    if (toValue != null)
                    {
                        object value = To.GetType().GetProperty(propertyInfo.Name).GetValue(To);
                        Type propertyType = propertyInfo.PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                        object def = getDefaultValue(underlyingType);

                        if (value != null)
                        {
                            if (value.Equals(def))
                            {
                                To.GetType().GetProperty(propertyInfo.Name).SetValue(To, toValue, null);
                            }
                            else
                            {
                                To.GetType().GetProperty(propertyInfo.Name).SetValue(To, value, null);
                            }
                        } else
                        {
                            To.GetType().GetProperty(propertyInfo.Name).SetValue(To, toValue, null);
                        }
                    }


                }
                return (TTo)conversion(To, typeof(TTo));
            }
            return default(TTo);
        }

        
        private object GetFromValue(PropertyInfo propertyInfo)
        {

                if (this._fromProperties != null)
                {

                    if (this._mappings != null && this._mappings.Any(x => x.Value.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)
                                  && this._fromProperties.Any(n => n.Name.Equals(x.Key, StringComparison.InvariantCultureIgnoreCase))))
                    {

                        KeyValuePair<string, string> mapping = this._mappings.First(x => x.Value.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase));

                        return getCorrespondingProp(getCorrespondingPropFromObject, propertyInfo, mapping.Key);

                    }
                    else
                    {
                        return getCorrespondingProp(getCorrespondingPropFromObject, propertyInfo);
                    }

                } else  if (this._fromDictionary!=default(Dictionary<string, object>))  {
                    if (this._mappings != null && this._mappings.Any(x => x.Value.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)
                                      && this._fromDictionary.Any(n => n.Key.Equals(x.Key, StringComparison.InvariantCultureIgnoreCase))))
                    {

                        KeyValuePair<string, string> mapping = this._mappings.First(x => x.Value.Equals(propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase));

                        return getCorrespondingProp(getCorrespondingPropFromDictionary, propertyInfo, mapping.Key);

                    }
                    else
                    {
                        return getCorrespondingProp(getCorrespondingPropFromDictionary, propertyInfo);
                    }
           
                }
            return null;

        }
        private object getCorrespondingProp(getProp PropGetter,PropertyInfo Info, string AltPropertyName="")
        {
            return PropGetter.Invoke(Info,AltPropertyName);
        }
        private object getCorrespondingPropFromDictionary(PropertyInfo toProperty, string AltPropertyName = "")
        {
            Type propertyType = toProperty.PropertyType;
            Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            string toPropertyName = AltPropertyName == string.Empty ? toProperty.Name: AltPropertyName;

            object toValue;
            bool found = this._fromDictionary.TryGetValue(toPropertyName, out toValue);
            if (found)
            {
                return conversion(toValue, underlyingType);
            }

            return null;
        }

        private object getCorrespondingPropFromObject(PropertyInfo toProperty, string AltPropertyName = "")
        {
            Type propertyType = toProperty.PropertyType;
            Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            string toPropertyName = AltPropertyName == string.Empty ?  toProperty.Name : AltPropertyName;
            var correspondingProp = this._fromProperties.FirstOrDefault(n => n.Name.Equals(toPropertyName, StringComparison.InvariantCultureIgnoreCase));
            if (correspondingProp != null)
            {
                return conversion(correspondingProp.GetValue(this._from), underlyingType);
            }   
            return null;
        }

        private object conversion(object Value, Type ObjectType)
        {
            object result = null;

            if (ObjectType.Equals(typeof(DateTime)))
            {

                return handleDate(Value);

            }

            if (Value==null)
            {
                return getDefaultValue(ObjectType);
            }
           
            try
            {
                result = Convert.ChangeType(Value, ObjectType);
            }
            catch (InvalidCastException)
            {
                return getDefaultValue(ObjectType);
            }
            catch (FormatException)
            {
                return getDefaultValue(ObjectType);
            }
            catch (OverflowException)
            {
                return getDefaultValue(ObjectType);
            }
            catch (ArgumentNullException)
            {
                return getDefaultValue(ObjectType);
            }
            return result;
        }

        private object getDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }

        private object handleDate(object Value)
        {

            if (Value.Equals(DateTime.MinValue))
            {
                if (_options.DateTimeMinMaxToNull)
                {
                    return null;
                }
                else 
                {
                    return _options.DateTimeMin;

                }

            } else if (Value.Equals(DateTime.MaxValue))
            {
                if (_options.DateTimeMinMaxToNull)
                {
                    return null;
                }
                else
                {
                    return _options.DateTimeMax;

                }
            }  else
            {
                //toObject.GetType().GetProperty(propertyInfo.Name).SetValue(toObject, toValue, null);

                try
                {
                    return Convert.ChangeType(Value, typeof(DateTime));
                }
                catch
                {
                    return getDefaultValue(typeof(DateTime));
                }
            }
        }
    }

}
