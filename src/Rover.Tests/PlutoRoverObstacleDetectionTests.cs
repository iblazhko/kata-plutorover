using FluentAssertions;
using Optional;
using Rover.Library;
using Xunit;

namespace Rover.Tests
{
    public class PlutoRoverObstacleDetectionTests
    {
        [Fact]
        public void When_ThereIsAnObstacleInTheWay_Expect_StopAtTheObstacle()
        {
            // Arrange
            var pluto = new Pluto(100, 100, new [] { new Location(0, 2) });
            var rover = new PlutoRover(pluto, new Position(new Location(0, 0), Orientation.N));

            // Act
            rover.Move("FF");

            // Assert
            rover.Position.ToString()
                .Should().Be("0,1,N");
        }

        [Fact]
        public void When_ThereIsAnObstacleInTheWay_Expect_ReportTheObstacleLocation()
        {
            // Arrange
            var obstacleLocation = new Location(0, 2);
            var pluto = new Pluto(100, 100, new [] { obstacleLocation });
            var rover = new PlutoRover(pluto, new Position(new Location(0, 0), Orientation.N));

            // Act
            rover.Move("FF");

            // Assert
            // Note: not using Fluent assertion (.Should().Be(...)) - it is not compatible with the Option type
            Assert.Equal(rover.ObstacleInTheWay, Option.Some(obstacleLocation));
        }

        [Fact]
        public void When_ThereAreNoObstaclesInTheWay_Expect_ReportNone()
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position(new Location(0, 0), Orientation.N));

            // Act
            rover.Move("FF");

            // Assert
            // Note: not using Fluent assertion (.Should().Be(...)) - it is not compatible with the Option type
            Assert.Equal(rover.ObstacleInTheWay, Option.None<Location>());
        }
    }
}
