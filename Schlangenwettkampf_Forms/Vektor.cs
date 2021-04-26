using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    class Vektor
    {
        public int x, y;

        public Vektor() {}
        public Vektor(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vektor operator +(Vektor a, Vektor b)
        {
            return new Vektor(a.x + b.x, a.y + b.y);
        }

        public static bool operator <(Vektor a, Vektor b)
        {
            if (a.x < b.x || a.y < b.y)
                return true;
            else
                return false;
        }

        public static bool operator >(Vektor a, Vektor b)
        {
            if (a.x > b.x || a.y > b.y)
                return true;
            else
                return false;
        }

        public static bool operator >(Vektor a, int b)
        {
            if (a.x > b || a.y > b)
                return true;
            else
                return false;
        }

        public static bool operator <(Vektor a, int b)
        {
            if (a.x < b || a.y < b)
                return true;
            else
                return false;
        }

        public static bool operator ==(Vektor a, Vektor b)
        {
            if (a.x == b.x && a.y == b.y)
                return true;
            else
                return false;         
        }

        public static bool operator !=(Vektor a, Vektor b)
        {
            if (a.x != b.x || a.y != b.y)
                return true;
            else
                return false;
        }

    }
}
