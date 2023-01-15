using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.CashFlow.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string Description<TEnum>(this TEnum @enum) where TEnum : Enum
        {
            FieldInfo field = @enum.GetType().GetField(@enum.ToString());
            DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
            return (array != null && array.Length != 0) ? array[0].Description : @enum.ToString();
        }
    }
}
