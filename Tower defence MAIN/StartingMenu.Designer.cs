
namespace Tower_defence_MAIN
{
    partial class StartingMenu
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
            this.play_button = new System.Windows.Forms.Button();
            this.How_to_play_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Leaderboards = new System.Windows.Forms.Button();
            this.Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // play_button
            // 
            this.play_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.play_button.Location = new System.Drawing.Point(34, 39);
            this.play_button.Name = "play_button";
            this.play_button.Size = new System.Drawing.Size(197, 72);
            this.play_button.TabIndex = 0;
            this.play_button.Text = "Play";
            this.play_button.UseVisualStyleBackColor = true;
            this.play_button.Click += new System.EventHandler(this.play_button_Click);
            // 
            // How_to_play_button
            // 
            this.How_to_play_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.How_to_play_button.Location = new System.Drawing.Point(34, 259);
            this.How_to_play_button.Name = "How_to_play_button";
            this.How_to_play_button.Size = new System.Drawing.Size(197, 72);
            this.How_to_play_button.TabIndex = 1;
            this.How_to_play_button.Text = "How to play";
            this.How_to_play_button.UseVisualStyleBackColor = true;
            this.How_to_play_button.Click += new System.EventHandler(this.How_to_play_button_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(34, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 72);
            this.button1.TabIndex = 2;
            this.button1.Text = "Quit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Leaderboards
            // 
            this.Leaderboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Leaderboards.Location = new System.Drawing.Point(34, 148);
            this.Leaderboards.Name = "Leaderboards";
            this.Leaderboards.Size = new System.Drawing.Size(197, 72);
            this.Leaderboards.TabIndex = 4;
            this.Leaderboards.Text = "Leaderboards";
            this.Leaderboards.UseVisualStyleBackColor = true;
            this.Leaderboards.Click += new System.EventHandler(this.Leaderboards_Click);
            // 
            // Username
            // 
            this.Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(246, 67);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(281, 31);
            this.Username.TabIndex = 5;
            this.Username.Click += new System.EventHandler(this.Username_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(335, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username";
            // 
            // StartingMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Leaderboards);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.How_to_play_button);
            this.Controls.Add(this.play_button);
            this.Name = "StartingMenu";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button play_button;
        private System.Windows.Forms.Button How_to_play_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Leaderboards;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label1;
    }
}

