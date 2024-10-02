using Application.Interfaces;
using Application.Interfaces.Repositories;
using Castle.Core.Configuration;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace Infrastructure.UnitTest.RepositoriesUnitTest
{
    public class AuthenUnitTest
    {
        private const string SecretKey = "QXV0aGVudGljYXRlQXBpQXBwbGljYXRpb246OkFwaVNlY3JldEAyMDI0;";
        private const string Issuer = "AuthenticateApiUnitTest::IssuerUnitTest";
        private const string Audience = "AuthenticateApiUnitTest::AudienceUnitTest";

        private readonly DbContextOptions<WorkflowDbContext> _dbContextOptions;
        private readonly WorkflowDbContext _context;

        public AuthenUnitTest()
        {
             var _configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            _dbContextOptions = new DbContextOptionsBuilder<WorkflowDbContext>()
            .UseInMemoryDatabase(databaseName: "UserAuthenTokenTest").Options;
            _context = new WorkflowDbContext(_dbContextOptions);
            UserAuthenRespository _repo = new UserAuthenRespository(_context, _configuration);
        }

        [Test]
        public void Test()
        {
            int numerator = 10;
            int denominator = 0;

            // Act and Assert
            var ex = Assert.Throws<DivideByZeroException>(() =>
            {
                int result = numerator / denominator;
            });
            // Optionally, verify the exception message or other properties
            Assert.AreEqual("Attempted to divide by zero.", ex.Message);
        }

        [Test]
        public void AuthenTest()
        {
           
        }
    }
}
