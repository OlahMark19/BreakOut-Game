using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakOut_Game
{
    public partial class Form1 : Form
    {
        bool goLeft;
        bool goRight;
        bool gameIsOver;


        int score;
        int ballx;
        int bally;
        int playerSpeed;
        int lenn;


        Random rnd = new Random();

        PictureBox[] blockArray;

        public Form1()
        {
            InitializeComponent();

            PlaceBlocks();
        }

        private void setupGame()
        {
            gameIsOver = false;
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 15;

            ball.Left = 380;
            ball.Top = 428;

            player.Left = 295;


            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }

           
        }
        private void gameOver(string message)
        {
            gameIsOver = true;
            gameTimer.Stop();

            label1.Text = message;
            label1.Visible = true;
        }

        private void PlaceBlocks()
        {
            blockArray = new PictureBox[45];

            int col = 0;
            int row = 0;

            int top = 60;
            int left = 23;

            for(int i = 0; i < blockArray.Length; i++)
            {
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 15;
                blockArray[i].Width = 95;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;


                if (col == 7)
                {
                    top = top + 20;
                    left = 23;
                    col = 0;
                    row++;
                }
                if (col < 7)
                {
                    col++;
                    blockArray[i].Top = top;
                    blockArray[i].Left = left;
                    this.Controls.Add(blockArray[i]);
                    left = left + 110;
                }
               
            }
            setupGame();

           
        }

        private void removeBlocks()
        {
            foreach(PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }
        }

        private void mainGameTimeEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;     

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true && player.Left < 625)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 774)
            {
                ballx = -ballx;
            }
            if(ball.Top < 0)
            {
                bally = -bally;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ball.Top = 463;
                bally = rnd.Next(5, 12) * -1;

                if(ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

            if(ball.Top > 485)
            {
                gameOver("Game Over! Press Enter to restart.");
            }


            if (score == 45)
            {
                gameOver("You Won! Press Enter to play again!");
            }

           
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
           
            if(e.KeyCode == Keys.Enter)
            {
                gameTimer.Start();
                label1.Visible = false;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if(e.KeyCode == Keys.Enter && gameIsOver == true)
            {
                removeBlocks();
                PlaceBlocks();
            }
        }


        
    }
}
