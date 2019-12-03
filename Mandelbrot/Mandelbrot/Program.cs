using System;
using System.Drawing;
using System.Windows.Forms;


namespace Mandelbrot
{

    class Mandel : Form
    {

        ComboBox menu = new ComboBox();

        Button okButton = new Button();
        

        TextBox txtMiddenX = new TextBox();
        TextBox txtMiddenY = new TextBox();
        TextBox txtSchaal = new TextBox();
        TextBox txtMax = new TextBox();

        Label midXLabel = new Label();
        Label midYLabel = new Label();
        Label schaalLabel = new Label();
        Label maxLabel = new Label();

        Panel panel = new Panel();

        int maxIteratie;
        double xMidden, yMidden, zoomfactor, x, y;
        double huidigeMidX = 0; double huidigeMidY = 0;

        double ratio;

        Cursor wait = Cursors.WaitCursor;
        Cursor def = Cursors.Default;

        public Mandel()
        {
            this.Text = "Mandelbrot";
            this.Size = new Size(700, 700);
            this.BackColor = Color.AliceBlue;
            
            MinimizeBox = false;
            MaximizeBox = false;

            //Opmaak OK knop
            int txtHoogte = 30;
            int txtboxLengte = 100;
            okButton.Location = new Point(600, 100);
            okButton.Size = new Size(30, txtHoogte);
            okButton.Text = "OK";

            //Opmaak Midden X
            midXLabel.Location = new Point(10, 30);
            midXLabel.Size = new Size(70, txtHoogte);
            midXLabel.Text = "Midden X:";

            txtMiddenX.Location = new Point(80, 30);
            txtMiddenX.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak Midden Y
            midYLabel.Location = new Point(10, 70);
            midYLabel.Size = new Size(70, txtHoogte);
            midYLabel.Text = "Midden Y:";

            txtMiddenY.Location = new Point(80, 70);
            txtMiddenY.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak Schaal
            schaalLabel.Location = new Point(220, 30);
            schaalLabel.Size = new Size(35, txtHoogte);
            schaalLabel.Text = "Schaal:";

            txtSchaal.Location = new Point(270, 30);
            txtSchaal.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak max
            maxLabel.Location = new Point(220, 70);
            maxLabel.Size = new Size(30, txtHoogte);
            maxLabel.Text = "Max:";

            txtMax.Location = new Point(270, 70);
            txtMax.Size = new Size(txtboxLengte, txtHoogte);

            //opmaak panel
            panel.Size = new Size(500, 500);
            panel.Location = new Point(100, 150);

            //opmaak menu
            menu.Location = new Point(10, 200);
            menu.Size = new Size(20, 40);
            //
            //menu.Items//

            Controls.Add(menu);
            Controls.Add(panel);
            Controls.Add(midXLabel);
            Controls.Add(txtMiddenX);
            Controls.Add(midYLabel);
            Controls.Add(txtMiddenY);
            Controls.Add(schaalLabel);
            Controls.Add(txtSchaal);
            Controls.Add(maxLabel);
            Controls.Add(txtMax);
            Controls.Add(okButton);

            // beginsettings
            maxIteratie = 50;
            xMidden = 250.0;
            yMidden = 250.0;
            zoomfactor = 0.01;
            ratio = zoomfactor / 0.01;

            //eventhandlers
            panel.Paint += Tekenmap;
            okButton.Click += KlikOK;
            panel.MouseClick += KlikScherm;
            //panel. += Klikdubbel;
            



            this.KeyDown += KlikEnter; 
            
        }

        public void KlikOK(object o, EventArgs e)
        {

            try
            {
                zoomfactor = Convert.ToDouble(txtSchaal.Text);
                //zoomfactor = double.Parse(txtSchaal.Text);
                xMidden = Convert.ToDouble(txtMiddenX.Text);
                yMidden = Convert.ToDouble(txtMiddenY.Text);
                maxIteratie = Convert.ToInt32(txtMax.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            panel.Invalidate();
        }

        public void KlikScherm(object o, MouseEventArgs e)
        {
            Console.WriteLine("x1: " + xMidden + "y" + yMidden + " mx " + e.Location.X + " my" + e.Location.Y);


            double mandelMuisX;
            double mandelMuisY;

            zoomfactor /= 2;

            ratio = zoomfactor /0.01;

            mandelMuisX = (e.Location.X + huidigeMidX);
            mandelMuisY = (e.Location.Y + huidigeMidY);


            huidigeMidX = mandelMuisX - (panel.Width);
            huidigeMidY = mandelMuisY - (panel.Height);

            xMidden = ((panel.Width) - mandelMuisX);
            yMidden = ((panel.Height) - mandelMuisY);

            txtMiddenX.Text = Convert.ToString(xMidden);
            txtMiddenY.Text = Convert.ToString(yMidden);
            txtSchaal.Text = Convert.ToString(zoomfactor);

            panel.Invalidate();

        }

        public void Klikdubbel(object o, MouseEventArgs e) //misschien - knopje?
        {
            zoomfactor /= 2;
            txtSchaal.Text = Convert.ToString(zoomfactor);
            panel.Invalidate();



        }

        public void KlikEnter(object o, KeyEventArgs kea)
        {
            try
            {
                if (kea.KeyCode == Keys.Enter)
                    KlikOK(o, kea);
            }
            catch
            {
                Console.WriteLine("hey");
            }
        }
            

        public void Tekenmap(Object obj, PaintEventArgs pea)
        {
            
            for (x = 0; x < panel.Width; x++)
            {
                for (y = 0; y < panel.Height; y++)
                {
                    double a = 0;
                    double b = 0;
                    double aa;
                    double bb;

                    double xzoom = ((x - xMidden) * zoomfactor) ;
                    double yzoom = ((yMidden - y) * zoomfactor) ; //!!
                    
                    for (int iteratie = 0;  iteratie <= maxIteratie; iteratie ++)
                    {
                        
                        aa = a * a - b * b + xzoom;
                        bb = 2 * a * b + yzoom;
                        

                        double pythagoras = a * a + b * b;
                        double afstand = Math.Sqrt(pythagoras);
                        
                        a = aa;
                        b = bb;


                        if (afstand > 2)
                        {
                            if (iteratie % 2 == 0)
                                pea.Graphics.FillRectangle(Brushes.DarkRed, (int)x, (int)y, 1, 1);
                                
                            if (iteratie%2 != 0)
                                pea.Graphics.FillRectangle(Brushes.Black, (int)x, (int)y, 1, 1);
                            break;
                        }
                        

                        

                    }
                }
            }
            
            
        }
    }


    class MainClass
    {
        public static void Main()
        {
            
            Application.Run(new Mandel());
        }
    }
}