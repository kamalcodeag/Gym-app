using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fitness
{
    public static class Hash
    {
        public static string GetHash(string password)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(password);
            byte[] enc = new SHA256Managed().ComputeHash(bytes);
            return new ASCIIEncoding().GetString(enc);
        }

        public static bool CheckHash(string hashedPassword,string simplePassword)
        {
            return hashedPassword == GetHash(simplePassword);
        }
        

    }
}
