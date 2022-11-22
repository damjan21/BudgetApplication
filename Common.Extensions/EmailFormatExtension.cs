using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class EmailFormatExtension
    {
        public static void CheckEmailFormat(string email)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                throw new ArgumentException("Wrong email format.");
        }

        public static string GetEmailWithoutDomain(string email)
        {
            string username = email.Split('@').First();

            return username;
        }
    }
}
