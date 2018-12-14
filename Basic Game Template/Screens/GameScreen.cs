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

        Font drawFont = new Font("Arial", 16, FontStyle.Bold);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        SolidBrush winBrush = new SolidBrush(Color.White);

        Boolean loseVar = false;
        Boolean winVar = false;

        Boolean herotagger = true;
        Boolean montagger = false;
        Boolean mon2tagger = false;

        Random randGen = new Random();

        int randomValue;

        //TODO create your global game variables here
        int heroX, heroY, heroSize, heroSpeed;
        int monX, monY, monSize, monSpeed;
        int mon2X, mon2Y;
        SolidBrush heroBrush = new SolidBrush(Color.Red);
        SolidBrush monBrush = new SolidBrush(Color.Blue);
        public GameScreen()
        {
            InitializeComponent();
            InitializeGameValues();
        }

        public void InitializeGameValues()
        {
            //TODO - setup all your initial game values here. Use this method
            // each time you restart your game to reset all values.
            heroX = 100;
            heroY = 100;
            heroSize = 20;
            heroSpeed = 5;

            monX = 200;
            monY = 200;
            monSize = 20;
            monSpeed = 3;

            mon2X = 300;
            mon2Y = 300;

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
                heroX = heroX - heroSpeed;

            }
            if (downArrowDown == true)
            {
                heroY = heroY + heroSpeed;
            }
            if (rightArrowDown == true)
            {
                heroX = heroX + heroSpeed;
            }
            if (upArrowDown == true)
            {
                heroY = heroY - heroSpeed;
            }

            //TODO move npc characters

            runnerMethod();
            runnerMethod2();

            if (heroX <= 0)
            {
                heroX = 0;
            }
            if (heroY <= 0)
            {
                heroY = 0;
            }
            if (heroX >= 300)
            {
                heroX = 300;
            }
            if (heroY >= 300)
            {
                heroY = 300;
            }

            Rectangle hero = new Rectangle(heroX, heroY, heroSize, heroSize);
            Rectangle mon = new Rectangle(monX, monY, monSize, monSize);
            Rectangle mon2 = new Rectangle(mon2X, mon2Y, monSize, monSize);

            Boolean heroMonCollision = hero.IntersectsWith(mon);
            Boolean monMon2Collision = mon.IntersectsWith(mon2);
            Boolean heroMon2Collision = hero.IntersectsWith(mon2);


            if (heroMonCollision && herotagger == true)
            {
                herotagger = false;
                montagger = true;
                taggerMethod();
            }
            else if (heroMonCollision && montagger == true)
            {
                herotagger = true;
                montagger = false;
                runnerMethod();
            }

            if (montagger == true)
            {
                taggerMethod();
            }
            else
            {
                runnerMethod();
            }


            if (heroMon2Collision && herotagger == true)
            {
                herotagger = false;
                mon2tagger = true;
                taggerMethod();
            }
            else if (heroMonCollision && montagger == true)
            {
                herotagger = true;
                mon2tagger = false;
                runnerMethod();
            }

            if (mon2tagger == true)
            {
                taggerMethod2();
            }
            else
            {
                runnerMethod2();
            }


            if (monMon2Collision && montagger == true)
            {
                montagger = false;
                mon2tagger = true; 
            }
            else if (monMon2Collision && mon2tagger == true)
            {
                montagger = true;
                mon2tagger = false;
            }
            
            //TODO collisions checks 

            //calls the GameScreen_Paint method to draw the screen.
            Refresh();
        }


        //Everything that is to be drawn on the screen should be done here
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw rectangle to screen

            e.Graphics.FillRectangle(heroBrush, heroX, heroY, heroSize, heroSize);
            e.Graphics.FillRectangle(monBrush, monX, monY, monSize, monSize);
            e.Graphics.FillRectangle(monBrush, mon2X, mon2Y, monSize, monSize);
        }

        public void runnerMethod()
        {
            SolidBrush tagBrush = new SolidBrush(Color.Blue);

            //Moving
            if (monX < heroX)
            {
                monX = monX - monSpeed;
            }
            else
            {
                monX = monX + monSpeed;
            }

            if (monY < heroY)
            {
                monY = monY - monSpeed;
            }
            else
            {
                monY = monY + monSpeed;
            }

            //Stopping it from going off the page
            if (monX <= 0)
            {
                monX = 0;
            }
            if (monY <= 0)
            {
                monY = 0;
            }
            if (monX >= 300)
            {
                monX = 300;
            }
            if (monY >= 300)
            {
                monY = 300;
            }
        }

        public void runnerMethod2()
        {
            SolidBrush tagBrush = new SolidBrush(Color.Blue);

            //Moving
            if (monX < heroX)
            {
                monX = monX - monSpeed;
            }
            else
            {
                monX = monX + monSpeed;
            }

            if (monY < heroY)
            {
                monY = monY - monSpeed;
            }
            else
            {
                monY = monY + monSpeed;
            }

            //Stopping it from going off the page
            if (monX <= 0)
            {
                monX = 0;
            }
            if (monY <= 0)
            {
                monY = 0;
            }
            if (monX >= 300)
            {
                monX = 300;
            }
            if (monY >= 300)
            {
                monY = 300;
            }
        }

        public void taggerMethod()
        {
            //Variables
            int heroLoc = heroX + heroY;
            int mon2Loc = monX + monY;

            //Moving
            int heroVar1 = heroLoc - mon2Loc;
            int mon2Var1 = mon2Loc - heroLoc;

            if (heroVar1 > mon2Var1)
            {
                if (mon2X < monX)
                {
                    monX = monX + monSpeed + 2;
                }
                else
                {
                    monX = monX - monSpeed - 2;
                }

                if (mon2Y < monY)
                {
                    monY = monY + monSpeed + 2;
                }
                else
                {
                    monY = monY - monSpeed - 2;
                }
            }
            else if (heroVar1 < mon2Var1)
            {
                if (mon2X < heroX)
                {
                    monX = monX + monSpeed + 2;
                }
                else
                {
                    monX = monX - monSpeed - 2;
                }

                if (mon2Y < heroY)
                {
                    monY = monY + monSpeed + 2;
                }
                else
                {
                    monY = monY - monSpeed - 2;
                }
            }
            else
            {
                if (mon2X < heroX)
                {
                    monX = monX + monSpeed + 2;
                }
                else
                {
                    monX = monX - monSpeed - 2;
                }

                if (mon2Y < heroY)
                {
                    monY = monY + monSpeed + 2;
                }
                else
                {
                    monY = monY - monSpeed - 2;
                }
            }
        }

        public void taggerMethod2()
        {
            //Variables
            int heroLoc = heroX + heroY;
            int mon2Loc = monX + monY;

            //Moving
            int heroVar1 = heroLoc - mon2Loc;
            int mon2Var1 = mon2Loc - heroLoc;

            if (heroVar1 > mon2Var1)
            {
                if (mon2X < monX)
                {
                    monX = monX + monSpeed + 2;
                }
                else
                {
                    monX = monX - monSpeed - 2;
                }

                if (mon2Y < monY)
                {
                    monY = monY + monSpeed + 2;
                }
                else
                {
                    monY = monY - monSpeed - 2;
                }
            }
            else if (heroVar1 < mon2Var1)
            {
                if (mon2X < heroX)
                {
                    monX = monX + monSpeed + 2;
                }
                else
                {
                    monX = monX - monSpeed - 2;
                }

                if (mon2Y < heroY)
                {
                    monY = monY + monSpeed + 2;
                }
                else
                {
                    monY = monY - monSpeed - 2;
                }
            }
            else
            {
                if (mon2X < heroX)
                {
                    monX = monX + monSpeed + 2;
                }
                else
                {
                    monX = monX - monSpeed - 2;
                }

                if (mon2Y < heroY)
                {
                    monY = monY + monSpeed + 2;
                }
                else
                {
                    monY = monY - monSpeed - 2;
                }
            }
        }
    }
}

