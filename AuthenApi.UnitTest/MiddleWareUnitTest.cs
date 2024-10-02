using AuthenApi.UnitTest.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;
using UserAuthenApi.Common;



namespace AuthenApi.UnitTest
{
    using static SignaturesHelpers;
    public class MiddleWareUnitTest
    {
        private readonly SignatureHeader _signature;
        private readonly IConfiguration _configuration;
        public MiddleWareUnitTest()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            RequestDelegate next = (HttpContext hc) => Task.CompletedTask;
            _signature = new SignatureHeader(next, _configuration);
        }
        [Fact]
        public async Task SignatureHeaderUnitTest()
        {
            // Arrange

            string ApiKey = _configuration["JwtConfig:Key"];
            string Secret = _configuration["JwtConfig:Secret"];
            Double UTCDate = Math.Floor(Convert.ToDouble(GetTime() / ((1000 * 60) * 8)));
            string Encode = SHA256Encode(ApiKey + Secret + UTCDate);

            HttpContext ctx = new DefaultHttpContext();
            var headers = new Dictionary<string, string>
            {
                { "ApiKey", ApiKey },
                { "Signature", Encode }
            };
            foreach (var header in headers)
            {
                ctx.Request.Headers.Add(header.Key, header.Value);
            }

            // Act 

            await _signature.Invoke(ctx);

            // Assert

            Assert.NotEqual(ctx.Response?.StatusCode, StatusCodes.Status406NotAcceptable);
            Assert.Equal(ctx.Response?.StatusCode, StatusCodes.Status200OK);

        }
    }
}