﻿using System;
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
    public partial class How_to_play : Form
    {
        public How_to_play()
        {
            InitializeComponent();
        }

        private void How_to_play_FormClosed(object sender, FormClosedEventArgs e)
        {
            StartingMenu menu = new StartingMenu();
            menu.Show();
        }
    }
}
