using System.Collections.Generic;
using System.Linq;
using Saper.Model;

namespace Saper.Game
{
    public class MapGenerator
    {
        private BombGenerator bombGenerator;
        private HintsGenerator hintsGenerator;

        public MapGenerator(BombGenerator bombGenerator, HintsGenerator hintsGenerator)
        {
            this.bombGenerator = bombGenerator;
            this.hintsGenerator = hintsGenerator;
        }
        
        public Map GenerateMap(int bombsAmount, int width, int height)
        {
            Map emptyMap = GenerateEmptyMap(width, height, bombsAmount);
            Map mapWithBombs = bombGenerator.GenerateBombs(emptyMap);
            Map mapWIthBombsAndHints = hintsGenerator.GenerateHints(mapWithBombs);
            
            return mapWIthBombsAndHints;
        }

        private Map GenerateEmptyMap(int width, int height, int bombsAmount)
        {
            Map map = new Map(width, height, bombsAmount);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    map.Squares.Add(new Square(0, x, y));

            return map;
        }
    }
}