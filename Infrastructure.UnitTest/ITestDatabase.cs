using System.Data.Common;

namespace Infrastructure.UnitTest;

internal interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
