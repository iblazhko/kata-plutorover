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
        private readonly Dictionary<Orientation, Func<Position, Position>> _moveForward;
        private readonly Dictionary<Orientation, Func<Position, Position>> _moveBackward;
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

            _moveForward = new Dictionary<Orientation, Func<Position, Position>>
            {
                { Orientation.N, p => new Position(p.X, p.Y < _pluto.Height-1 ? p.Y + 1 : 0, p.Orientation) },
                { Orientation.E, p => new Position(p.X + 1, p.Y, p.Orientation) },
                { Orientation.S, p => new Position(p.X, p.Y - 1, p.Orientation) },
                { Orientation.W, p => new Position(p.X - 1, p.Y, p.Orientation) }
            };

            _moveBackward = new Dictionary<Orientation, Func<Position, Position>>
            {
                { Orientation.N, p => new Position(p.X, p.Y - 1, p.Orientation) },
                { Orientation.E, p => new Position(p.X - 1, p.Y, p.Orientation) },
                { Orientation.S, p => new Position(p.X, p.Y + 1, p.Orientation) },
                { Orientation.W, p => new Position(p.X + 1, p.Y, p.Orientation) }
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

        private Position MoveForward() => _moveForward[_position.Orientation](_position);
        private Position MoveBackward() => _moveBackward[_position.Orientation](_position);
        private Position TurnRight() => new Position(_position.X, _position.Y, _turnRight[_position.Orientation]);
        private Position TurnLeft() => new Position(_position.X, _position.Y, _turnLeft[_position.Orientation]);
    }
}
