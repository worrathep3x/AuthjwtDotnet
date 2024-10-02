using Application.Model;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace UserAuthenApi.Common
{
    public class SignatureHeader
    {
        private readonly RequestDelegate _next;
        private IConfiguration _config;
        public SignatureHeader(RequestDelegate next, IConfiguration config)
        {
            this._next = next;
            this._config = config;
        }
        public async Task Invoke(HttpContext context)
        {
            if (IgnoreRoute(context))
            {
                await this._next(context);
            }
            else
            {
                bool signature = await Signature(context);

                if (!signature) return;

                await this._next(context);
            }
        }
        public async Task<bool> Signature(HttpContext context)
        {
            string apiKey = context.Request.Headers["ApiKey"];
            string signature = context.Request.Headers["Signature"];

            bool allow = true;

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(signature)) allow = false;

            string securityApiKey = this._config["JwtConfig:Key"];
            string securitySecret = this._config["JwtConfig:Secret"];

            if (!securityApiKey.Equals(apiKey)) allow = false;

            Double UTCDate = Math.Floor(Convert.ToDouble(GetTime() / ((1000 * 60) * 8)));

            string encryption = SHA256Encode(securityApiKey + securitySecret + UTCDate);

            if (!encryption.Equals(signature)) allow = false;

            if (!allow)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                string MakeResponse = JsonConvert.SerializeObject(Response<string>.FailedResult("Invalid Signature"));
                await context.Response.WriteAsync(MakeResponse, Encoding.UTF8);
            }

            return allow;
        }
        private Int64 GetTime()
        {
            Int64 Retval = 0;

            DateTime StartDate = new DateTime(1970, 1, 1);
            TimeSpan TSpan = (DateTime.Now.ToUniversalTime() - StartDate);
            Retval = (Int64)(TSpan.TotalMilliseconds + 0.5);

            return Retval;
        }
        private bool IgnoreRoute(HttpContext context)
        {
            string reqPath = context.Request.Path;

            List<string> paths = new List<string>();
            paths.Add("/swagger");
            paths.Add("/notify");
            paths.Add("/version");
            //paths.Add("/useracc/getuser");
            //paths.Add("/notify/negotiate");

            int index = -1;
            for (int idx = 0; idx < paths.Count; idx++)
            {
                index = reqPath.ToLower().IndexOf(paths[idx]);
                if (index >= 0) break;
            }

            return (index >= 0);
        }
        private static string SHA256Encode(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 Hash = SHA256.Create())
            {
                byte[] results = Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                foreach (Byte result in results)
                {
                    Sb.Append(result.ToString("x2"));
                }
            }

            return Sb.ToString();
        }
    }
}
