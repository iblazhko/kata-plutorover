using Optional;

namespace Rover.Library
{
    public struct PlutoRoverState
    {
        public Position Position { get; }
        public Option<Location> ObstacleInTheWay { get; }

        public PlutoRoverState(Position position, Option<Location> obstacleInTheWay)
        {
            Position = position;
            ObstacleInTheWay = obstacleInTheWay;
        }
    }
}
