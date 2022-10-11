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
    public partial class Leaderboards : Form
    {
        public Leaderboards()
        {
            InitializeComponent();
        }


        private void Bubble_Sort()
        {
            string path = @"Leaderboards.txt";

            string[] lines = File.ReadAllLines(path);

            bool Item_Moved = true;

            string[] Arr_1;

            int Input_1;

            string[] Arr_2;

            int Input_2;

            string[] arr = new string[2];

            string Temporary;

            int loop = lines.Length - 1;
            foreach(string Lines in lines)
            {
                arr = Lines.Split(',');

                Console.WriteLine(arr[0] + ":" + arr[1]);
            }

            for(int x = 1; (x <= loop) && (Item_Moved == true); x++)
            {
                Item_Moved = false;
                for(int i = 0; (i <= loop  - 1); i++)
                {
                    Arr_1 = lines[i].Split(',');

                    Input_1 = Convert.ToInt32(Arr_1[1]);

                    Arr_2 = lines[i + 1].Split(',');

                    Input_2 = Convert.ToInt32(Arr_2[1]);

                    if(Input_2 > Input_1)
                    {
                        Temporary = lines[i];

                        lines[i] = lines[i + 1];
                        lines[i + 1] = Temporary;

                        Item_Moved = true;
                    }

                }
            }

            Output_to_screen(lines);
            

        }


        public void Output_to_screen(string[] Data)
        {

            int Number_output;

            string[] Data_Split;

            int x = 1;


            Number_output = Data.Length;

            for(int i = 0; i < Number_output; i++)
            {
                Data_Split = Data[i].Split(',');

                ListViewItem item = new ListViewItem(Convert.ToString(x));

                item.SubItems.Add(Data_Split[0]);
                item.SubItems.Add(Data_Split[1]);

                listView1.Items.Add(item);

                x++;
            }

        }

        private void Leaderboards_Load(object sender, EventArgs e)
        {
            Bubble_Sort();
        }

        private void Leaderboards_FormClosed(object sender, FormClosedEventArgs e)
        {
            StartingMenu menu = new StartingMenu();
            menu.Show();
        }
    }
}
