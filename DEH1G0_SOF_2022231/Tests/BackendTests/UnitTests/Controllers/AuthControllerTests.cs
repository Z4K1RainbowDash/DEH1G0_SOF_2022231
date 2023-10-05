using DEH1G0_SOF_2022231.Controllers;
using DEH1G0_SOF_2022231.Models;
using DEH1G0_SOF_2022231.Models.Auth;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Tests.BackendTests.TestHelpers;

namespace Tests.BackendTests.UnitTests.Controllers;

[TestFixture]
public class AuthControllerTests
{
    
    private Mock<UserManager<AppUser>> _userManager;
    private AuthController _authController;
    
    [SetUp]
    public void SetUp()
    {
        this._userManager = MockHelpers.MockUserManager<AppUser>();
        this._authController = new AuthController(this._userManager.Object);
    }

    [Test]
    public async Task Register_WhenCalledWithValidResisterModel_ShouldReturnOkResult()
    {
        RegisterModel rm = new RegisterModel()
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "email@email.email",
            Password = "Password123",
            Username = "Username"
        };
        this._userManager
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var result = await this._authController.Register(rm);
        
        result.Should().BeOfType<OkResult>();

    }

    [Test]
    public async Task Register_WithExistingUserData_ShouldReturnProblem()
    {
        bool firstAttempt = true;
        RegisterModel rm = new RegisterModel()
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "email@email.email",
            Password = "Password123",
            Username = "Username"
        };
        this._userManager
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync(() =>
            {
                var result = firstAttempt 
                    ? IdentityResult.Success 
                    : IdentityResult.Failed(new IdentityError { Description = "User already exists." });
                firstAttempt = false;
                return result;
            });

        var result = await this._authController.Register(rm);
        var result2 = await this._authController.Register(rm);
        
        result.Should().BeOfType<OkResult>();
        var objectResult = result2.Should().BeOfType<ObjectResult>().Subject;
        var problemDetails = objectResult.Value.As<ProblemDetails>();
        problemDetails.Should().NotBeNull();
        problemDetails.Detail.Should().Be("Duplicated User");
    }
}