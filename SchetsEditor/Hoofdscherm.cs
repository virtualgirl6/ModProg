using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace SchetsEditor
{
    public class Hoofdscherm : Form
    {
        MenuStrip menuStrip;

        public Hoofdscherm()
        { this.ClientSize = new Size(800, 600);
            menuStrip = new MenuStrip();
            this.Controls.Add(menuStrip);
            this.maakFileMenu();
            this.maakHelpMenu();
            this.Text = "Schets editor";
            this.IsMdiContainer = true;
            this.MainMenuStrip = menuStrip;

        }
        private void maakFileMenu()
        { ToolStripDropDownItem menu;
            ToolStripMenuItem subopen = new ToolStripMenuItem("Open");
            //foreach (string f in files) 
            //  subopen.DropDownItems.Add(f, null, this.openen);

            menu = new ToolStripMenuItem("File");
            menu.DropDownItems.Add("Nieuw", null, this.nieuw);
            menu.DropDownItems.Add(subopen);
            menu.DropDownItems.Add("Opslaan", null, opslaan);    //!
            menu.DropDownItems.Add("Exit", null, this.afsluiten);
            menuStrip.Items.Add(menu);
        }
        private void maakHelpMenu()
        { ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("Help");
            menu.DropDownItems.Add("Over \"Schets\"", null, this.about);
            menuStrip.Items.Add(menu);
        }
        private void about(object o, EventArgs ea)
        { MessageBox.Show("Schets versie 1.0\n(c) UU Informatica 2010"
                         , "Over \"Schets\""
                         , MessageBoxButtons.OK
                         , MessageBoxIcon.Information
                         );
        }

        private void nieuw(object sender, EventArgs e)
        { SchetsWin s = new SchetsWin();
            s.MdiParent = this;

            s.Show();

        }
        private void afsluiten(object sender, EventArgs e)
        { this.Close();
        }
        private void openen(object sender, EventArgs e)
        {
            string filenaam = ((ToolStripMenuItem)sender).Text;
            StreamReader sr = new StreamReader(filenaam);

            //filenaam = sr.ReadToEnd;

            nieuw(sender, e);
            sr.Close();
        }

        public static void opslaan(object sender, EventArgs e)
        {
            
            Dialoog d = new Dialoog();


            string pad = d.s;


            StreamWriter sw = new StreamWriter(pad);
            sw.Write(sender);

            //sw.Write(this.Invoer.Text);
            sw.Close();
        }
    }

    public class Dialoog : Form
    {
        TextBox invoer; 
        Button b;
        Label l;
        public string s;

        public Dialoog()
        {
            this.Size = new Size(100, 100);

            invoer = new TextBox();
            b = new Button();

            s = invoer.Text;
            b.Text = "opslaan";


            this.Controls.Add(invoer);
            this.Controls.Add(b);
        }
    }
}
