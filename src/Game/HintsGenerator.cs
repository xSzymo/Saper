using System.Collections.Generic;
using Saper.Model;

namespace Saper.Game
{
    public class HintsGenerator
    {
        public Map GenerateHints(Map givenMap)
        {
            Map map = new Map(givenMap);

            for (int y = 0; y < map.Height; y++)
                for (int x = 0; x < map.Width; x++)
                    if (!isItBombWithoutPresenceCheck(map, new Coordinate(x, y)))
                        UpdateCurrentSquareValue(map, y, x);

            return map;
        }

        private void UpdateCurrentSquareValue(Map map, int y, int x)
        {
            Square square = map.Squares.Find(bm => bm.Coordinates.X == x && bm.Coordinates.Y == y);
            square.Nr = HowManyBombsAreAroundCurrentSqare(map, square);
        }

        private int HowManyBombsAreAroundCurrentSqare(Map map, Square square)
        {
            int x = square.Coordinates.X;
            int y = square.Coordinates.Y;
            List<Coordinate> coordinatesAroundCurrentSquare = new List<Coordinate> {
                new Coordinate(x+1, y), new Coordinate(x, y-1), new Coordinate(x+1, y+1), new Coordinate(x-1, y+1),
                new Coordinate(x-1, y), new Coordinate(x, y+1), new Coordinate(x+1, y-1), new Coordinate(x-1, y-1)};

            return coordinatesAroundCurrentSquare.FindAll(coords => isItBombWithPresenceCheck(map, coords)).Count;
        }

        private bool isItBombWithPresenceCheck(Map map, Coordinate coordinates)
        {
            Square square = map.Squares.Find(sq => sq.Coordinates.X == coordinates.X && sq.Coordinates.Y == coordinates.Y);
            return square != null && square.Nr == -1;
        }
        
        private bool isItBombWithoutPresenceCheck(Map map, Coordinate coordinates)
        {
            return map.Squares.Find(sq => sq.Coordinates.X == coordinates.X && sq.Coordinates.Y == coordinates.Y).Nr == -1;
        }
    }
}