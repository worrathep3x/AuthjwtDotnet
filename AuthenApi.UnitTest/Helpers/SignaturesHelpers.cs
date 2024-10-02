using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace AuthenApi.UnitTest.Helpers;

public partial class SignaturesHelpers
{
    public static string SHA256Encode(string value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 Hash = SHA256.Create())
        {
            byte[] results = Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            foreach (byte result in results)
            {
                Sb.Append(result.ToString("x2"));
            }
        }

        return Sb.ToString();
    }

    public static Int64 GetTime()
    {
        Int64 Retval = 0;

        DateTime StartDate = new DateTime(1970, 1, 1);
        TimeSpan TSpan = (DateTime.Now.ToUniversalTime() - StartDate);
        Retval = (Int64)(TSpan.TotalMilliseconds + 0.5);

        return Retval;
    }
}
