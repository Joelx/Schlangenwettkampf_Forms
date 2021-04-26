using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schlangenwettkampf_Forms
{
    class Welt_old
    {
        private List<Schlange> _schlangen;
        private Vektor _dimensionen;
        private System.Timers.Timer _heartbeat;
        const int MIN = 5;  // Frei gewähltes min und max
        const int MAX = 10; // Für Anzahl und Länge der Schlangen.

        private void detektiere_kollision()
        {

        }

        private static void update(object myobject, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("tick update");
        }

        public void generiere_schlangen()
        {
            // Generiere zufällige Anzahl an Schlangen, sowie deren Länge.
            Random r = new Random();
            int r_anzahl = r.Next(MIN, MAX);   

            for (int i = 0; i < r_anzahl; i++)
            {
                int r_laenge = r.Next(MIN, MAX); // Ebenso frei gewählt.
                // Generiere Startposition.
                Vektor startpos = new Vektor(MAX + 5, i);
                this._schlangen.Add(new Schlange(r_laenge, _dimensionen, startpos));
            }
        }

        public List<Schlange> get_schlangen() { return this._schlangen; }

        private bool ist_jaeger_groesser_beute(Schlange jaeger, Schlange beute)
        {
            return true;
        }

        public Welt_old(Vektor dimensionen, Graphics gr) 
        {
            this._schlangen = new List<Schlange>();
            this._dimensionen = dimensionen;
        }

        public void starte_timer()
        {
            this._heartbeat = new System.Timers.Timer();
            this._heartbeat.Interval = (1000); // 1 sek
            this._heartbeat.Elapsed += new System.Timers.ElapsedEventHandler(update);
            this._heartbeat.Enabled = true;
            this._heartbeat.Start();
        }
    }
}
