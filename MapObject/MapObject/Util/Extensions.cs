using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MapObject.DataTypes;
namespace MapObject.Util
{
    public static class Extensions
    {

        public static Dictionary<string, object> GetDictionaryFromObject<TFrom>(this TFrom From, bool SubTree=false)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            Type fromtype = typeof(TFrom);
            List<PropertyInfo> _fromProperties = new List<PropertyInfo>(fromtype.GetType().GetProperties());
            foreach (PropertyInfo propinfo in _fromProperties)
            {
                if (!SubTree)
                {
                    output.Add(propinfo.Name, propinfo.GetValue(From));
                } else
                {
                    Type tPropertyType = propinfo.GetType().GetProperty(propinfo.Name).PropertyType;
                    if (tPropertyType.IsClass)
                    {
                        output.Add(propinfo.Name, propinfo.GetValue(From).GetDictionaryFromObject(SubTree));
                    }
                    else
                    {
                        output.Add(propinfo.Name, propinfo.GetValue(From));
                    }
                }
            }
            return output;
        }
        public static Dictionary<string, object> GetDictionaryFromObject(this object From, bool SubTree = false)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            Type fromtype = From.GetType();
            List<PropertyInfo> _fromProperties = new List<PropertyInfo>(fromtype.GetType().GetProperties());
            foreach (PropertyInfo propinfo in _fromProperties)
            {
                if (!SubTree)
                {
                    output.Add(propinfo.Name, propinfo.GetValue(From));
                } else
                {
                    Type tPropertyType = propinfo.GetType().GetProperty(propinfo.Name).PropertyType;
                    if (tPropertyType.IsClass)
                    {
                        output.Add(propinfo.Name, propinfo.GetValue(From).GetDictionaryFromObject(SubTree));
                    }
                    else
                    {
                        output.Add(propinfo.Name, propinfo.GetValue(From));
                    }
                }
            }
            return output;
        }

        
    }
}
