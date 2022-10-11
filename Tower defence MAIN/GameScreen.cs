using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Tower_defence_MAIN
{
    class GameScreen
    {
        #region variables

        private static Waves wave_keeper;

        private static Objects_Game tower_placing;


        private static Point mouse_position;
        private PlayerMouse mouse;


        float FPS_current;
        private DateTime Time_end;


        //this will check if the end form has been shown to stop an infinate loop
        bool end_displayed = true;

        //Points along the path that the enemy has to follow.
        private static PointF[] path_checkpoints;


        //initiates graphics
        private Graphics dc;
        private BufferedGraphics backbuffer;
        private static Rectangle screensize;


        private Image Map_image;


        private static int lives;
        private static int money;

        // lists game objects
        private static List<Objects_Game> objects;
        private static List<Objects_Game> remove_object;
        private static List<Objects_Game> new_objects;


        //menu variable
        StartingMenu menu = new StartingMenu();
        string user_name;

        #endregion

        #region GET/SET Methods

        public static Waves Wave_Keeper
        {
            get { return wave_keeper; }
            set { GameScreen.wave_keeper = value; }
        }

        public static PointF[] Path_Checkpoints
        {
            get { return path_checkpoints; }
            set { path_checkpoints = value; }
        }

        public static Point Mouse_Position
        {
            get { return mouse_position; }
            set { mouse_position = value; }
        }

        // ADD WAVE METHOD

        public static Objects_Game Tower_Placing
        {
            get { return GameScreen.tower_placing; }
            set { GameScreen.tower_placing = value; }
        }

        public static int Money
        {
            get { return GameScreen.money; }
            set { GameScreen.money = value; }
        }

        public static int Lives
        {
            get { return GameScreen.lives; }
            set { GameScreen.lives = value; }
        }

        public static Rectangle Screensize
        {
            get { return screensize; }
        }

        //--------------------------------------------------
        public static List<Objects_Game> Objects
        {
            get { return GameScreen.objects; }
            set { GameScreen.objects = value; }
        }

        public static List<Objects_Game> Remove_Objects                // list methods
        {
            get { return GameScreen.remove_object; }
            set { GameScreen.remove_object = value; }
        }

        public static List<Objects_Game> New_Objects
        {
            get { return GameScreen.new_objects; }
            set { GameScreen.new_objects = value; }
        }
        //-------------------------------------------


        #endregion

        public GameScreen(Graphics dc, string User_Name)
        {
            this.user_name = User_Name;
            this.backbuffer = BufferedGraphicsManager.Current.Allocate(dc, screensize);
            this.dc = backbuffer.Graphics;
            Generate_map();
        }

        public void Click_Check(bool clicked)
        {
            // looks at mouse click in mouse cursor class
            mouse.Mouse_click(clicked, (1 / FPS_current));
        }


        public void Generate_map()
        {

            // these need to be instigated as objects at the start of the game
            objects = new List<Objects_Game>();
            new_objects = new List<Objects_Game>();
            remove_object = new List<Objects_Game>();

            //creating waves
            wave_keeper = new Waves(@"Sprites\Mouse\ENDOFMOUSE.png", new PointF(1000, 1000), 0, 1, 1);
            objects.Add(wave_keeper);

            Map_image = Image.FromFile(@"Sprites\Background\TDmap.jpg");


            // need a photo on the end of the mouse to keep track of its position
            mouse = new PlayerMouse(@"Sprites\Mouse\ENDOFMOUSE.png", Mouse_Position, 1, 1, 1);
            objects.Add(mouse);



            path_checkpoints = new PointF[]
            {
                new PointF(270, -40),
                new PointF(270, -40),
                new PointF(270, 55),
                new PointF(462, 55),
                new PointF(462, 215),
                new PointF(165, 215),
                new PointF(165, 93),
                new PointF(48, 93),
                new PointF(48, 338),
                new PointF(311, 338),
                new PointF(311, 431),
                new PointF(129, 431),
                new PointF(129, 547),
                new PointF(454, 547),

            };




            Lives = 5;
            Money = 100;



            // setting up towers
            objects.Add(new Tower_normal(@"Sprites\towers\MG.png", new PointF(595f, 90f), 0, 1, 1, false, false, false));
            objects.Add(new Tower_Slow(@"Sprites\towers\Cannon.png", new PointF(595f, 185f), 0, 1, 1, false, false, false));
            objects.Add(new Tower_missile(@"Sprites\towers\Missile_Launcher.png", new PointF(710f, 90f), 0, 1, 1, false, false, false));


            // sets the time so that the fps can be calculated
            Time_end = DateTime.Now;
        }


        // this wil update the objects that are being used in the game
        public void Update()
        {
            mouse.HandlingMousePos();


            // makes deltatime used in fps
            float deltatime = 1 / FPS_current;
            foreach (Objects_Game obj in objects)
            {
                obj.Update(deltatime);
            }
        }

        public void Animations_Update()
        {
            foreach (Objects_Game objects in objects)
            {
                objects.UpdateAnimation(FPS_current);
            }

        }
        public void Game_loop(Point gMousePos)
        {
            // calculates fps
            DateTime Time_Start = DateTime.Now;
            TimeSpan deltaTime = Time_Start - Time_end;
            int milliseconds = deltaTime.Milliseconds > 0 ? deltaTime.Milliseconds : 1;
            FPS_current = 1000 / milliseconds;
            Time_end = DateTime.Now;

            // add mouse position updater
            Mouse_Position = new Point(gMousePos.X, gMousePos.Y);





            Update();

            Animations_Update();

            objects.AddRange(new_objects);
            new_objects.Clear();

            //looks into the remove object list and removes them from the main list
            for (int i = 0; i < remove_object.Count; i++)
            {
                objects.Remove(Remove_Objects[i]);
            }
            Remove_Objects.Clear();




            Draw();

            if (lives <= 0)
            {
                Lost_Lives();
            }
        }



        public static void Rectangle_set(Rectangle rect)
        {
            screensize = rect;
        }

        private void Draw()
        {
            // drawing out the towers text onto the screen





            // this will draw the image to the screen instead of hard setting it
            dc.DrawImage(Map_image, 0, 0, Map_image.Width, Map_image.Height);





            // this will draw all of the objects in the game_objects draw function
            foreach (Objects_Game obj in objects)
            {
                obj.Draw(dc);
            }






            // showing the money and lives on the screen
            Font text = new Font("Arial", 18);
            dc.DrawString("Lives: " + lives.ToString(), text, Brushes.Green, 0, 50);
            dc.DrawString("£ " + money.ToString(), text, Brushes.Green, 0, 385);




            #region displaying towers text




            //sets the text for the normal tower
            text = new Font("Arial", 10);
            dc.DrawString("Normal", text, Brushes.Black, 637, 85);
            dc.DrawString("Cost: $10", text, Brushes.GreenYellow, 585, 130);

            text = new Font("Arial", 8);
            dc.DrawString("Dmg: 8", text, Brushes.Black, 637, 100);







            //sets the text for missile/splash tower
            text = new Font("Arial", 10);
            dc.DrawString("Missile", text, Brushes.Black, 702, 71);
            dc.DrawString("Cost: $30", text, Brushes.GreenYellow, 702, 130);

            text = new Font("Arial", 8);
            dc.DrawString("Dmg: 20", text, Brushes.Black, 740, 83);
            dc.DrawString("Splash", text, Brushes.Black, 744, 100);






            // sets the text for the slow tower
            text = new Font("Arial", 10);
            dc.DrawString("Slow", text, Brushes.Black, 637, 185);
            dc.DrawString("Cost: $15", text, Brushes.GreenYellow, 585, 225);

            text = new Font("Arial", 8);
            dc.DrawString("Dmg: 5", text, Brushes.Black, 637, 200);




            #endregion


            Font Large = new Font("Arial", 35);
            dc.DrawString("wave: " + wave_keeper.Wave_Current.ToString(), Large, Brushes.Gold, 590, 15);


            // testing things work -------------------------------------------------------------


            Font font = new Font("Arial ", 18);
            dc.DrawString("FPS: " + FPS_current.ToString(), font, Brushes.Red, 0, 25);


            ////draws lines between points 
            //Pen pen = new Pen(Color.Red, 2);
            //dc.DrawLines(pen, GameScreen.Path_Checkpoints);


            // ----------------------------------------------------------------------------------




            backbuffer.Render();
            dc.Clear(Color.White);
        }


        private void Lost_Lives()
        {
            string Path = (@"Leaderboards.txt");
            bool found = false;
            StreamReader sr = new StreamReader(Path);
            string str_data;
            string[] arr_data_items = new string[2];

            List<string> File_Lines = new List<string>();
            string file_lines;

            string Arr_username;
            string Arr_userscore;

            if (end_displayed == true)
            {

                while ((str_data = sr.ReadLine()) != null)
                {
                    str_data = str_data.Trim();
                    arr_data_items = str_data.Split(',');

                    Arr_username = arr_data_items[0];
                    Arr_userscore = arr_data_items[1];

                    
                    Console.WriteLine("test: " + Arr_username + Arr_userscore);
                    if (Arr_username == user_name)
                    {
                        Console.WriteLine(Arr_userscore + Arr_userscore);
                        found = true;
                        if (Convert.ToInt32(Arr_userscore) < Wave_Keeper.Wave_Current)
                        {
                            Arr_userscore = Convert.ToString(wave_keeper.Wave_Current);

                            
                        }
                        else
                        {
                            Console.WriteLine("score not higher");
                        }


                    }
                    else
                    {
                        Console.WriteLine("user doesnt exist");
                    }


                    file_lines = Arr_username + "," + Arr_userscore;
                    File_Lines.Add(file_lines);
                }


                end_displayed = false;
                End_Screen end_screen = new End_Screen();
                end_screen.Show();
                GameFrm.state.Close();

            }


            if (found == false)
            {
                file_lines = user_name +  ","  +  wave_keeper.Wave_Current;
                File_Lines.Add(file_lines);
            }

            sr.Close();
            Write_File(File_Lines);
        }

        private void Write_File(List<string> data)
        {
            bool written = false;
            while (written == false)
            {

                string Temp_File = @"TempFile.txt";
                string Path = (@"Leaderboards.txt");

                using (StreamWriter sw = File.AppendText(Temp_File))
                {
                    foreach (string line in data)
                    {
                        sw.WriteLine(line);
                    }
                }

                File.Delete(Path);

                File.Move(Temp_File, Path);

                written = true;
            }
        }
    }

}
