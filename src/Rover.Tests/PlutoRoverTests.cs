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

        [Theory]
        [InlineData("0,0,N", "0,1,N")]
        [InlineData("5,5,E", "6,5,E")]
        [InlineData("5,5,W", "4,5,W")]
        [InlineData("5,5,S", "5,4,S")]
        public void When_MoveForward_Expect_PositionChanged(
            string initialPosition,
            string expectedFinalPosition)
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position(initialPosition));

            // Act
            rover.Move("F");

            // Assert
            rover.Position.ToString()
                .Should().Be(expectedFinalPosition);
        }

        [Theory]
        [InlineData("0,2,N", "0,1,N")]
        [InlineData("5,5,E", "4,5,E")]
        [InlineData("5,5,W", "6,5,W")]
        [InlineData("5,5,S", "5,6,S")]
        public void When_MoveBackward_Expect_PositionChanged(
            string initialPosition,
            string expectedFinalPosition)
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position(initialPosition));

            // Act
            rover.Move("B");

            // Assert
            rover.Position.ToString()
                .Should().Be(expectedFinalPosition);
        }


        [Fact]
        public void When_TurnRight_Expect_OrientationChanged()
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position("0,0,N"));

            // Act
            rover.Move("R");

            // Assert
            rover.Position.ToString()
                .Should().Be("0,0,E");
        }

        [Fact]
        public void When_TurnLeft_Expect_OrientationChanged()
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position("0,0,N"));

            // Act
            rover.Move("L");

            // Assert
            rover.Position.ToString()
                .Should().Be("0,0,W");
        }
    }
}
