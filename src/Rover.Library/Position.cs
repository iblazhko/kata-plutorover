namespace Rover.Library
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }
        public Orientation Orientation { get; }

        public Position(int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }
    }
}
