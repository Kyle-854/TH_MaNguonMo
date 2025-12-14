using System.Security.Cryptography;
using System.Text;

namespace FileShareApp.Helpers
{
    public class HashHelper
    {
        public static string ToSHA256(string password)
        {
            using (SHA256? sha256 = SHA256.Create())
            {
                byte[]? bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
