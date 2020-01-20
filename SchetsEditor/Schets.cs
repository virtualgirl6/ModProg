using System;
using System.Drawing;

namespace SchetsEditor
{
    public class Schets
    {
        private Bitmap bitmap;
        Bitmap nieuw; 
        public Schets()
        {
            bitmap = new Bitmap(1, 1);
        }
        public Graphics BitmapGraphics
        {
            get
            {
                return Graphics.FromImage(bitmap);
                //Graphics g = Graphics.FromImage(bitmap);
                //g.Clear(Color.White);
                //return g;
            }
        }
        public void VeranderAfmeting(Size sz)
        {
            if (sz.Width > bitmap.Size.Width || sz.Height > bitmap.Size.Height)
            {
                Bitmap nieuw = new Bitmap(Math.Max(sz.Width, bitmap.Size.Width)
                                         , Math.Max(sz.Height, bitmap.Size.Height)
                                         );
                Graphics gr = Graphics.FromImage(nieuw);
                gr.FillRectangle(Brushes.White, 0, 0, sz.Width, sz.Height);
                //gr.DrawImage(bitmap, 0, 0);
                bitmap = nieuw;
            }
        }

        public void NieuweBitmap() //eigen
        {
            
            nieuw = new Bitmap(bitmap.Size.Width, bitmap.Size.Height);
            bitmap = nieuw;

        }

        public void TekenRecht(Graphics gr, Pen p, Rectangle r)
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.DrawRectangle(p, r);
        }

        public void TekenRechtVol(Graphics gr, Brush b, Rectangle r)
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.FillRectangle(b, r);
        }

        public void TekenLijn(Graphics gr, Pen p, Point p1, Point p2) //te hoekig!
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.DrawLine(p, p1, p2);
        }
        public void TekenPen(Graphics gr, Pen p, Point p1, Point p2)
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.DrawLine(p, p1, p2); //denk ik
        }

        public void TekenCirkel(Graphics gr, Pen p, Rectangle r)
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.DrawEllipse(p, r);
        }

        public void TekenCirkelVol(Graphics gr, Brush b, Rectangle r)
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.FillEllipse(b, r);
        }

        public void TekenTekst(Graphics gr, Brush b, Point p1, String t)
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.DrawString(t, new Font("Tahoma", 40), new SolidBrush(Color.Black), p1.X, p1.Y);
        }



        public void Weg()
        {
            Graphics g = Graphics.FromImage(bitmap);
            Color c = Color.White;
            g.Clear(c);
        }

        public void SchoonSchets()
        {
            Graphics gr = Graphics.FromImage(bitmap);
            Color c = Color.White;
            gr.Clear(c);

            //Graphics gr = Graphics.FromImage(bitmap);
            //gr.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
        }
    }
}