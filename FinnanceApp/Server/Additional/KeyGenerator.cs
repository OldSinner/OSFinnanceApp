using System;
using System.Security.Cryptography;
using System.Text;

namespace FinnanceApp.Server.Additional
{
    public class KeyGenerator
    {
        public string GetRandomString()
        {
            Guid guid = Guid.NewGuid();
            string rString = Convert.ToBase64String(guid.ToByteArray());
            rString = rString.Replace("=", "");
            rString = rString.Replace("-", "");  
            rString = rString.Replace("+", ""); 
            rString = rString.Replace("/", ""); 
            rString = rString.Replace(";", ""); 
            return rString;

        }
    }
}