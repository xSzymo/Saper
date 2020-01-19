using System;
using System.Collections.Generic;
using Saper.Game;
using Saper.Model;
using Xunit;
using System.Linq;

public class BombGeneratorTest
{
    [Theory]
    [InlineData(10, 8, 8)]
    [InlineData(5, 8, 8)]
    [InlineData(1, 8, 8)]
    public void GeneratorGeneratesExaclyAsManyBombsAsRequested(int nrOfBombs, int width, int height)
    {
        BombGenerator bombGenerator = new BombGenerator();

        List<Bomb> bombs = bombGenerator.GenerateBombs(nrOfBombs, width, height);

        Assert.Equal(nrOfBombs, bombs.Count);
    }

    [Theory]
    [InlineData(100, 8, 8)]
    [InlineData(5, 2, 2)]
    [InlineData(2, 1, 1)]
    public void GeneratorThrowsExceptionWhenMapIsTooSmallForAllBombs(int nrOfBombs, int width, int height)
    {   
        BombGenerator bombGenerator = new BombGenerator();

        Exception ex = Assert.Throws<Exception>(() => bombGenerator.GenerateBombs(nrOfBombs, width, height));

        Assert.NotNull(ex);
    }
    // B B B
    // X B B
    // X B X

    [Fact]
    public void GeneratorDoesNotGenerateBombsWhichAreSurroundedByOtherBombs()
    {
        int nrOfBombs = 8, width = 3, height = 3;
        BombGenerator bombGenerator = new BombGenerator();

        for (int i = 0; i < 2; i++)
        {
            List<Bomb> bombs = bombGenerator.GenerateBombs(nrOfBombs, width, height);
            Assert.False(IsAnyBombSurroundedByOtherBombs(bombs, width, height));
        }
    }

    private bool IsAnyBombSurroundedByOtherBombs(List<Bomb> bombs, int width, int height)
    {
        return bombs.Any(bomb => IsBombSurroundedByOtherBombs(bombs, bomb, width, height));
    }

    private bool IsBombSurroundedByOtherBombs(List<Bomb> bombs, Bomb currentBomb, int width, int height)
    {
        int x = currentBomb.coordinates.X;
        int y = currentBomb.coordinates.Y;
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

    private static bool DoesAnyBombHasGivenCoordinates(List<Bomb> bombs, int x, int y)
    {
        return bombs.Any(bm => bm.coordinates.X == x && bm.coordinates.Y == y);
    }
}
