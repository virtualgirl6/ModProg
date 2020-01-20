using System;
using System.Drawing;
using System.Drawing.Drawing2D;

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

    
    public class ObjectVorm
    {
        public string naam;
        public Point start;
        public Point eind;
        public Brush kwast;
        public Pen pen;
        public Rectangle rect;
        public string tekst;


        public void Eigenschap(String n, Pen p, Rectangle r)
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

        public void Eigenschap(String n, Pen p, Point p1, string t) 
        {
            naam = n;
            start = p1;
            pen = p;
            tekst = t;
        }

        public void Rect(Rectangle xr) //? weg?
        {
            rect = xr;
        }

        public void Toevoeg(SchetsControl s)
        {
            s.lijst.Add(this);
            //s.VoegToe(this); //alles toevoegen aan de lijst
        }

        public void Haalweg(SchetsControl s, int i)
        {
            //s.Weghaal(s.lijst[i]);
            s.lijst.RemoveAt(i);
            
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
                SizeF sz = new SizeF(30, 64);
                //gr.MeasureString(tekst, font, this.startpunt, StringFormat.GenericTypographic);
                gr.DrawString(tekst, font, kwast,
                                              this.startpunt, StringFormat.GenericTypographic);
                //gr.DrawRectangle(Pens.Black, startpunt.X, startpunt.Y, sz.Width, sz.Height); ?????
                startpunt.X += (int)sz.Width;

                Console.WriteLine(startpunt.X + " + "  + sz);
                
                var obj = new ObjectVorm();
                obj.Eigenschap(this.GetType().Name, new Pen(kwast), new Point(startpunt.X, startpunt.Y), tekst);//, this.startpunt, tekst);
                obj.Toevoeg(s);

                s.Invalidate();
            }
        }

        public override void MuisLos(SchetsControl s, Point p) //eigen
        {
            base.MuisLos(s, p);
            Console.WriteLine("hier"
                );
            /*if (this.GetType().Name == "GumTool")
            {
                Console.WriteLine(p.X + " ... " + p.Y);
                GumTool g = new GumTool();
                //g.Gum(p.X, p.Y, s);
                
                s.Invalidate();
            }*/
            
        }
    }

    public abstract class TweepuntTool : StartpuntTool
    {
        public ObjectVorm o = new ObjectVorm();

        public Rectangle Punten2Rechthoek(Point p1, Point p2)
        {
            Rectangle r = new Rectangle(new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y))
                                , new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y)));

            o.Rect(r);

            return r;
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


            //toevoegen eigenschappen aan lijst
            if (this.GetType().Name != "GumTool")
            {
                if (this.GetType().Name == "VolRechthoekTool" || this.GetType().Name == "VolCirkelTool")
                {
                    var obj = new ObjectVorm(); //nieuw object gemaakt anders wordt de andere overschreven
                    obj.Eigenschap(this.GetType().Name, kwast, Punten2Rechthoek(this.startpunt, p));
                    obj.Toevoeg(s);
                }
                else if (this.GetType().Name == "PenTool" || this.GetType().Name == "LijnTool")
                {
                    var obj = new ObjectVorm();
                    obj.Eigenschap(this.GetType().Name, MaakPen(kwast, 2), this.startpunt, p);
                    obj.Toevoeg(s);
                }

                else
                {
                    var obj = new ObjectVorm();
                    obj.Eigenschap(this.GetType().Name, MaakPen(kwast, 2), Punten2Rechthoek(this.startpunt, p));
                    obj.Toevoeg(s);
                }
            }
            else
            {
                GumTool g = new GumTool();
                g.Gum(p.X, p.Y, s);
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

    public class GumTool : TweepuntTool 
    {
        public override string ToString() { return "gum"; }

        public override void MuisLos(SchetsControl s, Point p)
        {

            base.MuisLos(s, p);  

        }
        public override void Bezig(Graphics g, Point p1, Point p2) 
        {
            
        }

        public void Gum(int px, int py, SchetsControl s)
        {
            Console.WriteLine($"COUNT {s.lijst.Count}");
            for (int i = 0; i < s.lijst.Count; i++)
            {
                if (s.lijst[i].naam == "LijnTool" || s.lijst[i].naam == "PenTool")
                {


                }
                else if (s.lijst[i].naam == "TekstTool")
                {
                    if (px > s.lijst[i].start.X && px < s.lijst[i].start.X + 30 &&
                        py > s.lijst[i].start.Y && py < s.lijst[i].start.Y + 64)
                    {
                        o.Haalweg(s, i);
                        i--;
                    }
                }
                else if (s.lijst[i].naam == "VolCirkelTool" )//|| s.lijst[i].naam == "CirkelTool") 
                {
                    Console.WriteLine("hierzo");
                    int y;
                    int y2;
                    int x = s.lijst[i].rect.X + (s.lijst[i].rect.Width * (1/ 4));
                    int x2 = s.lijst[i].rect.X + (s.lijst[i].rect.Width * (3/4));
                    int x3 = s.lijst[i].rect.X + (s.lijst[i].rect.Width * (1 / 4));
                    int x4 = s.lijst[i].rect.X + (s.lijst[i].rect.Width * (3 / 4));

                    for (y = s.lijst[i].rect.Y; y < ((s.lijst[i].rect.Height / 4) + s.lijst[i].rect.Y); y++)
                    { x--; x2++;
                        for (y2 = s.lijst[i].rect.Height + s.lijst[i].rect.Y; y2 < ((s.lijst[i].rect.Height * 3 / 4) + s.lijst[i].rect.Y); y2--)
                        {
                            x3--; x4++;
                            Console.WriteLine(x + " " + x2 + "  " + y + "  " + y2);
                            if (px > x && px < x2 && px > x3 && px < x4 && py > y && py > y2)
                            {
                                Console.WriteLine("xx");
                                o.Haalweg(s, i); i--;
                            }
                        }
                    }
                        
                    

                    
                    /*f (px > x && px < x2 && px > x3 && px < x4 && py > y && py > y2)
                    {
                        Console.WriteLine("xx");
                        o.Haalweg(s, i); i--;
                        //if (s.lijst[i].naam == "VolCirkelTool")

                            /*Console.WriteLine("print");
                            if (s.lijst[i].naam == "CirkelTool")
                            {
                                if (px < s.lijst[i].rect.X + s.lijst[i].rect.X * (1 / 4) && px > s.lijst[i].rect.X + s.lijst[i].rect.X * (3 / 4)
                                    && py < s.lijst[i].rect.Y + s.lijst[i].rect.Y * (1 / 4) && py > s.lijst[i].rect.X + s.lijst[i].rect.Y * (3 / 4))
                                { o.Haalweg(s, i); i--; }
                            }
                        //else {  }

                    }*/
                       
                        
                }

                else 
                {
                    if (px > s.lijst[i].rect.X && px < (s.lijst[i].rect.Width + s.lijst[i].rect.X) &&
                      py > s.lijst[i].rect.Y && py < (s.lijst[i].rect.Height + s.lijst[i].rect.Y))
                    {
                        if (s.lijst[i].naam == "VolRechthoekTool")
                        {
                            o.Haalweg(s, i);

                            Console.WriteLine($"haalweggum {i}");
                            i--;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"niet weggehaald {i}");
                    }
                }
            }

            s.Invalidate();
        }
    }

    public class CirkelTool : RechthoekTool
    {
        public override string ToString() { return "cirkel"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawEllipse(MaakPen(kwast, 3), Punten2Rechthoek(p1, p2));
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