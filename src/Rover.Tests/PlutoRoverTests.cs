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

        [Fact]
        public void When_MoveForwardOneStep_Expect_PositionChanged()
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position("0,0,N"));

            // Act
            rover.Move("F");

            // Assert
            rover.Position.ToString()
                .Should().Be("0,1,N");
        }
    }
}
