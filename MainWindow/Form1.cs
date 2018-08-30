using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        bool goleft, goright;
        bool ispressed = false; //prevent spamming space button
        int score = 0;
        int speed = 5;
        int totalEnemies = 12; 
        int playerspeed = 6; //pixels

        public Form1()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (goleft) { Player.Left -= playerspeed;  }
            if (goright) { Player.Left += playerspeed; }
            
            foreach(Control x in this.Controls) { 
                if (x is PictureBox && x.Tag == "Platform") { 
                    if (Player.Bounds.Intersectswith(x.Bounds)&& !jumping)
                    {
                        Force = 8;
                        Player.Top = x.Top - Player.Height;
                    }
         


        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goleft = true; }
            if (e.KeyCode == Keys.Right) { goright = true; }
            if (e.KeyCode == Keys.Space && !ispressed)
            {
                ispressed = true;
                makeBullet();
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goleft = false; }
            if (e.KeyCode == Keys.Right) { goright = false; }
            if (ispressed){ ispressed  = false};
        }

        private void makeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.bullet;
            bullet.Size = new Size(5, 20);
            bullet.Tag = "bullet";
            bullet.Left = Player.Left + Player.Width / 2;
            bullet.Top = Player.Top - 20;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void GameOver()
        {
            timer1.Stop();
            label1.Text += "Game Over";
        }
    }
}
