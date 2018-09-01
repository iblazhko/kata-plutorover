using System;

namespace Rover.Library
{
    public struct Position
    {
        public Location Location { get; }
        public Orientation Orientation { get; }

        public Position(Location location, Orientation orientation)
        {
            Location = location;
            Orientation = orientation;
        }

        public Position(string position)
        {
            var parts = position.Split(',');
            Location = new Location(int.Parse(parts[0]), int.Parse(parts[1]));
            Orientation = (Orientation)Enum.Parse(typeof(Orientation), parts[2], true);
        }

        public override string ToString() => $"{Location.X},{Location.Y},{Orientation}";
    }
}
