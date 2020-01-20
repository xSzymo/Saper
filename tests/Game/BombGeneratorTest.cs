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
    public void GeneratorGeneratesExaclyAsManyBombsAsRequested(int bombsAmount, int width, int height)
    {
        BombGenerator bombGenerator = new BombGenerator();
        Map map = GenerateEmptyMap(width, height, bombsAmount);

        Map generatedMap = bombGenerator.GenerateBombs(map);

        Assert.Equal(bombsAmount, HowManyBombsDoesMapHave(generatedMap));
    }

    [Theory]
    [InlineData(100, 8, 8)]
    [InlineData(5, 2, 2)]
    [InlineData(2, 1, 1)]
    public void GeneratorThrowsExceptionWhenMapIsTooSmallForAllBombs(int bombsAmount, int width, int height)
    {
        BombGenerator bombGenerator = new BombGenerator();
        Map map = GenerateEmptyMap(width, height, bombsAmount);

        Exception ex = Assert.Throws<Exception>(() => bombGenerator.GenerateBombs(map));

        Assert.NotNull(ex);
    }

    [Fact]
    public void GeneratorDoesNotGenerateBombsWhichAreSurroundedByOtherBombs()
    {
        int bombsAmount = 8, width = 3, height = 3;
        BombGenerator bombGenerator = new BombGenerator();
        Map map = GenerateEmptyMap(width, height, bombsAmount);

        for (int i = 0; i < 2; i++)
        {
            Map generatedMap = bombGenerator.GenerateBombs(map);
            Assert.False(IsAnyBombSurroundedByOtherBombs(generatedMap));
        }
    }

    private bool IsAnyBombSurroundedByOtherBombs(Map map)
    {
        return map.Squares
            .FindAll(x => x.Nr == -1)
            .Any(square => IsBombSurroundedByOtherBombs(map.Squares, square, map.Width, map.Height));
    }

    private bool IsBombSurroundedByOtherBombs(List<Square> squares, Square currentSquare, int width, int height)
    {
        int x = currentSquare.Coordinates.X;
        int y = currentSquare.Coordinates.Y;
        List<Coordinate> coordinatesAroundCurrentBomb = new List<Coordinate> {
                new Coordinate(x+1, y), new Coordinate(x, y-1), new Coordinate(x+1, y+1), new Coordinate(x-1, y+1),
                new Coordinate(x-1, y), new Coordinate(x, y+1), new Coordinate(x+1, y-1), new Coordinate(x-1, y-1)};

        return coordinatesAroundCurrentBomb.All(PointToBombOrEmptySpaceBeyondMap(squares, width, height));
    }

    private Func<Coordinate, bool> PointToBombOrEmptySpaceBeyondMap(List<Square> squares, int width, int height)
    {
        return cords => DoesAnyBombHasGivenCoordinates(squares, cords.X, cords.Y) || DoGivenCoordinatesPointBeyondMap(cords.X, cords.Y, width, height);
    }

    private bool DoGivenCoordinatesPointBeyondMap(int x, int y, int width, int height)
    {
        if (x < 0 || y < 0)
            return true;
        return x >= width || y >= height;
    }

    private static bool DoesAnyBombHasGivenCoordinates(List<Square> squares, int x, int y)
    {
        return squares.Any(sq => sq.Coordinates.X == x && sq.Coordinates.Y == y && sq.Nr == -1);
    }

    private Map GenerateEmptyMap(int width, int height, int bombsAmount)
    {
        Map map = new Map(width, height, bombsAmount);
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                map.Squares.Add(new Square(0, x, y));

        return map;
    }

    private int HowManyBombsDoesMapHave(Map map)
    {
        return map.Squares.FindAll(x => x.Nr == -1).Count;
    }
}
