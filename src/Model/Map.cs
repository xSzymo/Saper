using System.Collections.Generic;

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
    }
}