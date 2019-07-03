using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SistemaViveroApp.Helpers
{
    public static class RegexHelper
    {
        public static bool IsValidEmailAddress(string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
