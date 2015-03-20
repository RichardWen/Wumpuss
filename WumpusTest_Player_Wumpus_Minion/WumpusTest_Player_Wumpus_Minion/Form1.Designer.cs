namespace WumpusTest_Player_Wumpus_Minion
{
    partial class Player_Wumpus_Minion_Form
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PlayerButton = new System.Windows.Forms.Button();
            this.WumpusButton = new System.Windows.Forms.Button();
            this.MinionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayerButton
            // 
            this.PlayerButton.Location = new System.Drawing.Point(139, 168);
            this.PlayerButton.Name = "PlayerButton";
            this.PlayerButton.Size = new System.Drawing.Size(133, 23);
            this.PlayerButton.TabIndex = 0;
            this.PlayerButton.Text = "Player Class";
            this.PlayerButton.UseVisualStyleBackColor = true;
            this.PlayerButton.Click += new System.EventHandler(this.PlayerButton_Click);
            // 
            // WumpusButton
            // 
            this.WumpusButton.Location = new System.Drawing.Point(139, 197);
            this.WumpusButton.Name = "WumpusButton";
            this.WumpusButton.Size = new System.Drawing.Size(133, 23);
            this.WumpusButton.TabIndex = 1;
            this.WumpusButton.Text = "Wumpus Class";
            this.WumpusButton.UseVisualStyleBackColor = true;
            this.WumpusButton.Click += new System.EventHandler(this.WumpusButton_Click);
            // 
            // MinionButton
            // 
            this.MinionButton.Location = new System.Drawing.Point(139, 226);
            this.MinionButton.Name = "MinionButton";
            this.MinionButton.Size = new System.Drawing.Size(133, 23);
            this.MinionButton.TabIndex = 2;
            this.MinionButton.Text = "Minion Class";
            this.MinionButton.UseVisualStyleBackColor = true;
            this.MinionButton.Click += new System.EventHandler(this.MinionButton_Click);
            // 
            // Player_Wumpus_Minion_Form
            // 
            this.BackColor = System.Drawing.Color.SlateBlue;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.MinionButton);
            this.Controls.Add(this.WumpusButton);
            this.Controls.Add(this.PlayerButton);
            this.Name = "Player_Wumpus_Minion_Form";
            this.Text = "Player Wumpus and Minion Tester";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button PlayerButton;
        private System.Windows.Forms.Button WumpusButton;
        private System.Windows.Forms.Button MinionButton;
    }
}

