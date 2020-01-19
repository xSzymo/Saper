namespace Saper.Model
{
    class Bomb
    {
        public Coordinate coordinates { get; set; }

        public Bomb(int x, int y)
        {
            coordinates = new Coordinate();
            coordinates.X = x;
            coordinates.Y = y;
        }
    }
}