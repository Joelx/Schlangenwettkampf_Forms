using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    class Kopf : ABeweglich
    {
        private Vektor _dimensionen; // wird benötigt, um in den Grenzen der Welt zu bleiben.

        public override void bewege(Vektor pos, Random rand, List<ABeweglich> segments)
        {
            Vektor neue_pos;
            int deathTimer = 20;
            do
            {
                neue_pos = generiere_position(rand);
                deathTimer--;
                if (deathTimer == 0)
                {
                    Console.WriteLine("Oops.. Self destruct.");
                    IstTot = true;
                    return;
                }
            } while (!ueberpruefe_position(neue_pos, segments));
            this.set_position(neue_pos);
        }

        private Vektor generiere_position(Random rand)
        {
            Vektor neue_pos;
            int change = rand.Next(0, 2) * 2 - 1;
            if (rand.Next(0, 2) == 0)
            {
                neue_pos = this.get_position() + new Vektor(change, 0); // X
            }
            else
            {
                neue_pos = this.get_position() + new Vektor(0, change); // Y
            }

            return neue_pos;
        }

        private bool ueberpruefe_position(Vektor neue_pos, List<ABeweglich> segments)
        {
            foreach (var segment in segments)
            {
                if (neue_pos == segment.get_position()) { return false; }
                if ((neue_pos + new Vektor(2,2)) > _dimensionen || neue_pos < 5) { return false;  }
            }
            return true;
        }

        public Kopf(Vektor dimensionen, Vektor startPos)
        {
            this.Color = Color.Green;
            this._dimensionen = dimensionen;
            this.set_position(startPos);
        }
    }
}
