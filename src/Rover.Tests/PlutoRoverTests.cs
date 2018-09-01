using FluentAssertions;
using Rover.Library;
using Xunit;

namespace Rover.Tests
{
    public class PlutoRoverTests
    {
        [Fact]
        public void When_TryPositionRoverInsideGrid_ExpectSuccess()
        {
            // Arrange
            var pluto = new Pluto(100, 100);

            // Act
            var exception = Record.Exception(() => new PlutoRover(pluto, new Position(0, 0, Orientation.N)));

            // Assert
            exception.Should().BeNull();
        }
    }
}
