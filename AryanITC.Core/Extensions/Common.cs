using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AryanITC.Core.Extensions
{
    public static class CommonExtension
    {
        public static string GetEnumName(this Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType()
                .GetMember(myEnum.ToString())
                .FirstOrDefault();

            if (enumDisplayName != null)
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();

            return "";
        }

        public static string FixedEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
