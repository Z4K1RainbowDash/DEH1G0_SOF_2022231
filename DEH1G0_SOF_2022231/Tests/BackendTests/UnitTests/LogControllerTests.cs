using DEH1G0_SOF_2022231.Controllers;
using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.BackendTests.UnitTests
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

        // TODO rewrite

        /*
        [Test]
        public async Task GetLogs_NoArgs_ShouldReturnsAllTorrentLogs()
        {
            // Arrange
            var expectedTorrentLogs = new List<TorrentLog>
            {
                new TorrentLog { Id = "TorrentLogId_1", TorrentId = "TorrentId_1", Created = new DateTime(2022, 1, 1) },
                new TorrentLog { Id = "TorrentLogId_2", TorrentId = "TorrentId_2", Created = new DateTime(2022, 2, 2) }
            };
            this._torrentLogRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedTorrentLogs);

            // Act
            var result = await this._logController.GetLogs();

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(expectedTorrentLogs)
                .And.BeOfType<List<TorrentLog>>();
           
        }

        [Test]
        public async Task GetLogs_NoArgs_ShouldReturnsEmptyList()
        {
            // Arrange
            var expectedTorrentLogs = new List<TorrentLog>();

            this._torrentLogRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedTorrentLogs);

            //Act
            var result = await this._logController.GetLogs();

            //Assert
            result.Should()
                .BeEmpty()
                .And.BeOfType<List<TorrentLog>>();
        }
        
        [Test]
        public void Constructor_WhenCalledWithNullParameter_ShouldThrowsArgumentNullException()
        {
            // Arrange
            string expectedNullParameterName = "torrentLogRepository";
            string expectedExceptionMessageStart = "Value cannot be null.*";

            
            // Act
            Action act = () => new LogController(null);

            // Assert
            act.Should().NotBeNull();
            act.Should().Throw<ArgumentNullException>()
                .WithMessage(expectedExceptionMessageStart)
                .WithParameterName(expectedNullParameterName);
        }

        [Test]
        public void Constructor_WhenCalled_InitializesInstanceOfLogController()
        {

            // Arrange + Act
            Action action = () => new LogController(_torrentLogRepositoryMock.Object);


            // Assert
            action.Should().NotBeNull();
            action.Should().NotThrow();
        }
        */
    }

}
