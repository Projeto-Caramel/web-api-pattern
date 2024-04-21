using System.ComponentModel;
using System.Reflection;

namespace Caramel.Pattern.Services.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static bool IsInRange(this Enum value, int parameter)
        {
            foreach (Enum item in Enum.GetValues(value.GetType()))
                if (Convert.ToInt32(item) == parameter)
                    return true;

            return false;
        }
    }
}
