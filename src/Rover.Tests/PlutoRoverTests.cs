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
            var exception = Record.Exception(() => new PlutoRover(pluto, new Position(new Location(0, 0), Orientation.N)));

            // Assert
            exception.Should().BeNull();
        }

        [Theory]
        [InlineData("0,0,N", "0,1,N")]
        [InlineData("5,5,E", "6,5,E")]
        [InlineData("5,5,W", "4,5,W")]
        [InlineData("5,5,S", "5,4,S")]
        public void When_MoveForward_Expect_LocationChangedAndOrientationMaintained(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "F", expectedFinalPosition);

        [Theory]
        [InlineData("0,2,N", "0,1,N")]
        [InlineData("5,5,E", "4,5,E")]
        [InlineData("5,5,W", "6,5,W")]
        [InlineData("5,5,S", "5,6,S")]
        public void When_MoveBackward_Expect_LocationChangedAndOrientationMaintained(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "B", expectedFinalPosition);

        [Theory]
        [InlineData("0,0,N", "0,0,E")]
        [InlineData("0,0,E", "0,0,S")]
        [InlineData("0,0,S", "0,0,W")]
        [InlineData("0,0,W", "0,0,N")]
        public void When_TurnRight_Expect_OrientationChangedAndLocationMaintained(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "R", expectedFinalPosition);

        [Theory]
        [InlineData("0,0,N", "0,0,W")]
        [InlineData("0,0,W", "0,0,S")]
        [InlineData("0,0,S", "0,0,E")]
        [InlineData("0,0,E", "0,0,N")]
        public void When_TurnLeft_Expect_OrientationChangedAndLocationMaintained(
            string initialPosition,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, "L", expectedFinalPosition);

        [Theory]
        [InlineData("0,9,N", "F", "0,0,N")]
        [InlineData("0,0,N", "B", "0,9,N")]
        [InlineData("9,5,E", "F", "0,5,E")]
        [InlineData("0,5,E", "B", "9,5,E")]
        [InlineData("5,0,S", "F", "5,9,S")]
        [InlineData("5,9,S", "B", "5,0,S")]
        [InlineData("0,1,W", "F", "9,1,W")]
        [InlineData("9,0,W", "B", "0,0,W")]
        public void When_MoveOverBorder_Expect_LocationWrappedAndOrientationMaintained(
            string initialPosition,
            string command,
            string expectedFinalPosition) =>
            RunCommand(initialPosition, command, expectedFinalPosition, 10, 10);

        [Fact]
        public void When_MultiStepCommand_Expect_AllStepsExecuted() =>
            RunCommand("0,0,N", "FFBBFRFLRLFR", "1,2,E");

        private void RunCommand(
            string initialPosition,
            string command,
            string expectedFinalPosition,
            int plutoWidth = 100,
            int plutoHeight = 100)
        {
            // Arrange
            var pluto = new Pluto(plutoWidth, plutoHeight);
            var rover = new PlutoRover(pluto, new Position(initialPosition));

            // Act
            rover.Move(command);

            // Assert
            rover.Position.ToString()
                .Should().Be(expectedFinalPosition);
        }
    }
}
