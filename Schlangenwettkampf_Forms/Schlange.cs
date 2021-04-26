using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    class Schlange
    {
        private List<ABeweglich> _segmente;
        private int _laenge;
        private Kopf _kopf;

        public int get_laenge() { return this._laenge; }
        public Schlange(int laenge, Vektor dimensionen, Vektor startpos)
        {
            this._laenge = laenge;
            this._segmente = new List<ABeweglich>();
            this._kopf = new Kopf(dimensionen, startpos);

            _segmente.Add(this._kopf);
            for (int i = 1; i < laenge; i++) // Start bei 1, weil Kopf gesondert behandelt, aber in Länge enthalten.
            {
                _segmente.Add(new Segment(new Vektor(startpos.x - i, startpos.y)));
            }
        }

        public void fresse(Schlange beute)
        {
            int laengeBeute = beute.get_laenge();
            Vektor schwanzPos = this.get_segmente()[this._laenge - 1].get_position();
            for (int i = 0; i < laengeBeute; i++)
            {
                this._segmente.Add(new Segment(new Vektor(schwanzPos.x - i + 1, schwanzPos.y)));
            }
            this._laenge = this._segmente.Count;
        }

        public Kopf get_kopf() { return this._kopf; }
        public List<ABeweglich> get_segmente() { return this._segmente; }
        public void set_laenge(int laenge) { this._laenge = laenge; }
    }
}
