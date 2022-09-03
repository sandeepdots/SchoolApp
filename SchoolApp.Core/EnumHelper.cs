using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Core
{
    public static class EnumHelper
    {
        public static string GetDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }
        public static string GetDisplayName<TEnum>(this TEnum enumValue) //where TEnum : struct
        {
            try
            {
                return enumValue.GetType()
                           .GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .GetName();
            }
            catch (Exception e)
            {
                return enumValue.ToString();
            }
        }

        //public static List<TEnum> SearchByDescription<TEnum>(this TEnum enumType, string description)
        //{
        //    List<TEnum> list = new List<TEnum>();
        //    var type = typeof(TEnum);
        //    if (!type.IsEnum) throw new InvalidOperationException();
        //    foreach (var field in type.GetFields())
        //    {
        //        var attribute = Attribute.GetCustomAttribute(field,
        //            typeof(DescriptionAttribute)) as DescriptionAttribute;
        //        if (attribute != null)
        //        {
        //            if (attribute.Description.Contains(description))
        //                list.Add((TEnum)field.GetValue(null));
        //        }
        //        else
        //        {
        //            if (field.Name.Contains(description))
        //                list.Add((TEnum)field.GetValue(null));
        //        }
        //    }
        //    return list;
        //}
    }
    public class Enum<TEnum> where TEnum : struct, IConvertible
    {
        public static int Count
        {
            get
            {
                if (!typeof(TEnum).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetNames(typeof(TEnum)).Length;
            }
        }
        public static List<byte> SearchByDescription(string description)
        {
            List<byte> list = new List<byte>();
            description = string.IsNullOrWhiteSpace(description) ? string.Empty : description.ToLower().Trim().Replace(" ", "");
            if (string.IsNullOrWhiteSpace(description))
            {
                return list;
            }

            var type = typeof(TEnum);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.ToLower().Trim().Replace(" ", "").Contains(description))
                    {
                        TEnum enumval = (TEnum)field.GetValue(null);
                        list.Add(Convert.ToByte(enumval));
                    }
                    //list.Add(((byte)(TEnum)field.GetValue(null)));
                }
                else if (field.IsLiteral)
                {
                    if (field.Name.ToLower().Trim().Replace(" ", "").Contains(description))
                    {
                        bool tryConvert = Convert.ToBoolean(field.GetValue(null));

                        if (tryConvert)
                        {
                            TEnum enumval = (TEnum)field.GetValue(null);

                            list.Add(Convert.ToByte(enumval));
                            //list.Add(((byte)(TEnum)field.GetValue(null))); 
                        }
                    }
                }
            }
            return list;
        }
    }
}
