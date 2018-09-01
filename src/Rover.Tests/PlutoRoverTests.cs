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
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "F", expectedFinalPosition);

        [Theory]
        [InlineData("0,2,N", "0,1,N")]
        [InlineData("5,5,E", "4,5,E")]
        [InlineData("5,5,W", "6,5,W")]
        [InlineData("5,5,S", "5,6,S")]
        public void When_MoveBackward_Expect_PositionChanged(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "B", expectedFinalPosition);

        [Theory]
        [InlineData("0,0,N", "0,0,E")]
        [InlineData("0,0,E", "0,0,S")]
        [InlineData("0,0,S", "0,0,W")]
        [InlineData("0,0,W", "0,0,N")]
        public void When_TurnRight_Expect_OrientationChanged(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "R", expectedFinalPosition);

        [Theory]
        [InlineData("0,0,N", "0,0,W")]
        [InlineData("0,0,W", "0,0,S")]
        [InlineData("0,0,S", "0,0,E")]
        [InlineData("0,0,E", "0,0,N")]
        public void When_TurnLeft_Expect_OrientationChanged(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "L", expectedFinalPosition);

        private void RunCommand(
            string initialPosition,
            string command,
            string expectedFinalPosition)
        {
            // Arrange
            var pluto = new Pluto(100, 100);
            var rover = new PlutoRover(pluto, new Position(initialPosition));

            // Act
            rover.Move(command);

            // Assert
            rover.Position.ToString()
                .Should().Be(expectedFinalPosition);
        }
    }
}
