using System;
using System.Collections.Generic;
using System.Linq;
using Optional;

namespace Rover.Library
{
    public class PlutoRover
    {
        private static readonly Dictionary<char, Func<Pluto, Position, Position>> _positionTransformerByCommand =
            new Dictionary<char, Func<Pluto, Position, Position>>
            {
                {'F', MoveForward},
                {'B', MoveBackward},
                {'R', TurnRight},
                {'L', TurnLeft}
            };

        private static readonly Dictionary<Orientation, Func<Pluto, Location, Location>> _moveForward =
            new Dictionary<Orientation, Func<Pluto, Location, Location>>
            {
                { Orientation.N, (p, l) => new Location(l.X, PositiveWrap(l.Y, p.Width)) },
                { Orientation.E, (p, l) => new Location(PositiveWrap(l.X, p.Width), l.Y) },
                { Orientation.S, (p, l) => new Location(l.X, NegativeWrap(l.Y, p.Height)) },
                { Orientation.W, (p, l) => new Location(NegativeWrap(l.X, p.Width), l.Y) }
            };

        private static readonly Dictionary<Orientation, Func<Pluto, Location, Location>> _moveBackward =
            new Dictionary<Orientation, Func<Pluto, Location, Location>>
            {
                { Orientation.N, (p, l) => new Location(l.X, NegativeWrap(l.Y, p.Width)) },
                { Orientation.E, (p, l) => new Location(NegativeWrap(l.X, p.Width), l.Y) },
                { Orientation.S, (p, l) => new Location(l.X, PositiveWrap(l.Y, p.Width)) },
                { Orientation.W, (p, l) => new Location(PositiveWrap(l.X, p.Width), l.Y) }
            };

        private static readonly Dictionary<Orientation, Orientation> _turnRight =
            new Dictionary<Orientation, Orientation>
            {
                { Orientation.N, Orientation.E },
                { Orientation.E, Orientation.S },
                { Orientation.S, Orientation.W },
                { Orientation.W, Orientation.N }
            };

        private static readonly Dictionary<Orientation, Orientation> _turnLeft =
            new Dictionary<Orientation, Orientation>
            {
                { Orientation.N, Orientation.W },
                { Orientation.W, Orientation.S },
                { Orientation.S, Orientation.E },
                { Orientation.E, Orientation.N }
            };

        private readonly Pluto _pluto;
        private PlutoRoverState _state;

        public Position Position => _state.Position;
        public Option<Location> ObstacleInTheWay => _state.ObstacleInTheWay;

        public PlutoRover(Pluto pluto, Position initialPosition)
        {
            _pluto = pluto;
            _state = new PlutoRoverState(initialPosition, Option.None<Location>());
        }

        public void Move(string command)
        {
            foreach (var step in command)
            {
                _state = StepHandler(_pluto, _state, step);
                if (_state.ObstacleInTheWay.HasValue) break;
            }
        }

        private static PlutoRoverState StepHandler(Pluto pluto, PlutoRoverState state, char step) =>
            GetStateInNewPosition(pluto, state, _positionTransformerByCommand[step](pluto, state.Position));

        private static PlutoRoverState GetStateInNewPosition(Pluto pluto, PlutoRoverState state, Position newPosition) =>
            IsObstacleInTheWay(pluto, newPosition.Location)
                ? new PlutoRoverState(state.Position, Option.Some(newPosition.Location))
                : new PlutoRoverState(newPosition, Option.None<Location>());

        private static bool IsObstacleInTheWay(Pluto pluto, Location location) =>
            pluto.Obstacles.Any(x => x.Equals(location));

        private static int NegativeWrap(int position, int size) => position > 0 ? position - 1 : size - 1;
        private static int PositiveWrap(int position, int size) => position < size - 1 ? position + 1 : 0;

        private static Position MoveForward(Pluto pluto, Position position) =>
            new Position(_moveForward[position.Orientation](pluto, position.Location), position.Orientation);

        private static Position MoveBackward(Pluto pluto, Position position) =>
            new Position(_moveBackward[position.Orientation](pluto, position.Location), position.Orientation);

        private static Position TurnRight(Pluto _, Position position) =>
            new Position(position.Location, _turnRight[position.Orientation]);

        private static Position TurnLeft(Pluto _, Position position) =>
            new Position(position.Location, _turnLeft[position.Orientation]);
    }
}
