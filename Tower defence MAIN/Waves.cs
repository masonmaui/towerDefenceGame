using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Tower_defence_MAIN
{
    class Waves : Objects_Game
    {

        #region Variables

        // these will keep track of the waves

        private int wave_counter;
        private int wave_current;
        private float current_difficulty;
        private float difficulty_increase;
        private int enemy_health;



        // managing the money
        private bool earn_interest;
        private int interest;

        // these variables will be used to manage the spawning of the enemies
        private bool spawning;
        private int spawn_counter;
        private float spawn_accumilator = 0.0f;
        private float spawn_per_second = 2f;




        #endregion

        public int Wave_Current
        {
            get { return wave_current; }
            set { wave_current = value; }
        }


        public Waves(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed)
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {


            // this will set the waves to reset
            wave_counter = 0;
            wave_current = 0;
            interest = 10;
            current_difficulty = 1.175f;
            difficulty_increase = 10;
        }


        public void Next_Wave()
        {

            if (!spawning)
            {
                spawning = true;



                // need to check to see if any enemies are there from last round of enemies
                foreach(Objects_Game objects in GameScreen.Objects)
                {
                    if((objects as Enemy) != null)
                    {
                        spawning = false;
                    }
                }


                if(spawning == true)
                {
                    // resets counter
                    spawn_counter = 0;

                    earn_interest = true;


                    if(wave_counter >= 4)
                    {
                        wave_counter = 0;
                    }

                    // adds to the counters
                    wave_counter = wave_counter + 1;
                    wave_current = wave_current + 1;

                    enemy_health += (int)(difficulty_increase);

                    difficulty_increase = difficulty_increase * current_difficulty;

                }



                
            }
        }



        public override void Update(float deltatime)
        {

            //calculates how many enemies to spawn per second
            spawn_accumilator += spawn_per_second * deltatime;
            int to_spawn = (int)spawn_accumilator;
            spawn_accumilator -= (float)to_spawn;




            for(int i = 0; i < to_spawn && spawn_counter < 25 && spawning; i++)
            {
                spawn_counter++;

                switch (wave_counter)
                {
                    case 1:
                        GameScreen.New_Objects.Add(new Enemy(@"sprites\Enemies\Normal.png", new PointF(270, -40), 0, 1, enemy_health * 0.8f, 100, 1 + (int)(difficulty_increase * 0.04), false));
                        break;
                    case 2:
                        GameScreen.New_Objects.Add(new Enemy(@"sprites\Enemies\Fast.png", new PointF(270, -40), 0, 1, enemy_health * 0.8f, 150, 1 + (int)(difficulty_increase * 0.04), false));
                        break;
                    case 3:
                        GameScreen.New_Objects.Add(new Enemy(@"sprites\Enemies\Immune.png", new PointF(270, -40), 0, 1, enemy_health * 0.8f, 100, 1 + (int)(difficulty_increase * 0.04), true));
                        break;
                    case 4:
                        GameScreen.New_Objects.Add(new Enemy(@"sprites\Enemies\Boss.png", new PointF(270, -40), 0, 1, (enemy_health * 8), 75, 10 + (int)(difficulty_increase * 0.04), false));
                        spawn_counter = 25;
                        break;
                }

            }
            
            if(spawn_counter >= 25)
            {
                spawning = false;
            }

            if(earn_interest && !spawning)
            {
                bool completed = true;

                foreach(Objects_Game objects in GameScreen.New_Objects)
                {
                    if((objects as Enemy) != null)
                    {
                        completed = false;
                        break;
                    }
                }

                if(earn_interest == true && completed == true)
                {
                    GameScreen.Money += interest + (int)(difficulty_increase * 0.2);

                    earn_interest = false;
                }

            }







            base.Update(deltatime);
        }
    }
}
