using System;
//using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
//using SchetsEditor;

namespace SchetsEditor
{

    public interface ISchetsTool
    {
        void MuisVast(SchetsControl s, Point p);
        void MuisDrag(SchetsControl s, Point p);
        void MuisLos(SchetsControl s, Point p);
        void Letter(SchetsControl s, char c);
    }

    public abstract class StartpuntTool : ISchetsTool
    {
        protected Point startpunt;
        protected Brush kwast;

        public virtual void MuisVast(SchetsControl s, Point p)
        {
            startpunt = p;
        }
        public virtual void MuisLos(SchetsControl s, Point p)
        {
            kwast = new SolidBrush(s.PenKleur);
        }
        public abstract void MuisDrag(SchetsControl s, Point p);
        public abstract void Letter(SchetsControl s, char c);
    }


    //eigen!  Methode om size in double te maken 
    /*public class DoubleSize
    {

        public double Width;
        public double Height;

        public DoubleSize(double width, double height)
        {
            Width = width;
            Height = height;

        }

        public override string ToString()
        {
            return Width.ToString() + "," + Height.ToString();

        }
    }*/


    //eigen! wordt in tweepunttool aangeroepen
    public class ObjectVorm
    {
        public string naam;
        public Point start;
        public Point eind;
        public Brush kwast;
        public Pen pen;
        //public DoubleSize Grootte;
        public Rectangle rect;
        

        public void Eigenschap(String n, Pen p, Rectangle r) //pen of brush?
        {
            naam = n;
            rect = r;
            pen = p;
        }


        public void Eigenschap(String n, Brush b, Rectangle r)
        {
            naam = n;
            rect = r;
            kwast = b;
        }

        public void Eigenschap(String n, Brush b, Point p1, Point p2)
        {
            naam = n;
            start = p1;
            eind = p2;
            kwast = b;
        }

        public void Eigenschap(String n, Pen p, Point p1, Point p2)
        {
            naam = n;
            start = p1;
            eind = p2;
            pen = p;
        }


        /*public void Eigenschap(String naam, Point start, Brush b, DoubleSize grootte)
        {
            Grootte = grootte;
            Eigenschap(naam, start, b);

            Console.WriteLine("xy" + Naam + start + b + grootte);

        }*/

        public void Rect(Rectangle xr)
        {
            rect = xr;
        }

        public void Toevoeg(SchetsControl s)
        {
            s.VoegToe(this); //alles toevoegen aan de lijst
        }

        public void Haalweg(SchetsControl s, int i)
        {
            s.Weghaal(s.lijst[i]);
            Console.WriteLine("weggehaald");
        }
    }


    public class TekstTool : StartpuntTool
    {
        public override string ToString() { return "tekst"; }

        public override void MuisDrag(SchetsControl s, Point p) { }

        public override void Letter(SchetsControl s, char c)
        {
            if (c >= 32)
            {
                Graphics gr = s.MaakBitmapGraphics();
                Font font = new Font("Tahoma", 40);
                string tekst = c.ToString();
                SizeF sz =
                gr.MeasureString(tekst, font, this.startpunt, StringFormat.GenericTypographic);
                gr.DrawString(tekst, font, kwast,
                                              this.startpunt, StringFormat.GenericTypographic);
                // gr.DrawRectangle(Pens.Black, startpunt.X, startpunt.Y, sz.Width, sz.Height);
                startpunt.X += (int)sz.Width;
                s.Invalidate();

            }
        }
    }

    public abstract class TweepuntTool : StartpuntTool
    {
        public ObjectVorm o = new ObjectVorm(); //eigen!
        
        public Rectangle Punten2Rechthoek(Point p1, Point p2)
        {
            Rectangle r = new Rectangle(new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y))
                                , new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y)));

            o.Rect(r);
            
            return r;

            /*return new Rectangle(new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y))
                                , new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y))
                                );*/
        }
        public static Pen MaakPen(Brush b, int dikte)
        {
            Pen pen = new Pen(b, dikte);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }
        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            kwast = Brushes.Gray;
        }
        public override void MuisDrag(SchetsControl s, Point p)
        {
            s.Refresh();
            this.Bezig(s.CreateGraphics(), this.startpunt, p);
        }
        public override void MuisLos(SchetsControl s, Point p)
        {
            base.MuisLos(s, p);
            this.Compleet(s.MaakBitmapGraphics(), this.startpunt, p);

            
            //eigen!!
            if (this.GetType().Name != "GumTool")
            {
                if (this.GetType().Name == "VolRechthoekTool" || this.GetType().Name == "VolCirkelTool")
                {
                    o.Eigenschap(this.GetType().Name, kwast, Punten2Rechthoek(this.startpunt, p));
                    o.Toevoeg(s);
                }

                if (this.GetType().Name == "PenTool" || this.GetType().Name == "LijnTool")
                {
                    o.Eigenschap(this.GetType().Name, MaakPen(kwast, 2), this.startpunt, p);
                    o.Toevoeg(s);
                }
                else
                {
                    o.Eigenschap(this.GetType().Name, MaakPen(kwast, 2), Punten2Rechthoek(this.startpunt, p));
                    o.Toevoeg(s);
                }
            }
            s.Invalidate();
        }

        public override void Letter(SchetsControl s, char c)
        {
        }
        public abstract void Bezig(Graphics g, Point p1, Point p2);

        public virtual void Compleet(Graphics g, Point p1, Point p2)
        {
            this.Bezig(g, p1, p2);
        }
    }

    public class RechthoekTool : TweepuntTool
    {
        public override string ToString() { return "kader"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawRectangle(MaakPen(kwast, 3), Punten2Rechthoek(p1, p2));


            //eigen! groottte bepalen 
            /*double Xgrootte;
            Xgrootte = Math.Abs(p1.X - p2.X);

            double Ygrootte;
            Ygrootte = Math.Abs(p1.Y - p2.Y);

            DoubleSize grootte = new DoubleSize(Xgrootte, Ygrootte);*/


            //o.Eigenschap(this.GetType().Name, startpunt, kwast, Punten2Rechthoek(p1, p2)); //grootte
            //Console.WriteLine("p" + Punten2Rechthoek(p1, p2));
        }
    }

    public class VolRechthoekTool : RechthoekTool
    {
        public override string ToString() { return "vlak"; }

        public override void Compleet(Graphics g, Point p1, Point p2)
        {
            g.FillRectangle(kwast, Punten2Rechthoek(p1, p2));
        }
    }

    public class LijnTool : TweepuntTool
    {
        public override string ToString() { return "lijn"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawLine(MaakPen(this.kwast, 3), p1, p2);
        }
    }

    public class PenTool : LijnTool
    {
        public override string ToString() { return "pen"; }

        public override void MuisDrag(SchetsControl s, Point p)
        {
            this.MuisLos(s, p);
            this.MuisVast(s, p);
        }
    }

    public class GumTool : TweepuntTool //denk subklasse van 2punttool (was eerst pentool)
    {
        public override string ToString() { return "gum"; }

        public override void MuisLos(SchetsControl s, Point p)
        {
            
            base.MuisLos(s, p);
            
            Gum(p.X, p.Y, s);
            //Console.WriteLine("gum2");
        }
        public override void Bezig(Graphics g, Point p1, Point p2) //parameters? 
        {
            //SchetsControl s = new SchetsControl();
            //Console.WriteLine("bezig");
            
            //Gum(p2.X, p2.Y, o.);
            
            // match coordinaat met een uit de lijst, en pak de onderste (als er meer zijn(over elkaar liggen)).
        }

        public void Gum(int px, int py, SchetsControl s)
        {
            Console.WriteLine(s.lijst.Count);
            for (int i = 0; i < s.lijst.Count; i++)
            {
                
                if (px > s.lijst[i].rect.X && px < (s.lijst[i].rect.Width + s.lijst[i].rect.X) &&
                    py > s.lijst[i].rect.Y && py < (s.lijst[i].rect.Height + s.lijst[i].rect.Y))
                {
                    //Console.WriteLine(s.lijst.Count);
                    o.Haalweg(s, i);
                    //Console.WriteLine(s.lijst.Count);
                    Console.WriteLine("haalweggum");
                }
                else
                {
                    Console.WriteLine("geen gum");
                    //Console.WriteLine(s.lijst[i].rect);
                }
            }
            
        }
        public void GumLaatste(SchetsControl s) //UNDO
        {
            s.lijst.RemoveAt(s.lijst.Count - 1);
        }
    }

    public class CirkelTool : RechthoekTool
    {
        public override string ToString() { return "cirkel"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawEllipse(MaakPen(kwast, 3), Punten2Rechthoek(p1, p2));

            /*
            double Xgrootte;
            Xgrootte = Math.Abs(p1.X - p2.X);

            double Ygrootte;
            Ygrootte = Math.Abs(p1.Y - p2.Y);

            DoubleSize grootte = new DoubleSize(Xgrootte, Ygrootte);

            
            //o.Eigenschap(this.GetType().Name, startpunt, kwast, Punten2Rechthoek(p1, p2)); //grootte
            */
        }

    }

    public class VolCirkelTool : RechthoekTool
    {
        public override string ToString() { return "vulCirkel"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.FillEllipse(kwast, Punten2Rechthoek(p1, p2));
        }
    }
}

