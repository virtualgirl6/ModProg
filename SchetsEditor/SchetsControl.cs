using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SchetsEditor
{
    public class SchetsControl : UserControl
    {
        private Schets schets;
        private Color penkleur;
        public List<ObjectVorm> lijst = new List<ObjectVorm>(); //misschien de new in tekenLijst zetten
        int roteer;

        public Color PenKleur
        {
            get { return penkleur; }
        }
        public Schets Schets
        {
            get { return schets; }
        }

        public SchetsControl()
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            this.schets = new Schets();
            this.Paint += this.tekenLijst;

            this.Resize += this.veranderAfmeting;
            this.veranderAfmeting(null, null);

            roteer = 0;
        }

        public void tekenLijst(object o, PaintEventArgs pea) //weet niet of dit zo moet. tekenen wat op de lijst staat
        {
            Console.WriteLine($"Ik teken {lijst.Count} elementen");
            string naam;

            schets.NieuweBitmap(); //ervoor zorgen dat de nieuwe tekening geen oude elementen bevat

            for (int i = 0; i < lijst.Count; i++)
            {
                naam = lijst[i].naam;

                if (naam == "CirkelTool")
                    schets.TekenCirkel(pea.Graphics, lijst[i].pen, lijst[i].rect);

                if (naam == "VolCirkelTool")
                    schets.TekenCirkelVol(pea.Graphics, lijst[i].kwast, lijst[i].rect);

                if (naam == "RechthoekTool")
                    schets.TekenRecht(pea.Graphics, lijst[i].pen, lijst[i].rect);

                if (naam == "VolRechthoekTool")
                    schets.TekenRechtVol(pea.Graphics, lijst[i].kwast, lijst[i].rect);

                if (naam == "LijnTool")
                    schets.TekenLijn(pea.Graphics, lijst[i].pen, lijst[i].start, lijst[i].eind);

                if (naam == "PenTool")
                    schets.TekenLijn(pea.Graphics, lijst[i].pen, lijst[i].start, lijst[i].eind);

                if (naam == "TekstTool")
                    schets.TekenTekst(pea.Graphics, lijst[i].kwast, lijst[i].start, lijst[i].tekst);

            }
            Console.WriteLine($"PAINT {DateTime.Now.Millisecond}");
            //.Graphics.Clear(Color.);
            //pea.Graphics.Flush();
        }

       
        private void veranderAfmeting(object o, EventArgs ea)
        {
            schets.VeranderAfmeting(this.ClientSize);
            this.Invalidate();
        }
        public Graphics MaakBitmapGraphics()
        {
            Graphics g = schets.BitmapGraphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            return g;
        }
        public void Schoon(object o, EventArgs ea)
        {
            lijst.Clear();
            schets.SchoonSchets();

            this.Invalidate();
        }

        public void Undo(object o, EventArgs ea)
        {
            try { lijst.RemoveAt(lijst.Count - 1); this.Invalidate(); }
            catch { }
        }

        public void Roteer(object o, EventArgs ea)
        {
            string naam;
            
            int startx, starty, eindx, eindy, sizex, sizey;
            for (int i = 0; i < lijst.Count; i++)
            {
                naam = lijst[i].naam;
                startx = lijst[i].start.X;
                starty = lijst[i].start.Y;

                if (naam == "LijnTool" || naam == "PenTool")
                {
                    //startx = lijst[i].start.X;
                    //starty = lijst[i].start.Y;

                    lijst[i].start.X = starty;
                    lijst[i].start.Y = startx;

                    eindx = lijst[i].eind.X;
                    eindy = lijst[i].eind.Y;
                    lijst[i].eind.X = eindy;
                    lijst[i].eind.Y = eindx;
                }

                else if (naam == "TekstTool")
                {
                    //startx = lijst[i].start.X;
                    //starty = lijst[i].start.Y;
                    lijst[i].start.X = starty;
                    lijst[i].start.Y = startx;
                }

                else
                {
                    if (roteer % 2 != 0)
                    {
                        Console.WriteLine("oneven");
                        startx = lijst[i].rect.X;
                        starty = lijst[i].rect.Y;
                        lijst[i].rect.X = starty;
                        lijst[i].rect.Y = startx;

                        sizex = lijst[i].rect.Width;
                        sizey = lijst[i].rect.Height;
                        lijst[i].rect.Width = sizey;
                        lijst[i].rect.Height = sizex;
                    }
                    else
                    {
                        Console.WriteLine("even"); 
                        lijst[i].rect.X = startx;
                        lijst[i].rect.Y = starty;
                        lijst[i].rect.X = this.Width - lijst[i].rect.X - lijst[i].rect.Width;

                    }

                }

                roteer++;
                Console.WriteLine(roteer);
            }
                
                this.Invalidate();
        }
        public void VeranderKleur(object obj, EventArgs ea)
        {
            string kleurNaam = ((ComboBox)obj).Text;
            penkleur = Color.FromName(kleurNaam);
        }
        public void VeranderKleurViaMenu(object obj, EventArgs ea)
        {
            string kleurNaam = ((ToolStripMenuItem)obj).Text;
            penkleur = Color.FromName(kleurNaam);
        }
    }
}