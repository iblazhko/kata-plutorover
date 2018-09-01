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

        [Fact]
        public void When_TryPositionRoverOutsideGrid_ExpectException()
        {
            // Arrange
            var pluto = new Pluto(100, 100);

            // Act
            var exception = Record.Exception(() => new PlutoRover(pluto, new Position(new Location(100, 0), Orientation.N)));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentOutOfRangeException>();
        }
    }
}
