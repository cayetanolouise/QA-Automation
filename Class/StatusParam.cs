using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_WebDriver_Test.Class
{
    class StatusParam
    {
        public static int code;
        public static string errorMessage;
        public static string login;
    }

    public class Credential
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class CredentialData
    {
        public List<Credential> valid { get; set; }
        public List<Credential> invalidUsername { get; set; }
        public List<Credential> invalidPassword { get; set; }
    }
}
