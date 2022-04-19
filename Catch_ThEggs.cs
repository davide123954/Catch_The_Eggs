using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Catch_The_Eggs
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight;
        int speed = 8;
        int score = 0;
        int missed = 0;
        Random randX = new Random();
        Random randY = new Random();
        PictureBox splash = new PictureBox();
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblScore.Text = "Saved: " + score;
            lblMissed.Text = "Missed: " + missed;

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= 12;
                player.Image = Properties.Resources.chicken_normal2;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 12;
                player.Image = Properties.Resources.chicken_normal;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top += speed;

                    if (x.Top + x.Height > this.ClientSize.Height)
                    {
                        splash.Image = Properties.Resources.splash;
                        splash.Location = x.Location;
                        splash.Height = 80;
                        splash.Width = 80;
                        splash.BackColor = Color.Transparent;
                        this.Controls.Add(splash);

                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        missed++;
                    }
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        score++;
                    }
                }
            }
            if (score > 10)
            {
                speed = 10;
            }
            if (missed > 5)
            {
                player.Image = Properties.Resources.chicken_hurt;
                timer1.Stop();
                MessageBox.Show("Game Over!!!", "Catch The Eggs Game", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FileStream g = new FileStream("eggs.txt", FileMode.Append);
                StreamWriter m = new StreamWriter(g);
                m.WriteLine("The file contains the score of eggs you saved");
                m.WriteLine("Saved eggs are :  " + lblScore.Text.ToString());
                m.WriteLine("For all games that you play,you cant missed motre then 5 eggs.....");
                m.WriteLine("==========================================================");
                m.Close();
                g.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goRight = false;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goRight = true;
            }
        }
        private void RestartGame()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                }
            }

            player.Left = this.ClientSize.Width / 2;
            player.Image = Properties.Resources.chicken_normal;

            score = 0;
            missed = 0;
            speed = 8;
            goLeft = false;
            goRight = false;
            timer1.Start();
        }
    }

}
