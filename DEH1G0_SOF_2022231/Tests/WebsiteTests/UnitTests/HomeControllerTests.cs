using DEH1G0_SOF_2022231.Controllers;
using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Security.Claims;
using Tests.WebsiteTests.TestHelpers;

namespace Tests.WebsiteTests.UnitTests
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

        // TODO rewrite

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<HomeController>>();
            _userManager = MockHelpers.MockUserManager<AppUser>();
            _signInManager = MockHelpers.MockSignInManager<AppUser>();
            _userRepo = new Mock<IAppUserRepository>();
            _roleManager = MockHelpers.MockRoleManager<IdentityRole>();
            _homeController = new HomeController(_logger.Object, _userRepo.Object, _userManager.Object, _roleManager.Object, _signInManager.Object);
            _invalidAppUserId = "-------InvalidAppUserId-------";
        }

       

        /*

        [Test]
        public async Task GrantAdmin_WhenCalledWithValidUserId_ShouldsAddsAdminRoleToUser()
        {
            // Arrange
            string role = "Admin";
            var user = new AppUser { Id = "1", FirstName = "fName", LastName = "lName" };
            this._userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            this._userManager.Setup(x => x.AddToRoleAsync(user, role)).ReturnsAsync(IdentityResult.Success);

            // Act
            this._homeController.
            var result = await this._homeController.GrantAdmin(user.Id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.ListUsers));
            this._userManager.Verify(u => u.FindByIdAsync(user.Id), Times.Once);
            this._userManager.Verify(x => x.AddToRoleAsync(user, role), Times.Once);

            
        }

        [Test]
        public async Task GrantAdmin_WhenCalledWithInvalidUserId_ShouldNotAddsAdminRoleToUser()
        {
            // Arrange
            AppUser user = (AppUser)null;
            this._userManager.Setup(x => x.FindByIdAsync(this._invalidAppUserId)).ReturnsAsync(user);
            

            // Act
            var result = await this._homeController.GrantAdmin(this._invalidAppUserId);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.ListUsers));
            this._userManager.Verify(u => u.FindByIdAsync(this._invalidAppUserId), Times.Once);
            this._userManager.Verify(x => x.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);
            
        }

        [Test]
        public async Task RemoveAdmin_WhenCalledWithValidUserId_ShouldRemovesAdminRoleFromUser()
        {
            // Arrange
            string role = "Admin";
            var user = new AppUser { Id = "2", FirstName = "fName", LastName = "lName" };
            this._userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            this._userManager.Setup(x => x.RemoveFromRoleAsync(user, role)).ReturnsAsync(IdentityResult.Success);

            //Act
            var result = await this._homeController.RemoveAdmin(user.Id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.ListUsers));
            this._userManager.Verify(u => u.FindByIdAsync(user.Id), Times.Once);
            this._userManager.Verify(x => x.RemoveFromRoleAsync(user,role), Times.Once);
        }

        [Test]
        public async Task RemoveAdmin_WhenCalledWithInvalidUserId_ShouldNotRemovesAdminRoleFromUser()
        {
            // Arrange
            AppUser user = (AppUser)null;
            this._userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            
            // Act
            var result = await this._homeController.RemoveAdmin(this._invalidAppUserId);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.ListUsers));
            this._userManager.Verify(u => u.FindByIdAsync(this._invalidAppUserId), Times.Once);
            this._userManager.Verify(x => x.RemoveFromRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Never);
        }


        [Test]
        public async Task DeleteUserByAdmin_WhenCalledWithValidUserId_ShouldDeletesUserFromRepository()
        {
            // Arrange
            var user = new AppUser { Id = "3", FirstName = "fName", LastName = "lName" };
            this._userManager.Setup(x=> x.FindByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await this._homeController.DeleteUserByAdmin(user.Id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.ListUsers));
            this._userManager.Verify(x => x.FindByIdAsync(user.Id), Times.Once);
            this._userManager.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteUserByAdmin_WhenCalledWithInvalidUserId_ShouldNotDeletesUserFromRepository()
        {
            // Arrange
            AppUser user = (AppUser)null;
            this._userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await this._homeController.DeleteUserByAdmin(this._invalidAppUserId);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.ListUsers));
            this._userManager.Verify(x => x.FindByIdAsync(this._invalidAppUserId), Times.Once);
            this._userManager.Verify(x => x.DeleteAsync(It.IsAny<AppUser>()), Times.Never);
        }

        [Test]
        public async Task DeleteUser_WhenCalled_ShouldBeDeletesTheCurrentUserAndLogsOut()
        {
            // Arrange
            var user = new AppUser { Id = "4", FirstName = "fName", LastName = "lName" };

            this._userManager.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);
            this._userManager.Setup(x=> x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(user.Id);

            // Act

            var result = await this._homeController.DeleteUser();

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(_homeController.Index));
            this._userManager.Verify(x=> x.FindByIdAsync(user.Id), Times.Once);
            this._signInManager.Verify(x => x.SignOutAsync(), Times.Once);
            this._userManager.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public void Constructor_WhenCalled_InitializesInstanceOfHomeController()
        {
            // Arrange + Act
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object, this._userManager.Object, this._roleManager.Object, this._signInManager.Object);



            // Assert
            action.Should().NotBeNull();
            action.Should().NotThrow();
        }

        [Test]
        public void Constructor_WhenCalledWithNullLogger_ThrowsArgumentNullException()
        {
            // Arrange
            string expectedNullParameterName = "logger";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            // Act
            Action action = () => new HomeController(null, this._userRepo.Object, this._userManager.Object, this._roleManager.Object, this._signInManager.Object);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public void Constructor_WhenCalledWithNullUserRepository_ThrowsArgumentNullException()
        {
            // Arrange
            string expectedNullParameterName = "userRepository";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            // Act
            Action action = () => new HomeController(this._logger.Object, null, this._userManager.Object, this._roleManager.Object, this._signInManager.Object);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);

        }

        [Test]
        public void Constructor_WhenCalledWithNullUserManager_ShouldThrowsArgumentNullException()
        {


            // Arrange
            string expectedNullParameterName = "userManager";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            // Act
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object, null, this._roleManager.Object, this._signInManager.Object);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);

        }

        [Test]
        public void Constructor_WhenCalledWithNullRoleManager_ShouldThrowsArgumentNullException()
        {
            // Arrange
            string expectedNullParameterName = "roleManager";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            // Act
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object, this._userManager.Object, null, this._signInManager.Object);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);

        }

        [Test]
        public void Constructor_WhenCalledWithNullSignInManager_ShouldThrowsArgumentNullException()
        {
            // Arrange
            string expectedNullParameterName = "signInManager";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            // Act
            Action action = () => new HomeController(this._logger.Object, this._userRepo.Object, this._userManager.Object, this._roleManager.Object, null);

            // Assert
            action.Should().NotBeNull();
            action.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);

        }

        [Test]
        public async Task ListUsers_WhenCalled_ShouldReturnAllUsers()
        {
            // Arrange

            var user1 = new AppUser { Id = "1", FirstName = "fName", LastName = "lName" };
            var user2 = new AppUser { Id = "2", FirstName = "fName2", LastName = "lName2" };
            var users = new List<AppUser> { user1, user2 };

            this._userRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            // Act

            var result = await this._homeController.ListUsers();

            // Assert

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().Be(users);
        }

        [Theory]
        [TestCase("ListUsers")]
        [TestCase("DeleteUser")]
        [TestCase("DeleteUserByAdmin")]
        [TestCase("GrantAdmin")]
        [TestCase("RemoveAdmin")]
        [TestCase("GrantAdmin")]
        public void HomeController_MethodWithAuthorizeAttribute_ShouldReturnTrue(string methodName)
        {
           
            // Act
            bool result = AttributeHelper.MethodHasAttributeOfType<HomeController, AuthorizeAttribute>(methodName);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [TestCase("DeleteUserByAdmin")]
        [TestCase("GrantAdmin")]
        [TestCase("RemoveAdmin")]
        public void HomeController_MethodWithAuthorizeAttributeWhichContainsAdminRole_ShouldReturnTrue(string methodName )
        {
            // Arrange
            string attributePropertyName = "Roles";

            string expectedPropertyValue = "Admin";
            
            // Act
             bool result = AttributeHelper.MethodHasAttributeWithPropertyValue<HomeController, AuthorizeAttribute,String>(methodName, attributePropertyName, expectedPropertyValue);

            // Assert
            result.Should().BeTrue();
        }


        */
    }
}
