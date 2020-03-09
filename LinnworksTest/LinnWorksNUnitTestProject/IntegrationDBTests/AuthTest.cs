using NUnit.Framework;
using LinnworksTest.DataAccess;
using System;
using System.Threading.Tasks;
//using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using LinnWorksNUnitTestProject.IntegrationDBTests;


namespace LinnWorksNUnitTestProject
{
    [TestFixture]
    public class AuthTest : IntegrationBase
    {
        private ITokenRepository _tokenRepository;
        private System.Guid _token;

     /*   [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _dbtest.CleanTable("Tokens");
        }*/

        [SetUp]
        public void Setup()
        {
            _tokenRepository = DBStartup.ConfigureServiceProvider().GetRequiredService<ITokenRepository>();
        }

        [Test]
        public async Task IsValidTokenAsync_WhenTokenIsPresentInDB_ReturnsTrue()
        {
            //ARRANGE
            _token = Guid.NewGuid();
            _dbtest.AddToken(_token);

            // ACT
            var isTokenValid = await _tokenRepository.IsValidTokenAsync(_token);

            // ASSERT
            Assert.IsNotNull(isTokenValid);
        }

        [Test]
        public async Task IsValidTokenAsync_WhenTokenIsNotPresentInDB_ReturnsFalse()
        {
            // ACT
            var isTokenValid = await _tokenRepository.IsValidTokenAsync(Guid.NewGuid());

            // ASSERT
            isTokenValid.Should().BeFalse();
        }

        [TearDown]
        public void Teardown()
        {
            if (_token != Guid.Empty)
            {
                _dbtest.RemoveTokenByValue(_token);
            }
        }
    }

}