using System;
using System.Collections.Generic;
using System.Linq;

namespace Rover.Library
{
    public class PlutoRover
    {
        private readonly Pluto _pluto;
        private Position _position;
        private readonly Dictionary<char, Func<Position>> _stepHandlerByCommand;
        private readonly Dictionary<Orientation, Func<Location, Location>> _moveForward;
        private readonly Dictionary<Orientation, Func<Location, Location>> _moveBackward;
        private readonly Dictionary<Orientation, Orientation> _turnRight;
        private readonly Dictionary<Orientation, Orientation> _turnLeft;

        public Position Position => _position;

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
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
                { Orientation.N, p => new Location(p.X, p.Y < _pluto.Height-1 ? p.Y + 1 : 0) },
                { Orientation.E, p => new Location(p.X < _pluto.Width-1 ? p.X + 1 : 0, p.Y) },
                { Orientation.S, p => new Location(p.X, p.Y > 0 ? p.Y - 1 : _pluto.Height-1) },
                { Orientation.W, p => new Location(p.X > 0 ? p.X - 1 : _pluto.Width-1, p.Y) }
            };

            _moveBackward = new Dictionary<Orientation, Func<Location, Location>>
            {
                { Orientation.N, p => new Location(p.X, p.Y > 0 ? p.Y - 1 : _pluto.Height-1) },
                { Orientation.E, p => new Location(p.X > 0 ? p.X - 1 : _pluto.Width-1, p.Y) },
                { Orientation.S, p => new Location(p.X, p.Y < _pluto.Height-1 ? p.Y + 1 : 0) },
                { Orientation.W, p => new Location(p.X < _pluto.Width-1 ? p.X + 1 : 0, p.Y) }
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
            var step = command.First();
            _position = _stepHandlerByCommand[step]();
        }

        private Position MoveForward() => new Position(_moveForward[_position.Orientation](_position.Location), _position.Orientation);
        private Position MoveBackward() => new Position(_moveBackward[_position.Orientation](_position.Location), _position.Orientation);
        private Position TurnRight() => new Position(_position.Location, _turnRight[_position.Orientation]);
        private Position TurnLeft() => new Position(_position.Location, _turnLeft[_position.Orientation]);
    }
}
