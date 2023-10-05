﻿using DEH1G0_SOF_2022231.Controllers;
using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using DEH1G0_SOF_2022231.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Tests.BackendTests.TestHelpers;

namespace Tests.BackendTests.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _logger;
        private Mock<SignInManager<AppUser>> _signInManager;
        private Mock<IAppUserRepository> _userRepo;
        private Mock<UserManager<AppUser>> _userManager;
        private Mock<RoleManager<IdentityRole>> _roleManager;
        private HomeController _homeController;
        private string _invalidAppUserId;

        [SetUp]
        public void SetUp()
        {
            this._logger = new Mock<ILogger<HomeController>>();
            this._userManager = MockHelpers.MockUserManager<AppUser>();
            this._signInManager = MockHelpers.MockSignInManager<AppUser>();
            this._userRepo = new Mock<IAppUserRepository>();
            this._roleManager = MockHelpers.MockRoleManager<IdentityRole>();
            this._homeController = new HomeController(_logger.Object, _userRepo.Object, _userManager.Object, _roleManager.Object, _signInManager.Object);
            this._invalidAppUserId = "-------InvalidAppUserId-------";
        }
        // TODO StatusCode check
        [Test]
        public async Task GrantAdminAsync_WhenCalledWithValidUserId_ShouldAddAdminRoleToUser()
        {
            string role = "Admin";
            var user = new AppUser { Id = "1", FirstName = "fName", LastName = "lName" };
            this._userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            this._userManager
                .Setup(x => x.AddToRoleAsync(user, role))
                .ReturnsAsync(IdentityResult.Success);

            var result = await this._homeController.GrantAdminAsync(user.Id);
            
            result.Should().BeOfType<OkResult>();
            this._userManager.Verify(u => u.FindByIdAsync(user.Id), Times.Once);
            this._userManager.Verify(x => x.AddToRoleAsync(user, role), Times.Once);
        }

        [Test]
        public async Task GrantAdminAsync_WhenCalledWithInvalidUserId_ShouldNotAddsAdminRoleToUser()
        {
            this._userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null!);
            
            var result = await this._homeController.GrantAdminAsync(this._invalidAppUserId);

            result.Should().BeOfType<BadRequestObjectResult>();
            this._userManager.Verify(u => u.FindByIdAsync(this._invalidAppUserId), Times.Once);
            this._userManager.Verify(x => x.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task RemoveAdminAsync_WhenCalledWithValidUserId_ShouldRemovesAdminRoleFromUser()
        {
            string role = "Admin";
            var user = new AppUser { Id = "2", FirstName = "fName", LastName = "lName" };
            this._userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            this._userManager
                .Setup(x => x.RemoveFromRoleAsync(user, role))
                .ReturnsAsync(IdentityResult.Success);
            
            var result = await this._homeController.RemoveAdminAsync(user.Id);
            
            result.Should().BeOfType<OkResult>();
            this._userManager.Verify(u => u.FindByIdAsync(user.Id), Times.Once);
            this._userManager.Verify(x => x.RemoveFromRoleAsync(user,role), Times.Once);
        }

        [Test]
        public async Task RemoveAdminAsync_WhenCalledWithInvalidUserId_ShouldNotRemovesAdminRoleFromUser()
        {
            this._userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null!);
            
            var result = await this._homeController.RemoveAdminAsync(this._invalidAppUserId);
            
            result.Should().BeOfType<BadRequestObjectResult>();
            this._userManager.Verify(u => u.FindByIdAsync(this._invalidAppUserId), Times.Once);
            this._userManager.Verify(x => x.RemoveFromRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);
        }
        
        [Test]
        public async Task DeleteUserByAdminAsync_WhenCalledWithValidUserId_ShouldDeletesUserFromRepository()
        {
            var user = new AppUser { Id = "3", FirstName = "fName", LastName = "lName" };
            this._userManager
                .Setup(x=> x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            
            var result = await this._homeController.DeleteUserByAdminAsync(user.Id);
            
            result.Should().BeOfType<OkResult>();
            this._userManager.Verify(x => x.FindByIdAsync(user.Id), Times.Once);
            this._userManager.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteUserByAdminAsync_WhenCalledWithInvalidUserId_ShouldNotDeletesUserFromRepository()
        {
            this._userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null!);
            
            var result = await this._homeController.DeleteUserByAdminAsync(this._invalidAppUserId);
            
            result.Should().BeOfType<BadRequestObjectResult>();
            this._userManager.Verify(x => x.FindByIdAsync(this._invalidAppUserId), Times.Once);
            this._userManager.Verify(x => x.DeleteAsync(It.IsAny<AppUser>()), Times.Never);
        }

        [Test]
        public async Task DeleteUserAsync_WhenCalled_ShouldBeDeletesTheCurrentUserAndLogsOut()
        {
            var user = new AppUser { Id = "4", FirstName = "fName", LastName = "lName" };
            this._userManager
                .Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            this._userManager
                .Setup(x=> x.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(user.Id);
            
            var result = await this._homeController.DeleteUserAsync();
            
            result.Should().BeOfType<OkResult>();
            this._userManager.Verify(x=> x.FindByIdAsync(user.Id), Times.Once);
            this._signInManager.Verify(x => x.SignOutAsync(), Times.Once);
            this._userManager.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public void Constructor_WhenCalledWithNotNullParameters_ShouldInitializesInstanceOfHomeController()
        {
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object,
                this._userManager.Object, this._roleManager.Object, this._signInManager.Object);
            
            action.Should().NotBeNull();
            action.Should().NotThrow();
        }

        [Test]
        public void Constructor_WhenCalledWithNullLogger_ThrowsArgumentNullException()
        {
            string expectedNullParameterName = "logger";
            string expectedExceptionMessageStart = "Value cannot be null.*";
            
            Action action = () => new HomeController(null, this._userRepo.Object, this._userManager.Object,
                this._roleManager.Object, this._signInManager.Object);
            
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public void Constructor_WhenCalledWithNullUserRepository_ThrowsArgumentNullException()
        {
            string expectedNullParameterName = "userRepository";
            string expectedExceptionMessageStart = "Value cannot be null.*";
            
            Action action = () => new HomeController(this._logger.Object, null, this._userManager.Object,
                this._roleManager.Object, this._signInManager.Object);
            
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public void Constructor_WhenCalledWithNullUserManager_ShouldThrowsArgumentNullException()
        {
            string expectedNullParameterName = "userManager";
            string expectedExceptionMessageStart = "Value cannot be null.*";
            
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object,
                null, this._roleManager.Object, this._signInManager.Object);
            
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public void Constructor_WhenCalledWithNullRoleManager_ShouldThrowsArgumentNullException()
        {
            string expectedNullParameterName = "roleManager";
            string expectedExceptionMessageStart = "Value cannot be null.*";
            
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object,
                this._userManager.Object, null, this._signInManager.Object);
            
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);

        }

        [Test]
        public void Constructor_WhenCalledWithNullSignInManager_ShouldThrowsArgumentNullException()
        {
            string expectedNullParameterName = "signInManager";
            string expectedExceptionMessageStart = "Value cannot be null.*";
            
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object, this._userManager.Object, this._roleManager.Object, null);
            
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public async Task ListUsers_WhenCalled_ShouldReturnAllUsers()
        {
            var users = new List<AppUser>
            {
                new AppUser { Id = "1", FirstName = "fName", LastName = "lName" },
                new AppUser { Id = "2", FirstName = "fName2", LastName = "lName2" }
            };

            this._userRepo
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);
            this._userManager
                .Setup(x => x.GetRolesAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(new List<string>());
            var result = await this._homeController.ListUsersAsync();
            
            var objectResult = result.Result as OkObjectResult;
            objectResult.Should().NotBeNull();

            var userDTOs = objectResult.Value as List<BasicUserInfosDTO>;
            userDTOs.Should().HaveCount(2);
            userDTOs.Should().BeEquivalentTo(users.Select(user => new BasicUserInfosDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = new List<string>()
            }));
        }

        [Test]
        public async Task GrantAdminAsync_WhenAnErrorOccurs_ShouldReturnInternalServerError()
        {
            var user = new AppUser { Id = "1231234", FirstName = "fName", LastName = "lName" };
            string exceptionMessage = "um error";
            Exception exception = new Exception(exceptionMessage);
            this._userManager
                .Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            
            this._userManager
                .Setup(rm => rm.AddToRoleAsync(It.IsAny<AppUser>(),It.IsAny<string>()))
                .Throws(exception);
            
            var result = await this._homeController.GrantAdminAsync(user.Id);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.ToString().Should().Contain(exceptionMessage);
        }
        
        [Test]
        public async Task DeleteUserByAdminAsync_WhenAnErrorOccurs_ShouldReturnInternalServerError()
        {
            var user = new AppUser { Id = "1231234", FirstName = "fName", LastName = "lName" };
            string exceptionMessage = "um error";
            Exception exception = new Exception(exceptionMessage);
            this._userManager
                .Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            
            this._userManager
                .Setup(rm => rm.DeleteAsync(It.IsAny<AppUser>()))
                .Throws(exception);
            
            var result = await this._homeController.DeleteUserByAdminAsync(user.Id);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.ToString().Should().Contain(exceptionMessage);
        }
        
        [Test]
        public async Task RemoveAdminAsync_WhenAnErrorOccurs_ShouldReturnInternalServerError()
        {
            var user = new AppUser { Id = "1231234", FirstName = "fName", LastName = "lName" };
            string exceptionMessage = "um error";
            Exception exception = new Exception(exceptionMessage);
            this._userManager
                .Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            
            this._userManager
                .Setup(rm => rm.RemoveFromRoleAsync(It.IsAny<AppUser>(),It.IsAny<string>()))
                .Throws(exception);
            
            var result = await this._homeController.RemoveAdminAsync(user.Id);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.ToString().Should().Contain(exceptionMessage);
        }

        [Test]
        public async Task ListUsersAsync_WhenAnErrorOccurs_ShouldReturnInternalServerError()
        {
            string exceptionMessage = "ur error";
            Exception exception = new Exception(exceptionMessage);
          
            this._userRepo
                .Setup(ur => ur.GetAllAsync())
                .Throws(exception);
            
            var result = await this._homeController.ListUsersAsync();

            var objectResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.ToString().Should().Contain(exceptionMessage);
        }
        [Test]
        public async Task DeleteUserAsync_WhenCalledAndIdErrorOccurs_ShouldReturnBadRequest()
        {
            this._userManager
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null!);
            
            var result = await this._homeController.DeleteUserAsync();

            result.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Test]
        public async Task DeleteUserAsync_WhenAnErrorOccurs_ShouldReturnInternalServerError()
        {
            var user = new AppUser { Id = "1231234", FirstName = "fName", LastName = "lName" };
            string exceptionMessage = "ur error";
            Exception exception = new Exception(exceptionMessage);
            
            this._userManager
                .Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            this._userManager
                .Setup(x=> x.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(user.Id);
            this._signInManager
                .Setup(sim => sim.SignOutAsync())
                .Throws(exception);
            
            var result = await this._homeController.DeleteUserAsync();

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            objectResult.Value.ToString().Should().Contain(exceptionMessage);
        }
        
        [Theory]
        [TestCase("ListUsersAsync")]
        [TestCase("DeleteUserAsync")]
        [TestCase("DeleteUserByAdminAsync")]
        [TestCase("GrantAdminAsync")]
        [TestCase("RemoveAdminAsync")]
        [TestCase("GrantAdminAsync")]
        public void HomeController_MethodWithAuthorizeAttribute_ShouldReturnTrue(string methodName)
        {
            bool result = AttributeHelper.MethodHasAttributeOfType<HomeController, AuthorizeAttribute>(methodName);
            
            result.Should().BeTrue();
        }

        [Theory]
        [TestCase("DeleteUserByAdminAsync")]
        [TestCase("GrantAdminAsync")]
        [TestCase("RemoveAdminAsync")]
        public void HomeController_MethodWithAuthorizeAttributeWhichContainsAdminRole_ShouldReturnTrue(string methodName )
        {
            string attributePropertyName = "Roles";
            string expectedPropertyValue = "Admin";

            bool result = AttributeHelper
                .MethodHasAttributeWithPropertyValue<HomeController, AuthorizeAttribute, string>
                    (methodName, attributePropertyName, expectedPropertyValue);
            
            result.Should().BeTrue();
        }
    }
}
