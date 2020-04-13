using System.Collections.Generic;
using System.Linq;

namespace Saper.Model
{
    public class Map
    {
        public List<Square> Squares { get; set; }
        public int Width { get; set; } 
        public int Height { get; set; }
        public int BombsAmount { get; set; }

        public Map(int width, int height, int BombsAmount)
        {
            Squares = new List<Square>();
            this.Width = width;
            this.Height = height;
            this.BombsAmount = BombsAmount;
        }

        public Map(Map map)
        {
            this.Width = map.Width;
            this.Height = map.Height;
            this.BombsAmount = map.BombsAmount;
            this.Squares = map.Squares.Select(square => new Square(square)).ToList();
        }
    }
}