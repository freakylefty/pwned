using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Pwned
{
    static class StringUtil
    {
        //https://stackoverflow.com/a/26558102/376079
        public static string Hash(string input)
        {
            if (input == null)
            {
                input = string.Empty;
            }
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                string result = sb.ToString();
                return result;
            }
        }

        public static string[] GetLines(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new string[] { };
            }
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = value.Split(stringSeparators, StringSplitOptions.None);

            return lines;
        }

        public static KeyValuePair<string, int> GetPair(string value)
        {
            if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, "^[A-Za-z0-9]+:[0-9]+$"))
            {
                return new KeyValuePair<string, int>("", 0);
            }
            var parts = value.Split(':');
            var result = new KeyValuePair<string, int>(parts[0], int.Parse(parts[1]));
            return result;
        }

        //https://stackoverflow.com/a/105793/376079
        public static string FormatNumber(int value)
        {
            return string.Format("{0:n0}", value);
        }
    }
}
