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

        public Position Position => _position;

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
            _pluto = pluto;
            _position = initialPosition;

            _stepHandlerByCommand = new Dictionary<char, Func<Position>>
            {
                {'F', MoveForward},
                {'B', MoveBackward}
            };
        }

        public void Move(string command)
        {
            var step = command.First();
            _position = _stepHandlerByCommand[step]();
        }

        private Position MoveForward() => new Position(_position.X, _position.Y + 1, _position.Orientation);
        private Position MoveBackward() => new Position(_position.X, _position.Y - 1, _position.Orientation);
    }
}
