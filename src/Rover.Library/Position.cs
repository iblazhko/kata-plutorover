using System;

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

        public Position(string position)
        {
            var parts = position.Split(',');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);
            Orientation = (Orientation)Enum.Parse(typeof(Orientation), parts[2], true);
        }

        public override string ToString() => $"{X},{Y},{Orientation}";
    }
}
