using System;
using System.Drawing;
using System.Windows.Forms;

/* -----------------
 * TO DO

    klik & Zoom werkend (meeschalen)

    variabelnamen waar nodig duidelijker
    KLeuren methode + implementatie
    box dropdown
        presets in box dropdown

    extras (e.g. met + en - knopje zoomen */





namespace Mandelbrot
{

    class Mandel : Form
    {

        Button okButton = new Button();
        Button zoomButton = new Button();

        TextBox txtboxMidX = new TextBox();
        TextBox txtboxMidY = new TextBox();
        TextBox txtboxSchaal = new TextBox();
        TextBox txtboxMax = new TextBox();

        Label midXLabel = new Label();
        Label midYLabel = new Label();
        Label schaalLabel = new Label();
        Label maxLabel = new Label();
        Label zoomtxt = new Label();

        Panel panel = new Panel();

        ComboBox menu = new ComboBox();
       

        int maxIteratie;
        double xMidden, yMidden, zoomfactor, x, y;
        double absMiddenX, absMiddenY, zoomAan;
        
        bool boolZoom = false;



        public Mandel()
        {
            this.Text = "Mandelbrot";
            this.Size = new Size(600, 700);
            this.BackColor = Color.AliceBlue;
            
            MinimizeBox = false;
            MaximizeBox = false;

            //Opmaak OK knop
            int txtHoogte = 30;
            int txtboxLengte = 100;
            okButton.Location = new Point(360, 40);
            okButton.Size = new Size(30, txtHoogte);
            okButton.Text = "OK";

            //Opmaak Midden X
            midXLabel.Location = new Point(10, 30);
            midXLabel.Size = new Size(70, txtHoogte);
            midXLabel.Text = "Midden X:";

            txtboxMidX.Location = new Point(80, 30);
            txtboxMidX.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak Midden Y
            midYLabel.Location = new Point(10, 60);
            midYLabel.Size = new Size(70, txtHoogte);
            midYLabel.Text = "Midden Y:";

            txtboxMidY.Location = new Point(80, 60);
            txtboxMidY.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak Schaal
            schaalLabel.Location = new Point(200, 30);
            schaalLabel.Size = new Size(35, txtHoogte);
            schaalLabel.Text = "Schaal:";

            txtboxSchaal.Location = new Point(250, 30);
            txtboxSchaal.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak max
            maxLabel.Location = new Point(200, 60);
            maxLabel.Size = new Size(30, txtHoogte);
            maxLabel.Text = "Max:";

            txtboxMax.Location = new Point(250, 60);
            txtboxMax.Size = new Size(txtboxLengte, txtHoogte);

            //Opmaak panel
            panel.Size = new Size(500, 500);
            panel.Location = new Point(50, 150);

            //Opmaak menu
            menu.Location = new Point(450, 30);
            menu.Size = new Size(txtboxLengte, 60);
            //
            menu.Items.AddRange(new object[] {"Basis",
                        "Zoom",
                        "Item 3",
                        "Item 4"});

            //Opmaak zoomButton 
            zoomtxt.Location = new Point(35, 125);
            zoomtxt.Size = new Size(50, txtHoogte);
            zoomtxt.Text = "AutoZoom:";
            zoomButton.Location = new Point(50, 95);
            zoomButton.Size = new Size(20, 20);
            zoomButton.Text = " ";

            

            Controls.Add(menu);
            Controls.Add(panel);
            Controls.Add(midXLabel);
            Controls.Add(txtboxMidX);
            Controls.Add(midYLabel);
            Controls.Add(txtboxMidY);
            Controls.Add(schaalLabel);
            Controls.Add(txtboxSchaal);
            Controls.Add(maxLabel);
            Controls.Add(txtboxMax);
            Controls.Add(okButton);
            Controls.Add(zoomButton);
            Controls.Add(zoomtxt);            

            // beginsettings
            maxIteratie = 50;
            startPointX = 250;
            startPointY = 250;

            absMiddenX = 0;
            absMiddenY = 0;

            zoomfactor = 0.01;
            zoomAan = 1;

            //eventhandlers
            panel.Paint += Tekenmap;
            panel.MouseClick += KlikScherm;
            okButton.Click += KlikOK;
            zoomButton.Click += klikZoom;
            menu.SelectedIndexChanged += SelectItem;

        }

        public void klikZoom(object o, EventArgs e)
        {
            if (!boolZoom)
            {
                boolZoom = !boolZoom;
                zoomButton.Text = "X";
                zoomAan = 2;
            }
            else
            {
                boolZoom = !boolZoom;
                zoomButton.Text = " ";
                zoomAan = 1;
            }
        }

        public void KlikOK(object o, EventArgs e)
        {

            try
            {
                zoomfactor = Convert.ToDouble(txtboxSchaal.Text);
                startPointX = Convert.ToDouble(txtboxMidX.Text);
                startPointY = Convert.ToDouble(txtboxMidY.Text);
                maxIteratie = Convert.ToInt32(txtboxMax.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            panel.Invalidate();
        }



        public void KlikScherm(object o, MouseEventArgs e)
        {
            
            absMiddenX = (absMiddenX - (250 - e.Location.X)) * zoomAan;
            absMiddenY = ( (250 - e.Location.Y) - absMiddenY) * zoomAan;

            zoomfactor /= zoomAan;

            panel.Invalidate();

            txtboxMidX.Text = Convert.ToString(absMiddenX*zoomfactor);
            txtboxMidY.Text = Convert.ToString(absMiddenY*zoomfactor);
            txtboxSchaal.Text = Convert.ToString(zoomfactor);
        }




        public void Klikdubbel(object o, MouseEventArgs e) //misschien - knopje?
        {
            zoomfactor *= 2;
            txtboxSchaal.Text = Convert.ToString(zoomfactor);
            panel.Invalidate();
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

                    for (int iteratie = 0;  iteratie <= maxIteratie; iteratie ++)
                    {
                        double xzoom = ((absMiddenX + x) - panel.Width/2) * zoomfactor;
                        double yzoom = ((y - absMiddenY) - panel.Height/2) * zoomfactor;
                        
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

                        //(mandelgetal / (int)max_iteration) * 255)
                    }
                }
            } 
        }

        private void SelectItem(object sender, EventArgs e)
        {

            int gekozenItem = menu.SelectedIndex;



            if (gekozenItem == 0)
            {
                absMiddenX = 0;
                absMiddenY = 0;
                zoomfactor = 0.01;
                maxIteratie = 100;

                panel.Invalidate();

            }

            if (gekozenItem == 1)
            {
                absMiddenX = 100;
                absMiddenY = 100;
                zoomfactor = 0.001;
                maxIteratie = 100;

                panel.Invalidate();

            }

            if (gekozenItem == 2)
            {

                panel.Invalidate();
            }

            if (gekozenItem == 3)
            {
                panel.Invalidate();
            }




        }

        void Kleur(int i, byte r, byte g, byte b)
        {
            //maxkleur = 255;

            byte Rood = r;
            byte Groen = g;
            byte Blauw = b;

            Color.FromArgb(r, g, b);




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