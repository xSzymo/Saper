using System;
using System.Collections.Generic;
using System.Linq;
using Saper.Model;
using Saper.Model.Comparator;

namespace Saper.Game
{
    class BombGenerator
    {
        private static int MAX_TRIES = 100000; //A Kind of Magic

        public List<Bomb> GenerateBombs(int size, int height, int width)
        {
            if (size >= height * width)
                throw new Exception("Unable to generate - too many bombs");

            Random random = new Random();
            HashSet<Bomb> bombs;
            int index = 0;

            do
            {
                bombs = new HashSet<Bomb>(new BombComparator());

                for (int i = 0; i < size; i++)
                {
                    int x = random.Next(0, width);
                    int y = random.Next(0, height);

                    bombs.Add(new Bomb(x, y));
                }

                if (index++ >= MAX_TRIES)
                    throw new Exception("Unable to generate");
            } while (!isCorrect(bombs, size, width, height));

            return bombs.ToList();
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