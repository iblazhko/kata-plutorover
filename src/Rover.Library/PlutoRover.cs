using System;
using System.Collections.Generic;
using System.Linq;
using Optional;

namespace Rover.Library
{
    public class PlutoRover
    {
        private static readonly Option<Location> NoObstacles = Option.None<Location>();
        private readonly Pluto _pluto;
        private Position _position;
        private readonly Dictionary<char, Func<Position>> _stepHandlerByCommand;
        private readonly Dictionary<Orientation, Func<Location, Location>> _moveForward;
        private readonly Dictionary<Orientation, Func<Location, Location>> _moveBackward;
        private readonly Dictionary<Orientation, Orientation> _turnRight;
        private readonly Dictionary<Orientation, Orientation> _turnLeft;

        public Position Position => _position;
        public Option<Location> ObstacleInTheWay { get; private set; }

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
            if (initialPosition.Location.X < 0 || initialPosition.Location.X >= pluto.Width ||
                initialPosition.Location.Y < 0 || initialPosition.Location.Y >= pluto.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(initialPosition), "Rover must be located inside the Pluto");
            }

            _pluto = pluto;
            _position = initialPosition;

            _stepHandlerByCommand = new Dictionary<char, Func<Position>>
            {
                {'F', MoveForward},
                {'B', MoveBackward},
                {'R', TurnRight},
                {'L', TurnLeft}
            };

            _moveForward = new Dictionary<Orientation, Func<Location, Location>>
            {
                { Orientation.N, l => new Location(l.X, PositiveWrap(l.Y, _pluto.Width)) },
                { Orientation.E, l => new Location(PositiveWrap(l.X, _pluto.Width), l.Y) },
                { Orientation.S, l => new Location(l.X, NegativeWrap(l.Y, _pluto.Height)) },
                { Orientation.W, l => new Location(NegativeWrap(l.X, _pluto.Width), l.Y) }
            };

            _moveBackward = new Dictionary<Orientation, Func<Location, Location>>
            {
                { Orientation.N, l => new Location(l.X, NegativeWrap(l.Y, _pluto.Width)) },
                { Orientation.E, l => new Location(NegativeWrap(l.X, _pluto.Width), l.Y) },
                { Orientation.S, l => new Location(l.X, PositiveWrap(l.Y, _pluto.Width)) },
                { Orientation.W, l => new Location(PositiveWrap(l.X, _pluto.Width), l.Y) }
            };

            _turnRight = new Dictionary<Orientation, Orientation>
            {
                { Orientation.N, Orientation.E },
                { Orientation.E, Orientation.S },
                { Orientation.S, Orientation.W },
                { Orientation.W, Orientation.N }
            };

            _turnLeft = new Dictionary<Orientation, Orientation>
            {
                { Orientation.N, Orientation.W },
                { Orientation.W, Orientation.S },
                { Orientation.S, Orientation.E },
                { Orientation.E, Orientation.N }
            };
        }

        public void Move(string command)
        {
            ObstacleInTheWay = NoObstacles;
            foreach (var step in command)
            {
                var newPosition = _stepHandlerByCommand[step]();

                if (IsObstacleInTheWay(newPosition.Location))
                {
                    ObstacleInTheWay = Option.Some(newPosition.Location);
                    break;
                }

                _position = newPosition;
            }
        }

        private bool IsObstacleInTheWay(Location location) =>
            _pluto.Obstacles.Any(x => x.Equals(location));

        private int NegativeWrap(int position, int size) => position > 0 ? position - 1 : size - 1;
        private int PositiveWrap(int position, int size) => position < size - 1 ? position + 1 : 0;

        private Position MoveForward() => new Position(_moveForward[_position.Orientation](_position.Location), _position.Orientation);
        private Position MoveBackward() => new Position(_moveBackward[_position.Orientation](_position.Location), _position.Orientation);
        private Position TurnRight() => new Position(_position.Location, _turnRight[_position.Orientation]);
        private Position TurnLeft() => new Position(_position.Location, _turnLeft[_position.Orientation]);
    }
}
