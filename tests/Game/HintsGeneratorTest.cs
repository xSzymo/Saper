using Saper.Game;
using Saper.Model;
using Xunit;

public class HintsGeneratorTest
{
    [Fact]
    public void Map3X3WithTwoBombsCreatedGets4Hints()
    {
        HintsGenerator hintsGenerator = new HintsGenerator();
        Map createdMap = new Map(3, 3);

        createdMap.Squares.Add(new Square(-1, 0, 0));
        createdMap.Squares.Add(new Square(-1, 1, 0));
        createdMap.Squares.Add(new Square(0, 2, 0));

        createdMap.Squares.Add(new Square(0, 0, 1));
        createdMap.Squares.Add(new Square(0, 1, 1));
        createdMap.Squares.Add(new Square(0, 2, 1));

        createdMap.Squares.Add(new Square(0, 0, 2));
        createdMap.Squares.Add(new Square(0, 1, 2));
        createdMap.Squares.Add(new Square(0, 2, 2));

        Map map = hintsGenerator.GenerateHints(createdMap);

        Assert.Equal(-1, map.Squares.Find(x => x.Coordinates.X == 0 && x.Coordinates.Y == 0).Nr);
        Assert.Equal(-1, map.Squares.Find(x => x.Coordinates.X == 1 && x.Coordinates.Y == 0).Nr);
        Assert.Equal(1, map.Squares.Find(x => x.Coordinates.X == 2 && x.Coordinates.Y == 0).Nr);

        Assert.Equal(2, map.Squares.Find(x => x.Coordinates.X == 0 && x.Coordinates.Y == 1).Nr);
        Assert.Equal(2, map.Squares.Find(x => x.Coordinates.X == 1 && x.Coordinates.Y == 1).Nr);
        Assert.Equal(1, map.Squares.Find(x => x.Coordinates.X == 2 && x.Coordinates.Y == 1).Nr);

        Assert.Equal(0, map.Squares.Find(x => x.Coordinates.X == 0 && x.Coordinates.Y == 2).Nr);
        Assert.Equal(0, map.Squares.Find(x => x.Coordinates.X == 1 && x.Coordinates.Y == 2).Nr);
        Assert.Equal(0, map.Squares.Find(x => x.Coordinates.X == 2 && x.Coordinates.Y == 2).Nr);
    }
}
