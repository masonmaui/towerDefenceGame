
namespace Tower_defence_MAIN
{
    partial class GameFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameFrm));
            this.Main_Loop = new System.Windows.Forms.Timer(this.components);
            this.Button_Next_Wave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Main_Loop
            // 
            this.Main_Loop.Enabled = true;
            this.Main_Loop.Interval = 1;
            this.Main_Loop.Tick += new System.EventHandler(this.Main_Loop_Tick);
            // 
            // Button_Next_Wave
            // 
            this.Button_Next_Wave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Next_Wave.BackgroundImage")));
            this.Button_Next_Wave.Location = new System.Drawing.Point(629, 520);
            this.Button_Next_Wave.Name = "Button_Next_Wave";
            this.Button_Next_Wave.Size = new System.Drawing.Size(114, 29);
            this.Button_Next_Wave.TabIndex = 0;
            this.Button_Next_Wave.UseVisualStyleBackColor = true;
            this.Button_Next_Wave.Click += new System.EventHandler(this.Button_Next_Wave_Click);
            // 
            // GameFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Button_Next_Wave);
            this.Name = "GameFrm";
            this.Text = "GameFrm";
            this.Load += new System.EventHandler(this.GameFrm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameFrm_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameFrm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Main_Loop;
        private System.Windows.Forms.Button Button_Next_Wave;
    }
}