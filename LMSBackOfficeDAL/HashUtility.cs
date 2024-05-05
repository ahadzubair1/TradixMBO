using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography;

namespace LMSBackOfficeDAL
{
    public static class HashUtility
    {
        public static string ComputeSHA512Hash(string plainText)
        {
            using (var sha512 = SHA512.Create())
            {
                // Compute hash from the input string
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // Convert byte array to a string representation
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
