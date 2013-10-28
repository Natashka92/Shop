using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using Shop.DataBase;

namespace Shop
{
    public static class XmlElementExtentions
    {
        public static string GetStringValue(this XElement parentItem, string name)
        {
            var item = parentItem.Element(name);
            if (item == null)
                return null;
            return item.Value;
        }

        public static T GetValue<T>(this XElement parentItem, string name) where T : struct
        {
            var item = parentItem.Element(name);
            if (item == null)
                return default(T);
            var typeConverter = TypeDescriptor.GetConverter(typeof(T));
            return (T)typeConverter.ConvertFromString(item.Value);
        }

        public static T GetEntity<T>(this XElement item, string name, IDictionary<string, T> data) where T : class, INamedEntity, new()
        {
            item = item.Element(name);
            if (item == null)
                return null;
            T result;
            var value = item.Value;

            if (data.TryGetValue(value, out result))
                return result;

            result = new T { Name = value };
            data[value] = result;
            return result;
        }
    }
}
