using DEH1G0_SOF_2022231.Controllers;
using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Tests.BackendTests.UnitTests.Controllers
{
    [TestFixture]
    public class LogControllerTests
    {
        private LogController _logController;
        private Mock<ITorrentLogRepository> _torrentLogRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _torrentLogRepositoryMock = new Mock<ITorrentLogRepository>();
            _logController = new LogController(_torrentLogRepositoryMock.Object);
        }

        [Test]
        public async Task GetLogs_WhenCalled_ShouldReturnAllTorrentLogs()
        {
            var expectedTorrentLogs = new List<TorrentLog>
            {
                new TorrentLog { Id = "TorrentLogId_1", TorrentId = "TorrentId_1", Created = new DateTime(2022, 1, 1) },
                new TorrentLog { Id = "TorrentLogId_2", TorrentId = "TorrentId_2", Created = new DateTime(2022, 2, 2) }
            };
            this._torrentLogRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(expectedTorrentLogs);

            var actionResult = await this._logController.GetLogs();

            var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedTorrentLogs = okResult.Value.Should().BeAssignableTo<IEnumerable<TorrentLog>>().Subject;
            returnedTorrentLogs.Should().BeEquivalentTo(expectedTorrentLogs);
        }

        [Test]
        public async Task GetLogs_WhenAnErrorOccurs_ShouldReturnInternalServerError()
        {
            this._torrentLogRepositoryMock.Setup(repo => repo.GetAllAsync()).Throws(new Exception("Some exception"));

            var result = await this._logController.GetLogs();

            var objectResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public void Constructor_WhenCalledWithNullParameter_ShouldThrowsArgumentNullException()
        {
            string expectedNullParameterName = "torrentLogRepository";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            Action act = () => new LogController(null);

            act.Should().NotBeNull();
            act.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public void Constructor_WhenCalled_InitializesInstanceOfLogController()
        {
            Action action = () => new LogController(_torrentLogRepositoryMock.Object);

            action.Should().NotBeNull();
            action.Should().NotThrow();
        }

    }

}
