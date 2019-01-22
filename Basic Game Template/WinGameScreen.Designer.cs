namespace Basic_Game_Template
{
    partial class WinGameScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.endGameButton = new System.Windows.Forms.Button();
            this.playAgainButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.winGameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // endGameButton
            // 
            this.endGameButton.BackColor = System.Drawing.Color.Yellow;
            this.endGameButton.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endGameButton.Location = new System.Drawing.Point(194, 294);
            this.endGameButton.Name = "endGameButton";
            this.endGameButton.Size = new System.Drawing.Size(93, 35);
            this.endGameButton.TabIndex = 7;
            this.endGameButton.Text = "End Game";
            this.endGameButton.UseVisualStyleBackColor = false;
            this.endGameButton.Click += new System.EventHandler(this.endGameButton_Click);
            // 
            // playAgainButton
            // 
            this.playAgainButton.BackColor = System.Drawing.Color.Yellow;
            this.playAgainButton.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainButton.Location = new System.Drawing.Point(194, 231);
            this.playAgainButton.Name = "playAgainButton";
            this.playAgainButton.Size = new System.Drawing.Size(93, 35);
            this.playAgainButton.TabIndex = 6;
            this.playAgainButton.Text = "Play Again";
            this.playAgainButton.UseVisualStyleBackColor = false;
            this.playAgainButton.Click += new System.EventHandler(this.playAgainButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(145, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 42);
            this.label1.TabIndex = 5;
            this.label1.Text = "Play Again?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // winGameLabel
            // 
            this.winGameLabel.AutoSize = true;
            this.winGameLabel.BackColor = System.Drawing.Color.Transparent;
            this.winGameLabel.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winGameLabel.ForeColor = System.Drawing.Color.Blue;
            this.winGameLabel.Location = new System.Drawing.Point(154, 112);
            this.winGameLabel.Name = "winGameLabel";
            this.winGameLabel.Size = new System.Drawing.Size(176, 42);
            this.winGameLabel.TabIndex = 4;
            this.winGameLabel.Text = "You Win! ";
            this.winGameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WinGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.endGameButton);
            this.Controls.Add(this.playAgainButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.winGameLabel);
            this.Name = "WinGameScreen";
            this.Size = new System.Drawing.Size(493, 430);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button endGameButton;
        private System.Windows.Forms.Button playAgainButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label winGameLabel;
    }
}
