using System;
using System.Text.RegularExpressions;

namespace Coukkas.Core
{
    public  static class StringExtensions
    {
        public static bool IsPasswordMatch(this string @string)
        {
            string pattern = @"^(?=.*[a-z]+)(?=.*[A-Z]+)(?=.*[0-9]+)(?=.*[\\W_]+)(?!.*\\s).{5,15}$";
            return  Regex.IsMatch(@string, pattern);
        }

        public static bool IsLoginMatch(this string @string)
        {
            string pattern = @"^[a-zA-Z0-9].{3,12}$";
            return  Regex.IsMatch(@string, pattern);
        }

        public static bool IsEmailMatch(this string @string)
        {
            string pattern =  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            return  Regex.IsMatch(@string, pattern);
			
        }

    }
}