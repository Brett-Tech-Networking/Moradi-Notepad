using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Moradi_Notepad
{
        internal enum HashType
        {
            MD5,
            SHA1,
            SHA512
        }

        internal static class Hasher
        {
            internal static string HashFile(string filePath, HashType algo)
            {
                switch (algo)
                {
                    case HashType.MD5:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                        break;

                    case HashType.SHA1:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                    break;

                    case HashType.SHA512:
                    return MakeHashString(MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open)));
                    break;

                    default:
                    return "";
                        break;
                }
            }

            private static string MakeHashString(byte[] hash)
            {
                StringBuilder s = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    s.Append(b.ToString("X2").ToLower());

                return s.ToString();
            }
      }
}
