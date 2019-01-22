using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Game_Template
{
    public partial class EndGameScreen : UserControl
    {
        public EndGameScreen()
        {
            InitializeComponent();
            
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            MainForm.ChangeScreen(this, "GameScreen");
        }

        private void endGameButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
