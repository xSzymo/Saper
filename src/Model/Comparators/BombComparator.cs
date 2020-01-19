using System.Collections.Generic;
using Saper.Model;

namespace Saper.Model.Comparator
{
    public class BombComparator : IEqualityComparer<Bomb>
    {
        bool IEqualityComparer<Bomb>.Equals(Bomb bomb, Bomb bomb1)
        {
            return bomb.coordinates.X == bomb1.coordinates.X && bomb.coordinates.Y == bomb1.coordinates.Y;
        }

        int IEqualityComparer<Bomb>.GetHashCode(Bomb obj)
        {
            return obj.coordinates.X * 17 * obj.coordinates.Y;
        }
    }
}