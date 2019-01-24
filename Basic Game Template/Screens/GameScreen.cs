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
using System.Threading;

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

        int collisionWait = 0;

        SolidBrush drawBrush = new SolidBrush(Color.Black);
        SolidBrush winBrush = new SolidBrush(Color.White);

        Boolean winVar = false;

        Random randGen = new Random();

        int randomValue;

        //TODO create your global game variables here
        int playrunnerSpeed = 5;
        int friend1runnerSpeed = 3;
        int friend2runnerSpeed = 2;
        int friend3runnerSpeed = 1;
        int chaserSpeed = 4;
        int playX, playY, friend1X, friend1Y, friend2X, friend2Y, friend3X, friend3Y;
        int counter;
        int counter3 = 0;
        int timer1 = 0;

        int friend1playsubs, friend1friend2subs, friend1friend3subs;
        int friend2playsubs, friend2friend1subs, friend2friend3subs;
        int friend3playsubs, friend3friend1subs, friend3friend2subs;

        double friend1playchaserdis, friend1friend2chaserdis, friend1friend3chaserdis;
        double friend2playchaserdis, friend2friend1chaserdis, friend2friend3chaserdis;
        double friend3playchaserdis, friend3friend1chaserdis, friend3friend2chaserdis;

        SolidBrush chaserBrush = new SolidBrush(Color.Red);
        SolidBrush runnerBrush = new SolidBrush(Color.Blue);
        SolidBrush numBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);

        Font drawFont = new Font("Arial", 16, FontStyle.Bold);
        Font playFont = new Font("AriaL", 6);
        Font friend1Font = new Font("AriaL", 5);
        Font friend2Font = new Font("AriaL", 6);
        Font friend3Font = new Font("AriaL", 8);
        Font nameFont = new Font("Arial", 10);

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

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            friend1playchaserdis = Math.Sqrt((playX - friend1X) * (playX - friend1X) + (playY - friend1Y) * (playY - friend1Y));
            friend1friend2chaserdis = Math.Sqrt((friend2X - friend1X) * (friend2X - friend1X) + (friend2Y - friend1Y) * (friend2Y - friend1Y));
            friend1friend3chaserdis = Math.Sqrt((friend3X - friend1X) * (friend3X - friend1X) + (friend3Y - friend1Y) * (friend3Y - friend1Y));

            friend2playchaserdis = Math.Sqrt((playX - friend2X) * (playX - friend2X) + (playY - friend2Y) * (playY - friend2Y));
            friend2friend1chaserdis = Math.Sqrt((friend1X - friend2X) * (friend1X - friend2X) + (friend1Y - friend2Y) * (friend1Y - friend2Y));
            friend2friend3chaserdis = Math.Sqrt((friend3X - friend2X) * (friend3X - friend2X) + (friend3Y - friend2Y) * (friend3Y - friend2Y));

            friend3playchaserdis = Math.Sqrt((playX - friend3X) * (playX - friend3X) + (playY - friend3Y) * (playY - friend3Y));
            friend3friend2chaserdis = Math.Sqrt((friend2X - friend3X) * (friend2X - friend3X) + (friend2Y - friend3Y) * (friend2Y - friend3Y));
            friend3friend1chaserdis = Math.Sqrt((friend1X - friend3X) * (friend1X - friend3X) + (friend1Y - friend3Y) * (friend1Y - friend3Y));

            timer1 = counter / 35;

            while (counter3 < 1000)
            {
                invisLabel.Text = "Welcome to tag! Because you got here last, you are going to start off as the tagger! You are the red character, and all the blue characters are the runners! You can transfer being tagger by running into one of the runners. You have five minutes, GO!!!";
                
               // invisLabel.Text += "\nYou are the red character, and all the blue characters are the runners!";
                
              //  invisLabel.Text += "\nYou can transfer being tagger by running into one of the runners. You have five minutes, GO!!!";
               
                Refresh(); 

                counter3++;
            }
            if (counter3 == 1000)
            {
                invisLabel.Text = "";
            }

            //moves main character 
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

            //moves npc characters
            // Friend 1:
            if (friend1Chaser == true)
            {
                friend1playsubs = (playX - friend1X) + (playY - friend1Y);
                friend1friend2subs = (friend2X - friend1X) + (friend2Y - friend1Y);
                friend1friend3subs = (friend3X - friend1X) + (friend3Y - friend1Y);

                if (friend1playsubs < 0)
                {
                    friend1playsubs = friend1playsubs * -1;
                }
                if (friend1friend2subs < 0)
                {
                    friend1friend2subs = friend1friend2subs * -1;
                }
                if (friend1friend3subs < 0)
                {
                    friend1friend3subs = friend1friend3subs * -1;
                }

                if (friend1playsubs < friend1friend2subs && friend1playsubs < friend1friend3subs)
                {
                    friend1PlayerClose = true;
                    friend1Friend2Close = false;
                    friend1Friend3Close = false;
                }
                else if (friend1friend2subs < friend1playsubs && friend1friend2subs < friend1friend3subs)
                {
                    friend1Friend2Close = true;
                    friend1PlayerClose = false;
                    friend1Friend3Close = false;
                }
                else if (friend1friend3subs < friend1friend2subs && friend1friend3subs < friend1playsubs)
                {
                    friend1Friend3Close = true;
                    friend1Friend2Close = false;
                    friend1PlayerClose = false;
                }
                
                // If player is closest
                if (friend1PlayerClose == true)
                {
                    if (friend1X > playX)
                    {
                        friend1X = friend1X - chaserSpeed;
                    }
                    else if (friend1X < playX)
                    {
                        friend1X = friend1X + chaserSpeed;
                    }

                    if (friend1Y > playY)
                    {
                        friend1Y = friend1Y - chaserSpeed;
                    }
                    else if (friend1Y < playY)
                    {
                        friend1Y = friend1Y + chaserSpeed;
                    }
                }
                // If friend2 is the closest 
                else if (friend1Friend2Close == true)
                {
                    if (friend1X > friend2X)
                    {
                        friend1X = friend1X - chaserSpeed;
                    }
                    else if (friend1X < friend2X)
                    {
                        friend1X = friend1X + chaserSpeed;
                    }
                    if (friend1Y > friend2Y)
                    {
                        friend1Y = friend1Y - chaserSpeed;
                    }
                    else if (friend1Y < friend2Y)
                    {
                        friend1Y = friend1Y + chaserSpeed;
                    }
                }
                // If friend 3 is the closest 
                else if (friend1Friend3Close == true)
                {
                    if (friend1X > friend3X)
                    {
                        friend1X = friend1X - chaserSpeed;
                    }
                    else if (friend1X < friend3X)
                    {
                        friend1X = friend1X + chaserSpeed;
                    }
                    if (friend1Y > friend3Y)
                    {
                        friend1Y = friend1Y - chaserSpeed;
                    }
                    else if (friend1Y < friend3Y)
                    {
                        friend1Y = friend1Y + chaserSpeed;
                    }
                }
            }
            else if (friend1Chaser == false)
            {
                // If the player is it

                if (friend1playchaserdis < 75)
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
                    if (friend1Y > (playY + 50) || friend1Y < (playY + 50))
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

                if (friend1friend2chaserdis < 75)
                {
                    if (friend1X > (friend2X + 50) || friend1X < (friend2X + 50))
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
                    if (friend2Y > (friend2Y + 50) || friend2Y < (friend2Y + 50))
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

                if (friend1friend2chaserdis < 75)
                {
                    if (friend1X > (friend3X + 50) || friend1X < (friend3X + 50))
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
                    if (friend2Y > (friend3Y + 50) || friend2Y < (friend3Y + 50))
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
                friend2playsubs = (playX - friend2X) + (playY - friend2Y);
                friend2friend1subs = (friend1X - friend2X) + (friend1Y - friend2Y);
                friend2friend3subs = (friend3X - friend2X) + (friend3Y - friend2Y);

                if (friend2playsubs < 0)
                {
                    friend2playsubs = friend2playsubs * -1;
                }
                if (friend2friend1subs < 0)
                {
                    friend2friend1subs = friend2friend1subs * -1;
                }
                if (friend2friend3subs < 0)
                {
                    friend2friend3subs = friend2friend3subs * -1;
                }

                if (friend2playsubs < friend2friend1subs && friend2playsubs < friend1friend3subs)
                {
                    friend2PlayerClose = true;
                    friend2Friend1Close = false;
                    friend2Friend3Close = false;
                }

                else if (friend2friend1subs < friend2playsubs && friend2friend1subs < friend2friend3subs)
                {
                    friend2Friend1Close = true;
                    friend2PlayerClose = false;
                    friend2Friend3Close = false;
                }
                else if (friend2friend3subs < friend2friend1subs && friend2friend3subs < friend2playsubs)
                {
                    friend2Friend3Close = true;
                    friend2Friend1Close = false;
                    friend2PlayerClose = false;
                }

                // If player is closest
                if (friend2PlayerClose == true)
                {
                    if (friend2X > playX)
                    {
                        friend2X = friend2X - chaserSpeed;
                    }
                    else if (friend2X < playX)
                    {
                        friend2X = friend2X + chaserSpeed;
                    }
                     if (friend2Y > playY)
                    {
                        friend2Y = friend2Y - chaserSpeed;
                    }
                    else if (friend2Y < playY)
                    {
                        friend2Y = friend2Y + chaserSpeed;
                    }
                }
                // If friend1 is the closest 
                else if (friend2Friend1Close == true)
                {
                    if (friend2X > friend1X)
                    {
                        friend2X = friend2X - chaserSpeed;
                    }
                    else if (friend2X < friend1X)
                    {
                        friend2X = friend2X + chaserSpeed;
                    }
                     if (friend2Y > friend1Y)
                    {
                        friend2Y = friend2Y - chaserSpeed;
                    }
                    else if (friend2Y < friend1Y)
                    {
                        friend2Y = friend2Y + chaserSpeed;
                    }
                }
                // If friend 3 is the closest 
                else if (friend2Friend3Close == true)
                {
                    if (friend2X > friend3X)
                    {
                        friend2X = friend2X - chaserSpeed;
                    }
                    else if (friend2X < friend3X)
                    {
                        friend2X = friend2X + chaserSpeed;
                    }
                     if (friend2Y > friend3Y)
                    {
                        friend2Y = friend2Y - chaserSpeed;
                    }
                    else if (friend2Y < friend3Y)
                    {
                        friend2Y = friend2Y + chaserSpeed;
                    }
                }
            }
            else if (friend2Chaser == false)
            {
                // If the player is it

                if (friend2playchaserdis < 75)
                {
                    if (friend2X > (playX + 50) || friend2X < (playX + 50))
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
                    if (friend2Y > (playY + 50) || friend2Y < (playY + 50))
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

                if (friend2friend1chaserdis < 75)
                {
                    if (friend2X > (friend1X + 50) || friend2X < (friend1X + 50))
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
                    if (friend2Y > (friend1Y + 50) || friend2Y < (friend1Y + 50))
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

                if (friend2friend3chaserdis < 75)
                {
                    if (friend2X > (friend3X + 50) || friend2X < (friend3X + 50))
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
                    if (friend2Y > (friend3Y + 50) || friend2Y < (friend3Y + 50))
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
            if (friend3Chaser == true)
            {
                friend3playsubs = (playX - friend3X) + (playY - friend3Y);
                friend3friend2subs = (friend2X - friend3X) + (friend2Y - friend3Y);
                friend3friend1subs = (friend1X - friend3X) + (friend1Y - friend3Y);

                if (friend3playsubs < 50)
                {
                    friend3playsubs = friend3playsubs * -1;
                }
                if (friend3friend2subs < 50)
                {
                    friend3friend2subs = friend3friend2subs * -1;
                }
                if (friend3friend1subs < 50)
                {
                    friend3friend1subs = friend3friend1subs * -1;
                }

                if (friend3playsubs < friend3friend2subs && friend3playsubs < friend3friend1subs)
                {
                    friend3PlayerClose = true;
                    friend3Friend2Close = false;
                    friend3Friend1Close = false;
                }
                else if (friend3friend2subs < friend3playsubs && friend3friend2subs < friend3friend1subs)
                {
                    friend3Friend2Close = true;
                    friend3PlayerClose = false;
                    friend3Friend1Close = false;
                }
                else if (friend3friend1subs < friend3friend2subs && friend3friend1subs < friend3playsubs)
                {
                    friend3Friend1Close = true;
                    friend3Friend2Close = false;
                    friend3PlayerClose = false;
                }

                // If player is closest
                if (friend3PlayerClose == true)
                {
                    if (friend3X > playX)
                    {
                        friend3X = friend3X - chaserSpeed;
                    }
                    else if (friend3X < playX)
                    {
                        friend3X = friend3X + chaserSpeed;
                    }
                    if (friend3Y > playY)
                    {
                        friend3Y = friend3Y - chaserSpeed;
                    }
                    else if (friend3Y < playY)
                    {
                        friend3Y = friend3Y + chaserSpeed;
                    }
                }
                // If friend2 is closest
                else if (friend3Friend2Close == true)
                {
                    if (friend3X > friend2X)
                    {
                        friend3X = friend3X - chaserSpeed;
                    }
                    else if (friend3X < friend2X)
                    {
                        friend3X = friend3X + chaserSpeed;
                    }
                    if (friend3Y > friend2Y)
                    {
                        friend3Y = friend3Y - chaserSpeed;
                    }
                    else if (friend3Y < friend2Y)
                    {
                        friend3Y = friend3Y + chaserSpeed;
                    }
                }
                // If friend1 is the tagger
                else if (friend3Friend1Close == true)
                {
                    if (friend3X > friend1X)
                    {
                        friend3X = friend3X - chaserSpeed;
                    }
                    else if (friend3X < friend1X)
                    {
                        friend3X = friend3X + chaserSpeed;
                    }
                    if (friend3Y > friend1Y)
                    {
                        friend3Y = friend3Y - chaserSpeed;
                    }
                    else if (friend3Y < friend1Y)
                    {
                        friend3Y = friend3Y + chaserSpeed;
                    }
                }
            }
            else if (friend3Chaser == false)
            {
                // If the player is the tagger 

                if (friend3playchaserdis < 75)
                {
                    if (friend3X > (playX + 50) || friend3X < (playX + 50))
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
                    if (friend3Y > (playY + 50) || friend3Y < (playY + 50))
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

                // If friend1 is the tagger

                if (friend3friend1chaserdis < 75)
                {
                    if (friend3X > (friend1X + 50) || friend3X < (friend1X + 50))
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
                    if (friend3Y > (friend1Y + 50) || friend3Y < (friend1Y + 50))
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

                // If friend2 is the tagger

                if (friend3friend2chaserdis < 75)
                {
                    if (friend3X > (friend2X + 50) || friend3X < (friend2X + 50))
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
                    if (friend3Y > (friend2Y + 50) || friend3Y < (friend2Y + 50))
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
            playX = restrictionMethodX(playX);
            playY = restrictionMethodY(playY);

            // Friend1 momevment restrictions:
            friend1X = restrictionMethodX(friend1X);
            friend1Y = restrictionMethodY(friend1Y);

            // Friend2 momevment restrictions:
            friend2X = restrictionMethodX(friend2X);
            friend2Y = restrictionMethodY(friend2Y);

            // Friend3 momevment restrictions:
            friend3X = restrictionMethodX(friend3X);
            friend3Y = restrictionMethodY(friend3Y);

            // Creating collision detectors
            Rectangle play = new Rectangle(playX, playY, 10, 10);
            Rectangle friend1 = new Rectangle(friend1X, friend1Y, 8, 8);
            Rectangle friend2 = new Rectangle(friend2X, friend2Y, 10, 10);
            Rectangle friend3 = new Rectangle(friend3X, friend3Y, 12, 12);

            Boolean playFriend1Collision, playFriend2Collision, playFriend3Collision, friend1Friend2Collision, friend1Friend3Collision, friend2Friend3Collision;
            playFriend1Collision = playFriend2Collision = playFriend3Collision = friend1Friend2Collision = friend1Friend3Collision = friend2Friend3Collision = false;

            collisionWait++;

            if (collisionWait > 20)
            {
                //player collisions
                playFriend1Collision = play.IntersectsWith(friend1);
                playFriend2Collision = play.IntersectsWith(friend2);
                playFriend3Collision = play.IntersectsWith(friend3);

                //AI collisions
                friend1Friend2Collision = friend1.IntersectsWith(friend2);
                friend1Friend3Collision = friend1.IntersectsWith(friend3);
                friend2Friend3Collision = friend2.IntersectsWith(friend3);
            }

            // Player and friend1 collisions
            if (playFriend1Collision == true && playChaser == true)
            {
                collisionWait = 0;
                friend2Chaser = false;
                playChaser = false;
                friend1Chaser = true;
                friend3Chaser = false;

                friend1X = 0;
                friend1Y = 0;
            }
            else if (playFriend1Collision == true && friend1Chaser == true)
            {
                collisionWait = 0;

                playChaser = true;
                friend1Chaser = false;

                friend2Chaser = false;
                friend3Chaser = false;

                playX = 0;
                playY = 0;
            }

            // Player and friend2 collisions
            if (playFriend2Collision == true && playChaser == true)
            {
                collisionWait = 0;

                playChaser = false;
                friend2Chaser = true;

                friend1Chaser = false;
                friend3Chaser = false;

                friend2X = 0;
                friend2Y = 0;

            }
            else if (playFriend2Collision == true && friend2Chaser == true)
            {
                collisionWait = 0;

                playChaser = true;
                friend2Chaser = false;

                friend1Chaser = false;
                friend3Chaser = false;

                playX = 0;
                playY = 0;
            }

            // Player and friend3 collisions
            if (playFriend3Collision == true && playChaser == true)
            {
                collisionWait = 0;

                playChaser = false;
                friend3Chaser = true;

                friend2Chaser = false;
                friend1Chaser = false;

                friend3X = 0;
                friend3Y = 0;

            }
            else if (playFriend3Collision == true && friend3Chaser == true)
            {
                collisionWait = 0;

                playChaser = true;
                friend2Chaser = false;

                friend1Chaser = false;
                friend3Chaser = false;

                playX = 0;
                playY = 0;
            }

            // Friend1 and friend2 collisions
            if (friend1Friend2Collision == true && friend1Chaser == true)
            {
                collisionWait = 0;

                friend1Chaser = false;
                friend2Chaser = true;

                playChaser = false;
                friend3Chaser = false;

                friend2X = 0;
                friend2Y = 0;

            }
            else if (friend1Friend2Collision == true && friend2Chaser == true)
            {
                collisionWait = 0;

                playChaser = false;
                friend1Chaser = true;
                friend2Chaser = false;
                friend3Chaser = false;

                friend1X = 0;
                friend1Y = 0;
            }

            // Friend1 and friend3 collisions
            if (friend1Friend3Collision == true && friend1Chaser == true)
            {
                collisionWait = 0;

                friend1Chaser = false;
                friend3Chaser = true;

                friend2Chaser = false;
                playChaser = false;

                friend3X = 0;
                friend3Y = 0; 
            }
            else if (friend1Friend3Collision == true && friend3Chaser == true)
            {
                collisionWait = 0;

                friend1Chaser = true;
                friend3Chaser = false;

                friend2Chaser = false;
                playChaser = false;

                friend1X = 0;
                friend1Y = 0;
            }

            // Friend2 and friend3 collisions
            if (friend2Friend3Collision == true && friend2Chaser == true)
            {
                collisionWait = 0;

                friend2Chaser = false;
                friend3Chaser = true;

                friend1Chaser = false;
                playChaser = false;

                friend3X = 0;
                friend3Y = 0;
            }
            else if (friend2Friend3Collision == true && friend3Chaser == true)
            {
                collisionWait = 0;

                friend2Chaser = true;
                friend3Chaser = false;

                friend1Chaser = false;
                playChaser = false;

                friend2X = 0;
                friend2Y = 0;
            }

            //Deciding if the game is over
            if (timer1 == 10)
            {
                if (playChaser =! true)
                {
                    winVar = true;
                }
                if (winVar == true)
                {
                    gameTimer.Enabled = false;
                    MainForm.ChangeScreen(this, "WinGameScreen");
                    return;
                }
                else
                {
                    gameTimer.Enabled = false;
                    MainForm.ChangeScreen(this, "EndGameScreen");
                    return;
                }
            }
           
            counter++;
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

            // Player
            e.Graphics.DrawString("P", playFont, numBrush, playX, playY);

            // friend1
            e.Graphics.DrawString("1", friend1Font, numBrush, friend1X, friend1Y);
            // friend2
            e.Graphics.DrawString("2", friend2Font, numBrush, friend2X, friend2Y);

            //friend3
            e.Graphics.DrawString("3", friend3Font, numBrush, friend3X, friend3Y);

            //timer
            e.Graphics.DrawString("Time Remaining: " + (300 - counter/35), playFont, blackBrush, 100, 20);

            //Credits to the makers
            e.Graphics.DrawString("By: Charlie Kevill and Chris Rouse", playFont, numBrush, (this.Width - 100), (this.Height - 10));

            //Name of the Game
            e.Graphics.DrawString("RUN", playFont, numBrush, 0, 0);

        }
        public int restrictionMethodX(int chX)
        {
            if (chX < 0)
            {
                chX = 0;
            }

            if (chX > 317)
            {
                chX = 317;
            }

          
            return chX;


        }
        public int restrictionMethodY(int chY)
        {

            if (chY < 0)
            {
                chY = 0;
            }

            if (chY > 295)
            {
                chY = 295;
            }
           
            return chY;

        }
    }
}
