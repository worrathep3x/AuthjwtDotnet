using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Infrastructures.UnitTest
{
    public class AuthenRepositoryUnittest
    {
        private readonly DbContextOptions<EmployeeDbContext> _dbContextOptions;
        private readonly EmployeeDbContext _context;
        private readonly UserAuthenRespository _repo;
        private readonly IConfiguration _configuration;
        private readonly HttpContextAccessor httpContextAccessor;
        public AuthenRepositoryUnittest()
        {
             _configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            _dbContextOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
            .UseInMemoryDatabase(databaseName: "UserAuthenTokenTest").Options;
            _context = new EmployeeDbContext(_dbContextOptions);
            _repo = new UserAuthenRespository(_context, _configuration, httpContextAccessor);

            DataTesting();
        }

        private void DataTesting()
        {
            //_context.UserAccounts.RemoveRange(_context.UserAccounts);
/*            _context.Users.AddRange(new List<UserAccount> {
                new UserAccount { Login = "test1", Pw = "password1", UserId = "Code1", UserName = "Name1", UserGroup = "Group1", HomePage = "Home1", Duty = "Duty1", ClientId = "ClientId1", Branch = "Branch1", RoleId = "RoleID1", StartDate = DateTime.Now, EndDate = DateTime.Now , SysDate = DateTime.Now},
                new UserAccount { Login = "test2", Pw = "password2", UserId = "Code2", UserName = "Name2", UserGroup = "Group1", HomePage = "Home2", Duty = "Duty1", ClientId = "ClientId1", Branch = "Branch2", RoleId = "RoleID2", StartDate = DateTime.Now, EndDate = DateTime.Now, SysDate = DateTime.Now}
            });*/
            _context.SaveChangesAsync();

        }

        [Fact]
        public async Task AccessTokenUnitTest()
        {
            // Arrange
            var Data = _context.Users.First();

            // Act
            var result = await _repo.LoginAccessTokenAsync(Data.AdminEmail, Data.AdminPass);

            // Assert
            Assert.NotNull(result.Token);
            Assert.Equal(Data.Fname, result.Username);

        }

        [Fact]
        public async Task VerifyTokenUnitTest()
        {
            // Arrange
            var Data = _context.Users.First();
            var MockTokenData = await _repo.LoginAccessTokenAsync(Data.AdminEmail, Data.AdminPass);

            // Act
            var result = _repo.VerifyToken(MockTokenData.Token);

            // Assert
            Assert.NotNull(MockTokenData);
            Assert.Equal(Data.Fname, result.Username);

        }

/*        [Fact]
        public async Task RefreshTokenUnitTest()
        {
            // Arrange
            var Data =  _context.UserAccounts.First();
            var claims = new[]
            {
                new Claim("UserID", Data.UserId),
                new Claim("Username", Data.UserName),
                new Claim("Actor", Data.Duty),
                new Claim("Group", Data.UserGroup)
            };
            var MockHttpContext  = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(claims))
            };

            // Act
            var result = _repo.RefreshToken(MockHttpContext);

            // Assert
            Assert.NotNull(result.Token);
            Assert.Equal(Data.UserId, result.UserId);

        }*/

    }
}
