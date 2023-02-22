using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.WebsiteTests.TestHelpers;

namespace Tests.WebsiteTests.IntegrationTests
{
    internal class TorrentsControllerTests
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
        [TestCase("/Torrents/MostActiveUsersByDownloads")]
        [TestCase("/Torrents/ListLogs")]
        [TestCase("/Torrents/SearchTorrent")] 
        [TestCase("/Torrents/MostPopularTorrents")]
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
