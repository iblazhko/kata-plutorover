using System;
using System.Collections.Generic;

namespace Rover.Library
{
    public class Pluto
    {
        private static readonly Location[] NoObstacles = new Location [0];

        public int Width { get; }
        public int Height { get; }
        public ISet<Location> Obstacles { get; }

        public Pluto(int width, int height)
            :this(width, height, NoObstacles)
        {
        }

        public Pluto(int width, int height, IEnumerable<Location> obstacles)
        {
            if (width <=0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be a positive number");
            if (height <=0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be a positive number");

            Width = width;
            Height = height;
            Obstacles = new HashSet<Location>(obstacles);
        }
    }
}
