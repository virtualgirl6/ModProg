﻿using System;
using System.Drawing;

namespace SchetsEditor
{
    public class Schets
    {
        private Bitmap bitmap;

        public Schets()
        {
            bitmap = new Bitmap(1, 1);
        }
        public Graphics BitmapGraphics
        {
            get { return Graphics.FromImage(bitmap); }
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
                gr.DrawImage(bitmap, 0, 0);
                bitmap = nieuw;
            }
        }
        public void Teken(Graphics gr) //weg?
        {
            gr.DrawImage(bitmap, 0, 0);
            gr.DrawEllipse(Pens.Black, 1, 2, 6, 6);
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
            //gr.DrawImage(bitmap, 0, 0); 
            gr.DrawLine(p, p1, p2);
        }
        public void TekenPen(Graphics gr, Pen p, Point p1, Point p2)
        {
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

        public void Weg()
        {
            Graphics g = Graphics.FromImage(bitmap); 
            Color c = Color.White;
            g.Clear(c);
            
        }

        public void Schoon()
        {
            Graphics gr = Graphics.FromImage(bitmap);
            Color c = Color.White;
            gr.Clear(c);
            //Graphics gr = Graphics.FromImage(bitmap);
            //gr.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
        }

        public void Roteer()
        {
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
    }
}
