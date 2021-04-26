using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    interface IBeweglich
    {
        Vektor get_position();
        void set_position(Vektor v);
        void bewege(Vektor pos, Random r, List<ABeweglich> segments);
    }
}
