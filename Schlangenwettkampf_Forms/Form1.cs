using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schlangenwettkampf_Forms
{
    public partial class Welt : Form
    {
        public Welt()
        {
            InitializeComponent();
        }

        private Bitmap Bm;
        private Graphics Gr;
        private Random Rand = new Random();
        private List<Schlange> _schlangen = new List<Schlange>();
        private Vektor _dimensionen;
        private System.Windows.Forms.Timer _heartbeat;
        private const int MIN = 10;  // Frei gewähltes min und max
        private const int MAX = 15; // Für Anzahl und Länge der Schlangen.
        private const int PIXEL_SCALE = 10;
        private const int INTERVAL = 100; // 500ms

        private void detektiere_kollision(Schlange jaeger)
        {
            Vektor kopfPos = jaeger.get_kopf().get_position();
            using (Brush brush = new SolidBrush(Color.Gray))
            {
                for (int i = 0; i < _dimensionen.y; i++)
                {
                    for (int j = 0; j < _dimensionen.x; j++)
                    {                      
                        if ((i > 0 && j > 0) && (i != (_dimensionen.y-1)) && (j != (_dimensionen.x-1)))
                            continue;
                        Gr.FillRectangle(brush, j * PIXEL_SCALE, i * PIXEL_SCALE, 10, 10); // Zeichne Segment   
                    }
                }
            }
            foreach (var beute in _schlangen.ToList())
            {
                if (beute == jaeger)
                    continue;
                else
                {
                    foreach (var segment in beute.get_segmente())
                    {
                        Vektor segPos = segment.get_position();
                        if (kopfPos.x == segPos.x && kopfPos.y == segPos.y)
                        {
                            Console.WriteLine("Kollision!");
                            if (ist_jaeger_groesser_beute(jaeger, beute))
                            {
                                Console.WriteLine("fresse!");
                                using (Brush brush = new SolidBrush(Color.FromArgb(100, 255, 0, 0)))
                                {
                                    Gr.FillRectangle(brush, kopfPos.x * PIXEL_SCALE, kopfPos.y * PIXEL_SCALE, 30, 30); // Zeichne Segment       
                                    Refresh();
                                    System.Threading.Thread.Sleep(2000);
                                }
                                jaeger.fresse(beute);
                                _schlangen.Remove(beute);
                            } else { Console.WriteLine("Beute war zu groß!"); }
                        }
                    }
                }
            }
        }

        private void update(object sender, EventArgs e)
        {
            Gr.Clear(Color.White);
            foreach (var schlange in this._schlangen.ToList())
            {
                Vektor pos = new Vektor(0, 0);
                Vektor vorgaenger = new Vektor(0, 0);
                List<ABeweglich> segments = schlange.get_segmente();
                foreach (var segment in segments)
                {
                    Color color = segment.Color;
                    pos = segment.get_position();   // Hole aktuelle Segmentposition
                    using (Brush brush = new SolidBrush(color))
                    {                        
                        Gr.FillRectangle(brush, pos.x * PIXEL_SCALE, pos.y * PIXEL_SCALE, 10, 10); // Zeichne Segment                     
                    }
                    segment.bewege(vorgaenger, Rand, segments); // Bewege aktuelles Segment dort hin, wo das letzte Segment war
                    if (segment.IstTot)
                    {
                        _schlangen.Remove(schlange);
                        continue;
                    }
                    vorgaenger = pos;           // Aktuelles Segment wird zum Vorgaenger fuer das nachste Segment.
                }
                detektiere_kollision(schlange);
            }
            Refresh();
        }

        public void generiere_schlangen()
        {
            // Generiere zufällige Anzahl an Schlangen, sowie deren Länge.
            Random r = new Random();
            int r_anzahl = r.Next(MIN, MAX);

            for (int i = 0; i < r_anzahl; i++)
            {
                int r_laenge = r.Next(MIN, 16); // Ebenso frei gewählt.
                // Generiere Startposition.
                Vektor startpos = new Vektor(MAX + 5, i*6+10);
                this._schlangen.Add(new Schlange(r_laenge, _dimensionen, startpos));
            }
        }

        private static bool ist_jaeger_groesser_beute(Schlange jaeger, Schlange beute)
        {
            if (jaeger.get_laenge() > beute.get_laenge())
                return true;
            else
                return false;
        }

        public void starte_timer()
        {
            this._heartbeat = new System.Windows.Forms.Timer();
            this._heartbeat.Interval = (INTERVAL); 
            this._heartbeat.Tick += new EventHandler(update);
            this._heartbeat.Enabled = true;
            this._heartbeat.Start();
        }


        // Make a bitmap to display.
        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("test!");
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            DoubleBuffered = true;
            // Set at design time:
            //      StartPosition = FormStartPosition.CenterScreen;

            Bm = new Bitmap(ClientSize.Width, ClientSize.Height);
            Gr = Graphics.FromImage(Bm);
            BackgroundImage = Bm;

            this._dimensionen = new Vektor(ClientSize.Width/PIXEL_SCALE, ClientSize.Height/PIXEL_SCALE); // 
            Console.WriteLine(ClientSize.Height);

            generiere_schlangen();
            starte_timer();
        }


        private void Form1_MouseClick(object sender, EventArgs e)
        {
            var groessteSchlange = _schlangen.OrderByDescending(item => item.get_laenge()).First(); // Ermittle groesste Schlange
            int laenge = groessteSchlange.get_laenge();
            int low = (int)Math.Floor(laenge / 2.0f);
            int high = (int)Math.Ceiling(laenge / 2.0f);
            List<ABeweglich> segmente = groessteSchlange.get_segmente();
            this._schlangen.Remove(groessteSchlange);
            this._schlangen.Add(new Schlange(high, _dimensionen, segmente[0].get_position()));
            this._schlangen.Add(new Schlange(low, _dimensionen, segmente[high].get_position()));
        }
    }
}
