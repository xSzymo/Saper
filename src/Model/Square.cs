namespace Saper.Model
{
    public class Square
    {
        public Square(int nr, int x, int y)
        {
            this.Nr = nr;
            Coordinates = new Coordinate(x, y);
        }

        public Coordinate Coordinates { get; set; }

        /*
        ** TODO - name
        ** -1 == bomb
        **  0 == 0 bombs
        **  1 == 1 bombs
        **  2 == 2 bombs
        ....
        */
        public int Nr { get; set; } 
    }
}