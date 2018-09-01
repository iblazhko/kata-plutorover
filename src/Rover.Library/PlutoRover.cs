using System.Linq;

namespace Rover.Library
{
    public class PlutoRover
    {
        private readonly Pluto _pluto;
        private Position _position;
        public Position Position => _position;

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
            _pluto = pluto;
            _position = initialPosition;
        }

        public void Move(string command)
        {
            var step = command.First();
            switch (step)
            {
                case 'F':
                    _position = new Position(_position.X, _position.Y+1, _position.Orientation);
                    break;
                case 'B':
                    _position = new Position(_position.X, _position.Y-1, _position.Orientation);
                    break;
            }
        }
    }
}
