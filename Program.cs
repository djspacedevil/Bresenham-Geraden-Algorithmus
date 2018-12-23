using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void calulate(int xstart, int ystart, int xend, int yend)
        {
            /*--------------------------------------------------------------
             * Bresenham-Algorithmus: Linien auf Rastergeräten zeichnen
             *
             * Eingabeparameter:
             *    int xstart, ystart        = Koordinaten des Startpunkts
             *    int xend, yend            = Koordinaten des Endpunkts
             *
             *   Quelle: https://de.wikipedia.org/wiki/Bresenham-Algorithmus
             *   
             * Ausgabe:
             *    void SetPixel(int x, int y) setze ein Pixel in der Grafik
             *         (wird in dieser oder aehnlicher Form vorausgesetzt)
             *---------------------------------------------------------------
             */
            {
                int x, y, t, dx, dy, incx, incy, pdx, pdy, ddx, ddy, deltaslowdirection, deltafastdirection, err;

                /* Entfernung in beiden Dimensionen berechnen */
                dx = xend - xstart;
                dy = yend - ystart;

                /* Vorzeichen des Inkrements bestimmen */
                incx = Math.Sign(dx);
                incy = Math.Sign(dy);
                if (dx < 0) dx = -dx;
                if (dy < 0) dy = -dy;

                /* feststellen, welche Entfernung größer ist */
                if (dx > dy)
                {
                    /* x ist schnelle Richtung */
                    pdx = incx; pdy = 0;    /* pd. ist Parallelschritt */
                    ddx = incx; ddy = incy; /* dd. ist Diagonalschritt */
                    deltaslowdirection = dy; deltafastdirection = dx;   /* Delta in langsamer Richtung, Delta in schneller Richtung */
                }
                else
                {
                    /* y ist schnelle Richtung */
                    pdx = 0; pdy = incy; /* pd. ist Parallelschritt */
                    ddx = incx; ddy = incy; /* dd. ist Diagonalschritt */
                    deltaslowdirection = dx; deltafastdirection = dy;   /* Delta in langsamer Richtung, Delta in schneller Richtung */
                }

                /* Initialisierungen vor Schleifenbeginn */
                x = xstart;
                y = ystart;
                err = deltafastdirection / 2;
                Console.WriteLine("Start Punkt(" + x + ", " + y + ") Mit Error(nicht gewertet): " + err);

                /* Pixel berechnen */
                for (t = 0; t < deltafastdirection; ++t) /* t zaehlt die Pixel, deltafastdirection ist Anzahl der Schritte */
                {
                    /* Aktualisierung Fehlerterm */
                    err -= deltaslowdirection;
                    if (err < 0)
                    {
                        /* Schritt in langsame Richtung, Diagonalschritt */
                        x += ddx;
                        y += ddy;
                    }
                    else
                    {
                        /* Schritt in schnelle Richtung, Parallelschritt */
                        x += pdx;
                        y += pdy;
                    }
                    Console.WriteLine("Punkt(" + x + ", " + y + ") Mit Error: " + err);
                    if (err < 0)
                    {
                        /* Fehlerterm wieder positiv (>=0) machen */
                        err += deltafastdirection;
                    }
                        
                }
            } /* gbham() */
        }

        static void Main(string[] args)
        {
            int xstart, ystart, xend, yend;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            while (true)
            {

                Console.Write("Startpunkt X: ");
                xstart = Convert.ToInt32(Console.ReadLine());
                Console.Write("Startpunkt Y: ");
                ystart = Convert.ToInt32(Console.ReadLine());
            
                Console.Write("Endpunkt X: ");
                xend = Convert.ToInt32(Console.ReadLine());
                Console.Write("Endpunkt Y: ");
                yend = Convert.ToInt32(Console.ReadLine());


                calulate(xstart, ystart, xend, yend);
                Console.ReadLine();
            }
            
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling");
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                System.Environment.Exit(1);
                e.Cancel = true;
            }
        }
    }
}
