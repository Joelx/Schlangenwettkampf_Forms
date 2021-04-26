using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    class Segment : ABeweglich
    {
        public Segment() { }
        public Segment(Vektor startpos) 
        { 
            this._pos = startpos;
            this.Color = Color.Black;
        }

        public override void bewege(Vektor pos, Random r, List<ABeweglich> segments) { this.set_position(pos); }
    }
}
