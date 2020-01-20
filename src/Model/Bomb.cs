namespace Saper.Model
{
    public class Bomb
    {
        public Coordinate coordinates { get; set; }

        public Bomb(int x, int y)
        {
            coordinates = new Coordinate(x, y);
        }
    }
}