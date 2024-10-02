namespace Infrastructure.UnitTest;

using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        var _configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        DbContextOptions<WorkflowDbContext> _dbContextOptions = new DbContextOptionsBuilder<WorkflowDbContext>()
        .UseInMemoryDatabase(databaseName: "UserAuthenTokenTest").Options;
        WorkflowDbContext _context = new WorkflowDbContext(_dbContextOptions);
        UserAuthenRespository _repo = new UserAuthenRespository(_context, _configuration);

        DataTesting(_context);
    }

    private void DataTesting(WorkflowDbContext dbContext)
    {
        dbContext.UserAccounts.RemoveRange(dbContext.UserAccounts);
        dbContext.UserAccounts.AddRange(new List<UserAccount> {
                new UserAccount { Login = "test1", Pw = "password1", UserId = "Code1", UserName = "Name1", UserGroup = "Group1", HomePage = "Home1", Duty = "Duty1", ClientId = "ClientId1", Branch = "Branch1", RoleId = "RoleID1", StartDate = DateTime.Now, EndDate = DateTime.Now , SysDate = DateTime.Now},
                new UserAccount { Login = "test2", Pw = "password2", UserId = "Code2", UserName = "Name2", UserGroup = "Group1", HomePage = "Home2", Duty = "Duty1", ClientId = "ClientId1", Branch = "Branch2", RoleId = "RoleID2", StartDate = DateTime.Now, EndDate = DateTime.Now, SysDate = DateTime.Now}
            });
    }
}
