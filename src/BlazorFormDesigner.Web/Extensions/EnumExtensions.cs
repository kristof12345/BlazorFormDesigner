using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BlazorFormDesigner.Web.Extensions
{
    public static class EnumExtensions
    {
        public static string GetAttributeValue<T>(this T enumVal)
        {
            var enumType = typeof(T);
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr != null) return attr.Value;
            return string.Empty;
        }

        public static T GetValueFromAttribute<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                {
                    if (attribute.Value == description) return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description) return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }

        public static List<string> GetAttributeList<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<Enum>().Select(val => val.GetAttribute<EnumMemberAttribute>().Value).ToList();
        }

        private static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}