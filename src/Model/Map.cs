using System.Collections.Generic;
using System.Linq;

namespace Saper.Model
{
    public class Map
    {
        public List<Square> Squares { get; set; }
        public int Width { get; set; } 
        public int Height { get; set; }

        public Map(int width, int height)
        {
            Squares = new List<Square>();
            this.Width = width;
            this.Height = height;
        }

        public Map(Map map)
        {
            this.Width = map.Width;
            this.Height = map.Height;
            this.Squares = map.Squares.Select(square => new Square(square)).ToList();
        }
    }
}