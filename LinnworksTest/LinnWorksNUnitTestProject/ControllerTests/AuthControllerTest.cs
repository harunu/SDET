using LinnworksTest.Controllers;
using LinnworksTest.DataAccess;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using static LinnworksTest.Controllers.AuthController;

namespace LinnWorksNUnitTestProject.ControllerTests
{
    public class AuthControllerTest
    {
        private AuthController _authController;
        private readonly Mock<ITokenRepository> _mockTokenRepository;

        private readonly AuthController.Account _account = new AuthController.Account
        {
            Token = Guid.NewGuid().ToString()
        };
        public AuthControllerTest()
        {
            _mockTokenRepository = new Mock<ITokenRepository>();
        }

        [Test]
        public async Task Login_WhenTokenIsNotPresentInDb_ReturnsBadRequest()
        {
            // ARRANGE
            _mockTokenRepository
                .Setup(r => r.IsValidTokenAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            _authController = new AuthController(_mockTokenRepository.Object);

            // ACT
            var result = await _authController.Login(_account);

            // ASSERT
            AssertTokenIsInvalid(result);
        }

        [Test]
        public async Task Login_AccountValid_ReturnsOkObject()
        {
            // ARRANGE
            var configuredAuthController = ConfigureAuthController();

            // ACT
            var result = await configuredAuthController.Login(_account);

            // ASSERT
            configuredAuthController.ModelState.IsValid.Should()
                .BeTrue();
            configuredAuthController.ModelState.ErrorCount.Should()
                .Be(0);
            result.Should()
                .BeOfType<OkObjectResult>();
        }

        private AuthController ConfigureAuthController()
        {
            var httpContextMock = new Mock<HttpContext>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var authenticationServiceMock = new Mock<IAuthenticationService>();

            httpContextMock.Setup(c => c.RequestServices)
                .Returns(serviceProviderMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);
            authenticationServiceMock.Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);
            _mockTokenRepository
                .Setup(r => r.IsValidTokenAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            return new AuthController(_mockTokenRepository.Object) { ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object } };
        }

        private void AssertTokenIsInvalid(IActionResult result)
        {
            _authController.ModelState.IsValid.Should()
                .BeFalse();
            _authController.ModelState.ErrorCount.Should()
                .Be(1);
            _authController.ModelState["login_failure"].Errors.Should()
                .Contain(e => e.ErrorMessage.Equals("Invalid token."));
            result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockTokenRepository.Reset();
        }


        [Test]
        public async Task Login_ReturnBadRequest_WhenAccount_DoesNotExist()
        {
            // .Arrange
            _mockTokenRepository.Setup(r => r.IsValidTokenAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));
            var notExistingAccount = new LinnworksTest.Controllers.AuthController.Account() { Token = Guid.NewGuid().ToString() };

            _authController = new AuthController(_mockTokenRepository.Object);

            // .Act
            var result = await _authController.Login(notExistingAccount);

            // .Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

    }
}
