namespace Saper.Model
{
    public class Bomb
    {
        public Coordinate Coordinates { get; set; }

        public Bomb(int x, int y)
        {
            Coordinates = new Coordinate(x, y);
        }
    }
}