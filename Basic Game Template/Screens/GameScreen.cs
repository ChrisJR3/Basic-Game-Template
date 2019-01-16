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

        Boolean playChaser = true;
        Boolean friend1Chaser = false;
        Boolean friend2Chaser = false;
        Boolean friend3Chaser = false;

        Boolean friend1PlayerClose = false;
        Boolean friend1Friend2Close = false;
        Boolean friend1Friend3Close = false;

        Boolean friend2PlayerClose = false;
        Boolean friend2Friend1Close = false;
        Boolean friend2Friend3Close = false;

        Boolean friend3PlayerClose = false;
        Boolean friend3Friend1Close = false;
        Boolean friend3Friend2Close = false;

        Font drawFont = new Font("Arial", 16, FontStyle.Bold);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        SolidBrush winBrush = new SolidBrush(Color.White);

        Boolean loseVar = false;
        Boolean winVar = false;

        Random randGen = new Random();

        int randomValue;

        //TODO create your global game variables here
        int playrunnerSpeed = 4;
        int friend1runnerSpeed = 4;
        int friend2runnerSpeed = 3;
        int friend3runnerSpeed = 2;
        int chaserSpeed = 5;
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
                playX = playX - playrunnerSpeed;
            }
            if (downArrowDown == true)
            {
                playY = playY + playrunnerSpeed;
            }
            if (rightArrowDown == true)
            {
                playX = playX + playrunnerSpeed;
            }
            if (upArrowDown == true)
            {
                playY = playY - playrunnerSpeed;
            }

            //TODO move npc characters
            // Friend 1:
            if (friend1Chaser == true)
            {
                if ((friend1X > 150) && (friend1Y > 150))
                {
                    // if the player is closest:
                    if ((playX > friend2X && playX > friend3X) && (playY > friend2Y && playY > friend3Y))
                    {
                        friend1PlayerClose = true;
                        friend1Friend2Close = false;
                        friend1Friend3Close = false;
                    }
                    // if friend2 is closest:
                    else if ((friend2X > playX && friend2X > friend3X) && (friend2Y > playY && friend1Y > friend3Y))
                    {
                        friend1Friend2Close = true;
                        friend1PlayerClose = false;
                        friend1Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X > playX && friend3X > friend2X) && (friend3Y > playY && friend3Y > friend2Y))
                    {
                        friend1Friend3Close = true;
                        friend1Friend2Close = false;
                        friend1PlayerClose = false;

                    }
                }

                else if ((friend1X > 150) && (friend1Y < 150))
                {
                    // if the player is closest:
                    if ((playX > friend2X && playX > friend3X) && (playY < friend2Y && playY < friend3Y))
                    {
                        friend1PlayerClose = true;
                        friend1Friend2Close = false;
                        friend1Friend3Close = false;
                    }
                    // if friend2 is closest:
                    else if ((friend2X > playX && friend2X > friend3X) && (friend2Y < playY && friend2Y < friend3Y))
                    {
                        friend1Friend2Close = true;
                        friend1PlayerClose = false;
                        friend1Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X > playX && friend3X > friend2X) && (friend3Y < playY && friend3Y < friend2Y))
                    {
                        friend1Friend3Close = true;
                        friend1Friend2Close = false;
                        friend1PlayerClose = false;
                    }
                }

                else if ((friend1X <= 150) && (friend1Y <= 150))
                {
                    // if the player is closest:
                    if ((playX <= friend2X && playX <= friend3X) && (playY <= friend2Y && playY <= friend3Y))
                    {
                        friend1PlayerClose = true;
                        friend1Friend2Close = false;
                        friend1Friend3Close = false;
                    }
                    // if friend2 is closest:
                    else if ((friend2X <= playX && friend2X <= friend3X) && (friend2Y <= playY && friend2Y <= friend3Y))
                    {
                        friend1Friend2Close = true;
                        friend1PlayerClose = false;
                        friend1Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X <= playX && friend3X <= friend2X) && (friend3Y <= playY && friend3Y <= friend2Y))
                    {
                        friend1Friend3Close = true;
                        friend1Friend2Close = false;
                        friend1PlayerClose = false;
                    }
                }

                else if ((friend1X <= 150) && (friend1Y >= 150))
                {
                    // if the player is closest:
                    if ((playX <= friend2X && playX <= friend3X) && (playY >= friend2Y && playY >= friend3Y))
                    {
                        friend1PlayerClose = true;
                        friend1Friend2Close = false;
                        friend1Friend3Close = false;
                    }
                    // if friend2 is closest:
                    else if ((friend2X <= playX && friend2X <= friend3X) && (friend2Y >= playY && friend2Y >= friend3Y))
                    {
                        friend1Friend2Close = true;
                        friend1PlayerClose = false;
                        friend1Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X <= playX && friend3X <= friend2X) && (friend3Y >= playY && friend3Y >= friend2Y))
                    {
                        friend1Friend3Close = true;
                        friend1Friend2Close = false;
                        friend1PlayerClose = false;
                    }
                }

                // If player is closest
                if (friend1PlayerClose == true)
                {
                    if (friend1X > playX)
                    {
                        friend1X = friend1X - friend1runnerSpeed;
                    }
                    else if (friend1X < playX)
                    {
                        friend1X = friend1X + friend1runnerSpeed;
                    }
                    else if (friend1Y > playY)
                    {
                        friend1Y = friend1Y - friend1runnerSpeed;
                    }
                    else if (friend1Y < playY)
                    {
                        friend1Y = friend1Y + friend1runnerSpeed;
                    }
                }
                // If friend2 is the closest 
                else if (friend1Friend2Close == true)
                {
                    if (friend1X > friend2X)
                    {
                        friend1X = friend1X - friend1runnerSpeed;
                    }
                    else if (friend1X < friend2X)
                    {
                        friend1X = friend1X + friend1runnerSpeed;
                    }
                    else if (friend1Y > friend2Y)
                    {
                        friend1Y = friend1Y - friend1runnerSpeed;
                    }
                    else if (friend1Y < friend2Y)
                    {
                        friend1Y = friend1Y + friend1runnerSpeed;
                    }
                }
                // If friend 3 is the closest 
                else if (friend1Friend3Close == true)
                {
                    if (friend1X > friend3X)
                    {
                        friend1X = friend1X - friend1runnerSpeed;
                    }
                    else if (friend1X < friend3X)
                    {
                        friend1X = friend1X + friend1runnerSpeed;
                    }
                    else if (friend1Y > friend3Y)
                    {
                        friend1Y = friend1Y - friend1runnerSpeed;
                    }
                    else if (friend1Y < friend3Y)
                    {
                        friend1Y = friend1Y + friend1runnerSpeed;
                    }
                }
            }
            else if (friend1Chaser == false)
            {
                // If the player is it

                if (playChaser == true)
                {
                    if (friend1X > (playX + 50) || friend1X < (playX + 50))
                    {
                        if (friend1X < playX)
                        {
                            friend1X = friend1X - friend1runnerSpeed;
                        }
                        else
                        {
                            friend1X = friend1X + friend1runnerSpeed;
                        }
                    }
                    if (friend1Y > playY + 50 || friend1Y < playY + 50)
                    {
                        if (friend1Y < playY)
                        {
                            friend1Y = friend1Y - friend1runnerSpeed;
                        }
                        else
                        {
                            friend1Y = friend1Y + friend1runnerSpeed;
                        }
                    }

                }

                // If friend2 is it

                if (friend2Chaser == true)
                {
                    if (friend1X > friend2X + 50 || friend1X < friend2X + 50)
                    {
                        if (friend1X < friend2X)
                        {
                            friend1X = friend1X - friend1runnerSpeed;
                        }
                        else
                        {
                            friend1X = friend1X + friend1runnerSpeed;
                        }
                    }
                    if (friend2Y > friend2Y + 50 || friend2Y < friend2Y + 50)
                    {
                        if (friend1Y < friend2Y)
                        {
                            friend1Y = friend1Y - friend1runnerSpeed;
                        }
                        else
                        {
                            friend1Y = friend1Y + friend1runnerSpeed;
                        }
                    }

                }

                // If friend3 is it

                if (friend3Chaser == true)
                {
                    if (friend1X > friend3X + 50 || friend1X < friend3X + 50)
                    {
                        if (friend1X < friend3X)
                        {
                            friend1X = friend1X - friend1runnerSpeed;
                        }
                        else
                        {
                            friend1X = friend1X + friend1runnerSpeed;
                        }
                    }
                    if (friend2Y > friend3Y + 50 || friend2Y < friend3Y + 50)
                    {
                        if (friend1Y < friend3Y)
                        {
                            friend1Y = friend1Y - friend1runnerSpeed;
                        }
                        else
                        {
                            friend1Y = friend1Y + friend1runnerSpeed;
                        }
                    }
                }
            }

            // Friend 2:
            if (friend2Chaser == true)
            {
                if ((friend2X > 150) && (friend2Y > 150))
                {
                    // if the player is closest:
                    if ((playX > friend1X && playX > friend3X) && (playY > friend1Y && playY > friend3Y))
                    {
                        friend2PlayerClose = true;
                        friend2Friend1Close = false;
                        friend2Friend3Close = false;
                    }
                    // if friend1 is closest:
                    else if ((friend1X > playX && friend1X > friend3X) && (friend1Y > playY && friend1Y > friend3Y))
                    {
                        friend2Friend1Close = true;
                        friend2PlayerClose = false;
                        friend2Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X > playX && friend3X > friend2X) && (friend3Y > playY && friend3Y > friend2Y))
                    {
                        friend2Friend3Close = true;
                        friend2Friend1Close = false;
                        friend2PlayerClose = false;

                    }
                }

                else if ((friend2X > 150) && (friend2Y < 150))
                {
                    // if the player is closest:
                    if ((playX > friend1X && playX > friend3X) && (playY < friend1Y && playY < friend3Y))
                    {
                        friend2PlayerClose = true;
                        friend2Friend1Close = false;
                        friend2Friend3Close = false;
                    }
                    // if friend1 is closest:
                    else if ((friend1X > playX && friend1X > friend3X) && (friend1Y < playY && friend1Y < friend3Y))
                    {
                        friend2Friend1Close = true;
                        friend2PlayerClose = false;
                        friend2Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X > playX && friend3X > friend1X) && (friend3Y < playY && friend3Y < friend1Y))
                    {
                        friend2Friend3Close = true;
                        friend2Friend1Close = false;
                        friend2PlayerClose = false;
                    }
                }

                else if ((friend2X <= 150) && (friend2Y <= 150))
                {
                    // if the player is closest:
                    if ((playX <= friend1X && playX <= friend3X) && (playY <= friend1Y && playY <= friend3Y))
                    {
                        friend2PlayerClose = true;
                        friend2Friend1Close = false;
                        friend2Friend3Close = false;
                    }
                    // if friend1 is closest:
                    else if ((friend1X <= playX && friend1X <= friend3X) && (friend1Y <= playY && friend1Y <= friend3Y))
                    {
                        friend2Friend1Close = true;
                        friend2PlayerClose = false;
                        friend2Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X <= playX && friend3X <= friend1X) && (friend3Y <= playY && friend3Y <= friend1Y))
                    {
                        friend2Friend3Close = true;
                        friend2Friend1Close = false;
                        friend2PlayerClose = false;
                    }
                }

                else if ((friend2X <= 150) && (friend2Y >= 150))
                {
                    // if the player is closest:
                    if ((playX <= friend1X && playX <= friend3X) && (playY >= friend1Y && playY >= friend3Y))
                    {
                        friend2PlayerClose = true;
                        friend2Friend1Close = false;
                        friend2Friend3Close = false;
                    }
                    // if friend1 is closest:
                    else if ((friend1X <= playX && friend1X <= friend3X) && (friend1Y >= playY && friend1Y >= friend3Y))
                    {
                        friend2Friend1Close = true;
                        friend2PlayerClose = false;
                        friend2Friend3Close = false;
                    }
                    //if friend3 is closest:
                    else if ((friend3X <= playX && friend3X <= friend1X) && (friend3Y >= playY && friend3Y >= friend1Y))
                    {
                        friend2Friend3Close = true;
                        friend2Friend1Close = false;
                        friend2PlayerClose = false;
                    }
                }

                // If player is closest
                if (friend2PlayerClose == true)
                {
                    if (friend2X > playX)
                    {
                        friend2X = friend2X - friend2runnerSpeed;
                    }
                    else if (friend2X < playX)
                    {
                        friend2X = friend2X + friend2runnerSpeed;
                    }
                    else if (friend2Y > playY)
                    {
                        friend2Y = friend2Y - friend2runnerSpeed;
                    }
                    else if (friend2Y < playY)
                    {
                        friend2Y = friend2Y + friend2runnerSpeed;
                    }
                }
                // If friend1 is the closest 
                else if (friend2Friend1Close == true)
                {
                    if (friend2X > friend1X)
                    {
                        friend2X = friend2X - friend2runnerSpeed;
                    }
                    else if (friend2X < friend1X)
                    {
                        friend2X = friend2X + friend2runnerSpeed;
                    }
                    else if (friend2Y > friend1Y)
                    {
                        friend2Y = friend2Y - friend2runnerSpeed;
                    }
                    else if (friend2Y < friend1Y)
                    {
                        friend2Y = friend2Y + friend2runnerSpeed;
                    }
                }
                // If friend 3 is the closest 
                else if (friend2Friend3Close == true)
                {
                    if (friend2X > friend3X)
                    {
                        friend2X = friend2X - friend2runnerSpeed;
                    }
                    else if (friend2X < friend3X)
                    {
                        friend2X = friend2X + friend2runnerSpeed;
                    }
                    else if (friend2Y > friend3Y)
                    {
                        friend2Y = friend2Y - friend2runnerSpeed;
                    }
                    else if (friend2Y < friend3Y)
                    {
                        friend2Y = friend2Y + friend2runnerSpeed;
                    }
                }
            }
            else if (friend2Chaser == false)
            {
                // If the player is it

                if (playChaser == true)
                {
                    if (friend2X > playX + 50 || friend2X < playX + 50)
                    {
                        if (friend2X < playX)
                        {
                            friend2X = friend2X - friend2runnerSpeed;
                        }
                        else
                        {
                            friend2X = friend2X + friend2runnerSpeed;
                        }
                    }
                    if (friend2Y > playY + 50 || friend2Y < playY + 50)
                    {
                        if (friend2Y < playY)
                        {
                            friend2Y = friend2Y - friend2runnerSpeed;
                        }
                        else
                        {
                            friend2Y = friend2Y + friend2runnerSpeed;
                        }
                    }
                }

                // If friend1 is it

                if (friend1Chaser == true)
                {
                    if (friend2X > friend1X + 50 || friend2X < friend1X + 50)
                    {
                        if (friend2X < friend1X)
                        {
                            friend2X = friend2X - friend2runnerSpeed;
                        }
                        else
                        {
                            friend2X = friend2X + friend2runnerSpeed;
                        }
                    }
                    if (friend2Y > friend1Y + 50 || friend2Y < friend1Y + 50)
                    {
                        if (friend2Y < friend1Y)
                        {
                            friend2Y = friend2Y - friend2runnerSpeed;
                        }
                        else
                        {
                            friend2Y = friend2Y + friend2runnerSpeed;
                        }
                    }
                }

                // If friend3 is it

                if (friend3Chaser == true)
                {
                    if (friend2X > friend3X + 50 || friend2X < friend3X + 50)
                    {
                        if (friend2X < friend3X)
                        {
                            friend2X = friend2X - friend2runnerSpeed;
                        }
                        else
                        {
                            friend2X = friend2X + friend2runnerSpeed;
                        }
                    }
                    if (friend2Y > friend3Y + 50 || friend2Y < friend3Y + 50)
                    {
                        if (friend2Y < friend3Y)
                        {
                            friend2Y = friend2Y - friend2runnerSpeed;
                        }
                        else
                        {
                            friend2Y = friend2Y + friend2runnerSpeed;
                        }
                    }
                }
            }

            // Friend 3:
            if (friend3Chaser == true) { friend3chaserMethod(); }
            else if (friend3Chaser == false)
            {
                // If the player is it

                if (playChaser == true)
                {
                    if (friend3X > playX + 50 || friend3X < playX + 50)
                    {
                        if (friend3X < playX)
                        {
                            friend3X = friend3X - friend3runnerSpeed;
                        }
                        else
                        {
                            friend3X = friend3X + friend3runnerSpeed;
                        }
                    }
                    if (friend3Y > playY + 50 || friend3Y < playY + 50)
                    {
                        if (friend3Y < playY)
                        {
                            friend3Y = friend3Y - friend3runnerSpeed;
                        }
                        else
                        {
                            friend3Y = friend3Y + friend3runnerSpeed;
                        }
                    }
                }

                // If friend1 is it

                if (friend1Chaser == true)
                {
                    if (friend3X > friend1X + 50 || friend3X < friend1X + 50)
                    {
                        if (friend3X < friend1X)
                        {
                            friend3X = friend3X - friend3runnerSpeed;
                        }
                        else
                        {
                            friend3X = friend3X + friend3runnerSpeed;
                        }
                    }
                    if (friend3Y > friend1Y + 50 || friend3Y < friend1Y + 50)
                    {
                        if (friend3Y < friend1Y)
                        {
                            friend3Y = friend3Y - friend3runnerSpeed;
                        }
                        else
                        {
                            friend3Y = friend3Y + friend3runnerSpeed;
                        }
                    }
                }

                // If friend2 is it

                if (friend2Chaser == true)
                {
                    if (friend3X > friend2X + 50 || friend3X < friend2X + 50)
                    {
                        if (friend3X < friend2X)
                        {
                            friend3X = friend3X - friend3runnerSpeed;
                        }
                        else
                        {
                            friend3X = friend3X + friend3runnerSpeed;
                        }
                    }
                    if (friend3Y > friend2Y + 50 || friend3Y < friend2Y + 50)
                    {
                        if (friend3Y < friend2Y)
                        {
                            friend3Y = friend3Y - friend3runnerSpeed;
                        }
                        else
                        {
                            friend3Y = friend3Y + friend3runnerSpeed;
                        }
                    }
                }
            }

            // Player momevment restrictions:

            if (playX < 0)
            {
                playX = 0;
            }
            if (playY < 0)
            {
                playY = 0;
            }
            if (playX > 300)
            {
                playX = 300;
            }
            if (playY > 300)
            {
                playY = 300;
            }

            // Friend1 momevment restrictions:

            if (friend1X < 0)
            {
                friend1X = 0;
            }
            if (friend1Y < 0)
            {
                friend1Y = 0;
            }
            if (friend1X > 300)
            {
                friend1X = 300;
            }
            if (friend1Y > 300)
            {
                friend1Y = 300;
            }

            // Friend2 momevment restrictions:

            if (friend2X < 0)
            {
                friend2X = 0;
            }
            if (friend2Y < 0)
            {
                friend2Y = 0;
            }
            if (friend2X > 300)
            {
                friend2X = 300;
            }
            if (friend2Y > 300)
            {
                friend2Y = 300;
            }

            // Friend3 momevment restrictions:

            if (friend3X < 0)
            {
                friend3X = 0;
            }
            if (friend3Y < 0)
            {
                friend3Y = 0;
            }
            if (friend3X > 300)
            {
                friend3X = 300;
            }
            if (friend3Y > 300)
            {
                friend3Y = 300;
            }

            // Creating collision detecters

            Rectangle play = new Rectangle(playX, playY, 10, 10);
            Rectangle friend1 = new Rectangle(friend1X, friend1Y, 8, 8);
            Rectangle friend2 = new Rectangle(friend2X, friend2Y, 10, 10);
            Rectangle friend3 = new Rectangle(friend3X, friend3Y, 12, 12);

            //Creating player collisions

            Boolean playFriend1Collision = play.IntersectsWith(friend1);
            Boolean playFriend2Collision = play.IntersectsWith(friend2);
            Boolean playFriend3Collision = play.IntersectsWith(friend3);

            // Creating AI collisions

            Boolean friend1Friend2Collision = friend1.IntersectsWith(friend2);
            Boolean friend1Friend3Collision = friend1.IntersectsWith(friend3);
            Boolean friend2Friend3Collision = friend2.IntersectsWith(friend3);

            // Changing who is the chaser:

            // Player and friend1 collisions
            if (playFriend1Collision == true && playChaser == true)
            {
                playChaser = false;
                friend1Chaser = true;

                friend2Chaser = false;
                friend3Chaser = false;
            }
            else if (playFriend1Collision == true && friend1Chaser == true)
            {
                playChaser = true;
                friend1Chaser = false;

                friend2Chaser = false;
                friend3Chaser = false;
            }

            // Player and friend2 collisions
            if (playFriend2Collision == true && playChaser == true)
            {
                playChaser = false;
                friend2Chaser = true;

                friend1Chaser = false;
                friend3Chaser = false;
            }
            else if (playFriend2Collision == true && friend2Chaser == true)
            {
                playChaser = true;
                friend2Chaser = false;

                friend1Chaser = false;
                friend3Chaser = false;
            }

            // Player and friend3 collisions
            if (playFriend3Collision == true && playChaser == true)
            {
                playChaser = false;
                friend3Chaser = true;

                friend2Chaser = false;
                friend1Chaser = false;
            }
            else if (playFriend3Collision == true && friend3Chaser == true)
            {
                playChaser = true;
                friend2Chaser = false;

                friend1Chaser = false;
                friend3Chaser = false;
            }

            // Friend1 and friend2 collisions
            if (friend1Friend2Collision == true && friend1Chaser == true)
            {
                friend1Chaser = false;
                friend2Chaser = true;

                playChaser = false;
                friend3Chaser = false;
            }
            else if (friend1Friend2Collision == true && friend2Chaser == true)
            {
                friend1Chaser = true;
                friend2Chaser = false;

                playChaser = false;
                friend3Chaser = false;
            }

            // Friend1 and friend3 collisions
            if (friend1Friend3Collision == true && friend1Chaser == true)
            {
                friend1Chaser = false;
                friend3Chaser = true;

                friend2Chaser = false;
                playChaser = false;
            }
            else if (friend1Friend3Collision == true && friend3Chaser == true)
            {
                friend1Chaser = true;
                friend3Chaser = false;

                friend2Chaser = false;
                playChaser = false;
            }

            // Friend2 and friend3 collisions
            if (friend2Friend3Collision == true && friend2Chaser == true)
            {
                friend2Chaser = false;
                friend3Chaser = true;

                friend1Chaser = false;
                playChaser = false;
            }
            else if (friend2Friend3Collision == true && friend3Chaser == true)
            {
                friend2Chaser = true;
                friend3Chaser = false;

                friend1Chaser = false;
                playChaser = false;
            }

            Refresh();
        }


        //Everything that is to be drawn on the screen should be done here
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Player graphics
            if (playChaser == true) { e.Graphics.FillRectangle(chaserBrush, playX, playY, 10, 10); }
            else { e.Graphics.FillRectangle(runnerBrush, playX, playY, 10, 10); }

            // Friend1 graphics
            if (friend1Chaser == true) { e.Graphics.FillRectangle(chaserBrush, friend1X, friend1Y, 8, 8); }
            else { e.Graphics.FillRectangle(runnerBrush, friend1X, friend1Y, 8, 8); }

            // Friend2 graphics
            if (friend2Chaser == true) { e.Graphics.FillRectangle(chaserBrush, friend2X, friend2Y, 10, 10); }
            else { e.Graphics.FillRectangle(runnerBrush, friend2X, friend2Y, 10, 10); }

            // Friend3 graphics
            if (friend3Chaser == true) { e.Graphics.FillRectangle(chaserBrush, friend3X, friend3Y, 12, 12); }
            else { e.Graphics.FillRectangle(runnerBrush, friend3X, friend3Y, 12, 12); }
        }

        public void friend1chaserMethod()
        {

        }

        public void friend2chaserMethod()
        {

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
                    friend1X = friend1X + chaserSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - chaserSpeed - 2;
                }

                if (friend2Y < friend1Y)
                {
                    friend1Y = friend1Y + chaserSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - chaserSpeed - 2;
                }
            }
            else if (heroVar1 < mon2Var1)
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + chaserSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - chaserSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + chaserSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - chaserSpeed - 2;
                }
            }
            else
            {
                if (friend2X < playX)
                {
                    friend1X = friend1X + chaserSpeed + 2;
                }
                else
                {
                    friend1X = friend1X - chaserSpeed - 2;
                }

                if (friend2Y < playY)
                {
                    friend1Y = friend1Y + chaserSpeed + 2;
                }
                else
                {
                    friend1Y = friend1Y - chaserSpeed - 2;
                }
            }
        }
    }
}
