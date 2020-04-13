using System;
using System.Collections.Generic;
using System.Linq;
using Saper.Model;
using Saper.Model.Comparator;

namespace Saper.Game
{
    public class BombGenerator
    {
        private static int MAX_TRIES = 100000; //A Kind of Magic

        public Map GenerateBombs(Map map)
        {
            if (map.BombsAmount >= map.Height * map.Width)
                throw new Exception("Unable to generate - too many bombs");

            Random random = new Random();
            HashSet<Bomb> bombs;
            int index = 0;

            do
            {
                bombs = new HashSet<Bomb>(new BombComparator());

                for (int i = 0; i < map.BombsAmount; i++)
                {
                    int x = random.Next(0, map.Width);
                    int y = random.Next(0, map.Height);

                    bombs.Add(new Bomb(x, y));
                }

                if (index++ >= MAX_TRIES)
                    throw new Exception("Unable to generate");
            } while (!isCorrect(bombs, map.BombsAmount, map.Width, map.Height));

            return GetMapWithGeneratedBombs(map, bombs.ToList());
        }

        private Map GetMapWithGeneratedBombs(Map givenMap, List<Bomb> bombs)
        {
            Map map = new Map(givenMap);
            for (int y = 0; y < map.Height; y++)
                for (int x = 0; x < map.Width; x++)
                    if (DoesAnyBombHasGivenCoordinates(bombs, x, y))
                        map.Squares.Find(bm => bm.Coordinates.X == x && bm.Coordinates.Y == y).Nr = -1;

            return map;
        }

        private bool isCorrect(HashSet<Bomb> bombs, int size, int width, int height)
        {
            return bombs.Count == size && !IsAnyBombSurroundedByOtherBombs(bombs.ToList(), width, height);
        }

        private bool IsAnyBombSurroundedByOtherBombs(List<Bomb> bombs, int width, int height)
        {
            return bombs.Any(bomb => IsBombSurroundedByOtherBombs(bombs, bomb, width, height));
        }

        private bool IsBombSurroundedByOtherBombs(List<Bomb> bombs, Bomb currentBomb, int width, int height)
        {
            int x = currentBomb.Coordinates.X;
            int y = currentBomb.Coordinates.Y;
            List<Coordinate> coordinatesAroundCurrentBomb = new List<Coordinate> {
                new Coordinate(x+1, y), new Coordinate(x, y-1), new Coordinate(x+1, y+1), new Coordinate(x-1, y+1),
                new Coordinate(x-1, y), new Coordinate(x, y+1), new Coordinate(x+1, y-1), new Coordinate(x-1, y-1)};

            return coordinatesAroundCurrentBomb.All(PointToBombOrEmptySpaceBeyondMap(bombs, width, height));
        }

        private Func<Coordinate, bool> PointToBombOrEmptySpaceBeyondMap(List<Bomb> bombs, int width, int height)
        {
            return cords => DoesAnyBombHasGivenCoordinates(bombs, cords.X, cords.Y) || DoGivenCoordinatesPointBeyondMap(cords.X, cords.Y, width, height);
        }

        private bool DoGivenCoordinatesPointBeyondMap(int x, int y, int width, int height)
        {
            if (x < 0 || y < 0)
                return true;
            return x >= width || y >= height;
        }

        private bool DoesAnyBombHasGivenCoordinates(List<Bomb> bombs, int x, int y)
        {
            return bombs.Any(bm => bm.Coordinates.X == x && bm.Coordinates.Y == y);
        }
    }
}