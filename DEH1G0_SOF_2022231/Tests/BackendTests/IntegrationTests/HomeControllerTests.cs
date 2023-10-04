using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Tests.BackendTests.TestHelpers;

namespace Tests.BackendTests.IntegrationTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private TestingWebAppFactory<Program> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = new TestingWebAppFactory<Program>();
            _client = _factory.CreateClient();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Theory]
        [TestCase("/")]
        [TestCase("Home/Privacy")]
        public async Task GetEndpoints_WhenCalledWithValidUrl_ShouldReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString().Should().Be("text/html; charset=utf-8");
        }

        [Test]
        [TestCase("/Home/ListUsers")]
        public async Task GetEndpoints_WhenCalledWithValidUrl_SecurePageRedirectsAnUnauthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.OriginalString.Should()
                .StartWith("http://localhost/Identity/Account/Login");
            
        }

        
    }
}
