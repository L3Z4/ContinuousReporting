using System;
using System.Configuration;

namespace VSO.RestAPI
{
    internal static class Constantes
    {
        internal static string UserName
        {
            get
            {
                var vsoUserName = ConfigurationManager.AppSettings["VsoUsername"];
                return string.IsNullOrEmpty(vsoUserName) ? "teambuild@outlook.fr" : vsoUserName;
            }
        }

        internal static string Password
        {
            get
            {
                var vsoPassword = ConfigurationManager.AppSettings["VsoPassword"];
                return string.IsNullOrEmpty(vsoPassword) ? "nES0Z6zZorSSTwun" : vsoPassword;
            }
        }

        internal static string Key
        {
            get { return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", UserName, Password))); }
        }

        // internal static string Key { get { return "YXVibGV0LmJhc3RpZW5Ab3V0bG9vay5jb206UGFzczI5Mzg4U2thdGU="; } }
    }
}
