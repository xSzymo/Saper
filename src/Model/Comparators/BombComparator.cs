using System.Collections.Generic;
using Saper.Model;

namespace Saper.Model.Comparator
{
    public class BombComparator : IEqualityComparer<Bomb>
    {
        bool IEqualityComparer<Bomb>.Equals(Bomb bomb, Bomb bomb1)
        {
            return bomb.Coordinates.X == bomb1.Coordinates.X && bomb.Coordinates.Y == bomb1.Coordinates.Y;
        }

        int IEqualityComparer<Bomb>.GetHashCode(Bomb obj)
        {
            return obj.Coordinates.X * 17 * obj.Coordinates.Y;
        }
    }
}