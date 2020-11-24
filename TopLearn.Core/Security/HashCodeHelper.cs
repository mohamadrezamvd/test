using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TopLearn.Core.Security
{
    public class HashCodeHelper
    {
        public static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashbytes = MD5.Create().ComputeHash(bytes);
            return Encoding.UTF8.GetString(hashbytes);
        }
    }
}
