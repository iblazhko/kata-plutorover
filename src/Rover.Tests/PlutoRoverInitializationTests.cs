using System;
using FluentAssertions;
using Rover.Library;
using Xunit;

namespace Rover.Tests
{
    public class PlutoRoverInitializationTests
    {
        [Fact]
        public void When_TryPositionRoverInsideGrid_ExpectSuccess()
        {
            // Arrange
            var pluto = new Pluto(100, 100);

            // Act
            var exception = Record.Exception(() => new PlutoRover(pluto, new Position(new Location(0, 0), Orientation.N)));

            // Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        [InlineData(11, 5)]
        [InlineData(5, 11)]
        [InlineData(11, 11)]
        public void When_TryPositionRoverOutsideTheGrid_ExpectException(int x, int y)
        {
            // Arrange
            var pluto = new Pluto(10, 10);

            // Act
            var exception = Record.Exception(() => new PlutoRover(pluto, new Position(new Location(x, y), Orientation.N)));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentOutOfRangeException>();
            exception.Message.Should().StartWith("Rover must be located inside the Pluto");
            exception.Message.Should().EndWith("Parameter name: initialPosition");
        }

        [Fact]
        public void When_TryPositionRoverWithNoPluto_ExpectSuccess()
        {
            // Arrange
            // N/A

            // Act
            var exception = Record.Exception(() => new PlutoRover(null, new Position(new Location(0, 0), Orientation.N)));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().EndWith("Parameter name: pluto");
        }

        [Fact]
        public void When_PlutoHasInvalidWidth_ExpectException()
        {
            // Arrange
            // N/A

            // Act
            var exception = Record.Exception(() => new Pluto(0, 1));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentOutOfRangeException>();
            exception.Message.Should().StartWith("Width must be a positive number");
            exception.Message.Should().EndWith("Parameter name: width");
        }

        [Fact]
        public void When_PlutoHasInvalidHeight_ExpectException()
        {
            // Arrange
            // N/A

            // Act
            var exception = Record.Exception(() => new Pluto(10, 0));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentOutOfRangeException>();
            exception.Message.Should().StartWith("Height must be a positive number");
            exception.Message.Should().EndWith("Parameter name: height");
        }
    }
}
