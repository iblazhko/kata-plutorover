namespace Rover.Library
{
    public class PlutoRover
    {
        private readonly Pluto _pluto;
        private readonly Position _position;

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
            _pluto = pluto;
            _position = initialPosition;
        }
    }
}
