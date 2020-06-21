using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaTestApp.Utilities
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input, CultureInfo cultureInfo=null)
        {
            if (cultureInfo == null)
                cultureInfo = CultureInfo.GetCultureInfo("en-US");

            switch (input)
            {
                case "":
                case null:                
                    return "";
                default:
                    return input.First().ToString().ToUpper() + input.Substring(1).ToLower(cultureInfo);
            }
        }
    }
}
