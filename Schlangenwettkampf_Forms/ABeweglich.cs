using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    abstract class ABeweglich : IBeweglich
    {
        protected Vektor _pos;
        protected Color _color;
        protected bool _istTot = false;
        public bool IstTot { get => _istTot; set => _istTot = value; }
        public Color Color { get => _color; set => _color = value; }

        public Vektor get_position() { return _pos; }
        public void set_position(Vektor vek) { _pos = vek; }
        public abstract void bewege(Vektor vek, Random rand, List<ABeweglich> segs); 
    }
}
