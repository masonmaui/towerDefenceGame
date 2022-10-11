using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_defence_MAIN
{
    public partial class GameFrm : Form
    {
        private static GameFrm gameFRM;

        GameScreen gS;
        Graphics dc;

        string user_name;
        public static GameFrm state
        {
            get { return GameFrm.gameFRM; }
        }
        public GameFrm(string User_Name)
        {
            user_name = User_Name;

            gameFRM = this;
            InitializeComponent();
        }

        private void GameFrm_Load(object sender, EventArgs e)
        {
            dc = CreateGraphics();

            GameScreen.Rectangle_set(this.DisplayRectangle);

            gS = new GameScreen(dc, user_name);
        }

        private void Main_Loop_Tick(object sender, EventArgs e)
        {
            gS.Game_loop(this.PointToClient(Cursor.Position));
        }

        private void GameFrm_MouseDown(object sender, MouseEventArgs e)
        {
            gS.Click_Check(true);
        }

        private void GameFrm_MouseUp(object sender, MouseEventArgs e)
        {
            gS.Click_Check(false);
        }

        private void Button_Next_Wave_Click(object sender, EventArgs e)
        {
            GameScreen.Wave_Keeper.Next_Wave();
        }
    }

}
