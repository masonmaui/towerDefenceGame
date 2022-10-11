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
    public partial class End_Screen : Form
    {

        private static End_Screen end;

        public static End_Screen state
        {
            get { return End_Screen.end; }
        }

        public End_Screen()
        {
            end = this;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics Graphics = this.CreateGraphics();
            Font F = new Font("Arial", 25);

            Graphics.DrawString(GameScreen.Wave_Keeper.Wave_Current.ToString(), F, Brushes.Black, 300, 105);

        }

        private void Btn_Menu_Click(object sender, EventArgs e)
        {
            GameFrm.state.Close();
            this.Close();

        }
    }
}
