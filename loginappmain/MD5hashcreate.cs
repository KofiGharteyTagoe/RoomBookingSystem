using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LoginAppmain
{
    public class MD5hashcreate
    {

        public string createMd5hash(string password)
        {
            MD5 md5Hash = MD5.Create();
            //convert password into a byte array and compute the hash
            byte[] pwHashData = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Stringbuilder to collect the bytes
            StringBuilder sBuilder = new StringBuilder();

            //loop through each byte of the hash data
            //format each one as a hexidecimal string
            for (int i = 0; i < pwHashData.Length; i++)
            {
                sBuilder.Append(pwHashData[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        //Compare password input with Hashcode
        public bool compareHash(string password, string hashPhrase)
        {
            MD5 md5Hash = MD5.Create();

            //convert input to hash
            string pwHash = createMd5hash(password);

            //Compare two hashes by creating StringComparer
            StringComparer compareHash = StringComparer.OrdinalIgnoreCase;

            if (compareHash.Compare(password, hashPhrase) == 0) // If 0, there is a value
            {
                return true;
            }
            else
                return false;

        }

    }
}