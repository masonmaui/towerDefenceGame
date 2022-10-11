using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tower_defence_MAIN
{
    public partial class StartingMenu : Form
    {
        private static StartingMenu menu;



        public static StartingMenu state
        {
            get { return StartingMenu.menu; }
        }


        public StartingMenu()
        {
            menu = this;
            //User_Name = Username.Text;
            InitializeComponent();
        }


        private void play_button_Click(object sender, EventArgs e)
        {

            //string Path = (@"Leaderboards.csv");
            //using (StreamWriter sw = File.AppendText(Path))
            //{
            //    sw.WriteLine(Username.Text + "");
            //}



            if(Username.Text == "")
            {
                MessageBox.Show("Enter a username");
            }
            else
            {
                //File.WriteAllText(@"Leaderboards.txt", Username.Text.ToString());
                GameFrm game = new GameFrm(Username.Text);
                game.Show();
                game.Closed += new EventHandler(GameFrm_Closed);
                this.Hide();
            }

        }


        private void GameFrm_Closed(object sender,EventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void How_to_play_button_Click(object sender, EventArgs e)
        {

            How_to_play how_to = new How_to_play();
            how_to.Show();
            this.Hide();




        }

        private void Username_Click(object sender, EventArgs e)
        {
            Username.Text = "";
        }

        private void Leaderboards_Click(object sender, EventArgs e)
        {
            Leaderboards Board = new Leaderboards();
            Board.Show();
            this.Hide();
        }
    }
}
