using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameSystemServices;

namespace Basic_Game_Template
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;

        //player2 button control keys - DO NOT CHANGE
        Boolean aDown, sDown, dDown, wDown, cDown, vDown, xDown, zDown;

        Boolean playChaser, friend1Chaser, friend2Chaser, friend3Chaser = false;

        Font drawFont = new Font("Arial", 16, FontStyle.Bold);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        SolidBrush winBrush = new SolidBrush(Color.White);

        Boolean loseVar = false;
        Boolean winVar = false;

        Random randGen = new Random();

        int randomValue;

        //TODO create your global game variables here
        int size, runnerSpeed, chaserSpeed;
        int playX, playY, friend1X, friend1Y, friend2X, friend2Y, friend3X, friend3Y;

        SolidBrush chaserBrush = new SolidBrush(Color.Red);
        SolidBrush runnerBrush = new SolidBrush(Color.Blue);

        public GameScreen()
        {
            InitializeComponent();
            InitializeGameValues();
        }

        public void InitializeGameValues()
        {
            //TODO - setup all your initial game values here. Use this method
            // each time you restart your game to reset all values.
            playX = 100;
            playY = 100;

            friend1X = 100;
            friend1Y = 200;

            friend2X = 200;
            friend2Y = 100;

            friend3X = 200;
            friend3Y = 200;

            size = 10;

            runnerSpeed = 7;
            chaserSpeed = 5; 

            randomValue = randGen.Next(1, 101);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // opens a pause screen is escape is pressed. Depending on what is pressed
            // on pause screen the program will either continue or exit to main menu
            if (e.KeyCode == Keys.Escape && gameTimer.Enabled)
            {
                gameTimer.Enabled = false;
                rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

                DialogResult result = PauseForm.Show();

                if (result == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;

                }
                else if (result == DialogResult.Abort)
                {
                    MainForm.ChangeScreen(this, "MenuScreen");
                }
            }

            //TODO - basic player 1 key down bools set below. Add remainging key down
            // required for player 1 or player 2 here.

            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                case Keys.M:
                    mDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //TODO - basic player 1 key up bools set below. Add remainging key up
            // required for player 1 or player 2 here.

            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
            }
        }

        /// <summary>
        /// This is the Game Engine and repeats on each interval of the timer. For example
        /// if the interval is set to 16 then it will run each 16ms or approx. 50 times
        /// per second
        /// </summary>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //TODO move main character 
            if (leftArrowDown == true)
            {
                playX = playX - chaserSpeed;

            }
            if (downArrowDown == true)
            {
                playY = playY + chaserSpeed;
            }
            if (rightArrowDown == true)
            {
                playX = playX + chaserSpeed;
            }
            if (upArrowDown == true)
            {
                playY = playY - chaserSpeed;
            }

            //TODO move npc characters


            if (playX <= 0)
            {
                playX = 0;
            }
            if (playY <= 0)
            {
                playY = 0;
            }
            if (playX >= 300)
            {
                playX = 300;
            }
            if (playY >= 300)
            {
                playY = 300;
            }

            if (friend1X <= 0)
            {
                friend1X = 0;
            }
            if (friend1Y <= 0)
            {
                friend1Y = 0;
            }
            if (friend1X >= 300)
            {
                friend1X = 300;
            }
            if (friend1Y >= 300)
            {
                friend1Y = 300;
            }

            if (friend2X <= 0)
            {
                friend2X = 0;
            }
            if (friend2Y <= 0)
            {
                friend2Y = 0;
            }
            if (friend2X >= 300)
            {
                friend2X = 300;
            }
            if (friend2Y >= 300)
            {
                friend2Y = 300;
            }

            if (friend3X <= 0)
            {
                friend3X = 0;
            }
            if (friend3Y <= 0)
            {
                friend3Y = 0;
            }
            if (friend3X >= 300)
            {
                friend3X = 300;
            }
            if (friend3Y >= 300)
            {
                friend3Y = 300;
            }

            Rectangle play = new Rectangle(playX, playY, size, size);
            Rectangle friend1 = new Rectangle(friend1X, friend1Y, size, size);
            Rectangle friend2 = new Rectangle(friend2X, friend2Y, size, size);
            Rectangle friend3 = new Rectangle(friend3X, friend3Y, size, size);

            Boolean playFriend1Collision = play.IntersectsWith(friend1);
            Boolean playFriend2Collision = play.IntersectsWith(friend2);
            Boolean playFriend3Collision = play.IntersectsWith(friend3);

            Boolean friend1Friend2Collision = friend1.IntersectsWith(friend2);
            Boolean friend1Friend3Collision = friend1.IntersectsWith(friend3);

            Boolean friend2Friend3Collision = friend2.IntersectsWith(friend3);

            if (playFriend1Collision && playChaser == true)
            {
                playChaser = false;
                friend1Chaser = true;
            }
            else if (playFriend1Collision && friend1Chaser == true)
            {
                playChaser = true;
                friend1Chaser = false;
            }

            if (playFriend2Collision && playChaser == true)
            {
                playChaser = false;
                friend2Chaser = true;
            }
            else if (playFriend2Collision && friend2Chaser == true)
            {
                playChaser = true;
                friend2Chaser = false; 
            }

            if (playFriend3Collision && playChaser == true)
            {
                playChaser = false;
                friend3Chaser = true;
            }
            else if (playFriend3Collision && friend3Chaser == true)
            {
                playChaser = true;
                friend2Chaser = false;
            }

            if (friend1Friend2Collision && friend1Chaser == true)
            {
                friend1Chaser = false;
                friend2Chaser = true; 
            }
            else if (friend1Friend2Collision && friend2Chaser == true)
            {
                friend1Chaser = true;
                friend2Chaser = false;
            }

            if (friend1Friend3Collision && friend1Chaser == true)
            {
                friend1Chaser = false;
                friend3Chaser = true;
            }
            else if (friend1Friend3Collision && friend3Chaser == true)
            {
                friend1Chaser = true;
                friend3Chaser = false;
            }

            if (friend2Friend3Collision && friend2Chaser == true)
            {
                friend2Chaser = false;
                friend3Chaser = true;
            }
            else if (friend2Friend3Collision && friend3Chaser == true)
            {
                friend2Chaser = true;
                friend3Chaser = false;
            }

            if (friend1Chaser == true) { friend1chaserMethod(); }
            else
            {
                if (playChaser == true)
                {
                    if (friend1X < playX)
                    {
                        friend1X = friend1X - runnerSpeed;
                    }
                    else
                    {
                        friend1X = friend1X + runnerSpeed;
                    }

                    if (friend1Y < playY)
                    {
                        friend1Y = friend1Y - runnerSpeed;
                    }
                    else
                    {
                        friend1Y = friend1Y + runnerSpeed;
                    }
                }
                else if (friend2Chaser == true)
                {
                    if (friend1X < friend2X)
                    {
                        friend1X = friend1X - runnerSpeed;
                    }
                    else
                    {
                        friend1X = friend1X + runnerSpeed;
                    }

                    if (friend1Y < friend2Y)
                    {
                        friend1Y = friend1Y - runnerSpeed;
                    }
                    else
                    {
                        friend1Y = friend1Y + runnerSpeed;
                    }
                }
                else if (friend3Chaser == true)
                {
                    if (friend1X < friend3X)
                    {
                        friend1X = friend1X - runnerSpeed;
                    }
                    else
                    {
                        friend1X = friend1X + runnerSpeed;
                    }

                    if (friend1Y < friend3Y)
                    {
                        friend1Y = friend1Y - runnerSpeed;
                    }
                    else
                    {
                        friend1Y = friend1Y + runnerSpeed;
                    }
                }
            }

            if (friend2Chaser == true) { friend2chaserMethod(); }
            else {
                if (playChaser == true)
                {
                    if (friend2X < playX)
                    {
                        friend2X = friend2X - runnerSpeed;
                    }
                    else
                    {
                        friend2X = friend2X + runnerSpeed;
                    }

                    if (friend2Y < playY)
                    {
                        friend2Y = friend2Y - runnerSpeed;
                    }
                    else
                    {
                        friend2Y = friend2Y + runnerSpeed;
                    }
                }
                else if (friend2Chaser == true)
                {
                    if (friend2X < friend1X)
                    {
                        friend2X = friend2X - runnerSpeed;
                    }
                    else
                    {
                        friend2X = friend2X + runnerSpeed;
                    }

                    if (friend2Y < friend1Y)
                    {
                        friend2Y = friend2Y - runnerSpeed;
                    }
                    else
                    {
                        friend2Y = friend2Y + runnerSpeed;
                    }
                }
                else if (friend3Chaser == true)
                {
                    if (friend2X < friend3X)
                    {
                        friend2X = friend2X - runnerSpeed;
                    }
                    else
                    {
                        friend2X = friend2X + runnerSpeed;
                    }

                    if (friend2Y < friend3Y)
                    {
                        friend2Y = friend2Y - runnerSpeed;
                    }
                    else
                    {
                        friend2Y = friend2Y + runnerSpeed;
                    }
                }
            }

            if (friend3Chaser == true) { friend3chaserMethod(); }
            else {
                if (playChaser == true)
                {
                    if (friend3X < playX)
                    {
                        friend3X = friend3X - runnerSpeed;
                    }
                    else
                    {
                        friend3X = friend3X + runnerSpeed;
                    }

                    if (friend3Y < playY)
                    {
                        friend3Y = friend3Y - runnerSpeed;
                    }
                    else
                    {
                        friend3Y = friend3Y + runnerSpeed;
                    }
                }
                else if (friend2Chaser == true)
                {
                    if (friend3X < friend2X)
                    {
                        friend3X = friend3X - runnerSpeed;
                    }
                    else
                    {
                        friend3X = friend3X + runnerSpeed;
                    }

                    if (friend3Y < friend2Y)
                    {
                        friend3Y = friend3Y - runnerSpeed;
                    }
                    else
                    {
                        friend3Y = friend3Y + runnerSpeed;
                    }
                }
                else if (friend3Chaser == true)
                {
                    if (friend3X < friend1X)
                    {
                        friend3X = friend3X - runnerSpeed;
                    }
                    else
                    {
                        friend3X = friend3X + runnerSpeed;
                    }

                    if (friend3Y < friend1Y)
                    {
                        friend3Y = friend3Y - runnerSpeed;
                    }
                    else
                    {
                        friend3Y = friend3Y + runnerSpeed;
                    }
                }
            }

            Refresh();
        }


        //Everything that is to be drawn on the screen should be done here
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw rectangle to screen

            e.Graphics.FillRectangle(chaserBrush, playX, playY, size, size);
            e.Graphics.FillRectangle(runnerBrush, friend1X, friend1Y, size, size);
            e.Graphics.FillRectangle(runnerBrush, friend2X, friend2Y, size, size);
            e.Graphics.FillRectangle(runnerBrush, friend3X, friend3Y, size, size);
        }

        public void friend1runnerMethod()
        {
            SolidBrush tagBrush = new SolidBrush(Color.Blue);

            //Moving
            if (playChaser == true)
            {
                if (friend1X < playX)
                {
                    friend1X = friend1X - runnerSpeed;
                }
                else
                {
                    friend1X = friend1X + runnerSpeed;
                }

                if (friend1Y < playY)
                {
                    friend1Y = friend1Y - runnerSpeed;
                }
                else
                {
                    friend1Y = friend1Y + runnerSpeed;
                }
            }
            else if (friend2Chaser == true)
            {
                if (friend1X < friend2X)
                {
                    friend1X = friend1X - runnerSpeed;
                }
                else
                {
                    friend1X = friend1X + runnerSpeed;
                }

                if (friend1Y < friend2Y)
                {
                    friend1Y = friend1Y - runnerSpeed;
                }
                else
                {
                    friend1Y = friend1Y + runnerSpeed;
                }
            }
            else if (friend3Chaser == true)
            {
                if (friend1X < friend3X)
                {
                    friend1X = friend1X - runnerSpeed;
                }
                else
                {
                    friend1X = friend1X + runnerSpeed;
                }

                if (friend1Y < friend3Y)
                {
                    friend1Y = friend1Y - runnerSpeed;
                }
                else
                {
                    friend1Y = friend1Y + runnerSpeed;
                }
            }
        }

        public void friend2runnerMethod()
        {
            SolidBrush tagBrush = new SolidBrush(Color.Blue);

            //Moving
            if (playChaser == true)
            {
                if (friend2X < playX)
                {
                    friend2X = friend2X - runnerSpeed;
                }
                else
                {
                    friend2X = friend2X + runnerSpeed;
                }

                if (friend2Y < playY)
                {
                    friend2Y = friend2Y - runnerSpeed;
                }
                else
                {
                    friend2Y = friend2Y + runnerSpeed;
                }
            }
            else if (friend2Chaser == true)
            {
                if (friend2X < friend1X)
                {
                    friend2X = friend2X - runnerSpeed;
                }
                else
                {
                    friend2X = friend2X + runnerSpeed;
                }

                if (friend2Y < friend1Y)
                {
                    friend2Y = friend2Y - runnerSpeed;
                }
                else
                {
                    friend2Y = friend2Y + runnerSpeed;
                }
            }
            else if (friend3Chaser == true)
            {
                if (friend2X < friend3X)
                {
                    friend2X = friend2X - runnerSpeed;
                }
                else
                {
                    friend2X = friend2X + runnerSpeed;
                }

                if (friend2Y < friend3Y)
                {
                    friend2Y = friend2Y - runnerSpeed;
                }
                else
                {
                    friend2Y = friend2Y + runnerSpeed;
                }
            }
        }

        public void friend3runnerMethod()
        {
            SolidBrush tagBrush = new SolidBrush(Color.Blue);

            //Moving
            if (playChaser == true)
            {
                if (friend3X < playX)
                {
                    friend3X = friend3X - runnerSpeed;
                }
                else
                {
                    friend3X = friend3X + runnerSpeed;
                }

                if (friend3Y < playY)
                {
                    friend3Y = friend3Y - runnerSpeed;
                }
                else
                {
                    friend3Y = friend3Y + runnerSpeed;
                }
            }
            else if (friend2Chaser == true)
            {
                if (friend3X < friend2X)
                {
                    friend3X = friend3X - runnerSpeed;
                }
                else
                {
                    friend3X = friend3X + runnerSpeed;
                }

                if (friend3Y < friend2Y)
                {
                    friend3Y = friend3Y - runnerSpeed;
                }
                else
                {
                    friend3Y = friend3Y + runnerSpeed;
                }
            }
            else if (friend3Chaser == true)
            {
                if (friend3X < friend1X)
                {
                    friend3X = friend3X - runnerSpeed;
                }
                else
                {
                    friend3X = friend3X + runnerSpeed;
                }

                if (friend3Y < friend1Y)
                {
                    friend3Y = friend3Y - runnerSpeed;
                }
                else
                {
                    friend3Y = friend3Y + runnerSpeed;
                }
            }
        }

        public void friend1chaserMethod()
        {
            //Variables
            int heroLoc = playX + playY;
            int mon2Loc = friend1X + friend1Y;

            //Moving
            int heroVar1 = heroLoc - mon2Loc;
            int mon2Var1 = mon2Loc - heroLoc;

            if (heroVar1 > mon2Var1)
            {
                if (friend2X < friend1X)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < friend1Y)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
            else if (heroVar1 < mon2Var1)
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
            else
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
        }

        public void friend2chaserMethod()
        {
            //Variables
            int heroLoc = playX + playY;
            int mon2Loc = friend1X + friend1Y;

            //Moving
            int heroVar1 = heroLoc - mon2Loc;
            int mon2Var1 = mon2Loc - heroLoc;

            if (heroVar1 > mon2Var1)
            {
                if (friend2X < friend1X)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < friend1Y)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
            else if (heroVar1 < mon2Var1)
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
            else
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
        }

        public void friend3chaserMethod()
        {
            //Variables
            int heroLoc = playX + playY;
            int mon2Loc = friend1X + friend1Y;

            //Moving
            int heroVar1 = heroLoc - mon2Loc;
            int mon2Var1 = mon2Loc - heroLoc;

            if (heroVar1 > mon2Var1)
            {
                if (friend2X < friend1X)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < friend1Y)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
            else if (heroVar1 < mon2Var1)
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
            else
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + runnerSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - runnerSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + runnerSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - runnerSpeed - 2;
                }
            }
        }
    }
}

