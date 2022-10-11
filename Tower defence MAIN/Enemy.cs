using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;


namespace Tower_defence_MAIN
{



    public class Enemy : Objects_Game
    {


        #region Variables


        // used to check that the enemies are going along the right path
        private int Check_Point_Checker = 0;




        // used to draw enemies and features
        Rectangle health_bar;
        SolidBrush brush;
        Pen pen;


        // used to check for the amount of time that the enemy is slowed
        Stopwatch Stop_watch;


        //special enemies variants
        private bool immune;
        private bool slowed;


        // money that is dropped
        private int money_dropped;



        // used to check different aspects of the enemies health
        private float health_current;
        private float health_max;
        private bool dead;
        private float speed_temp;
        private int percentage_health;


        #endregion

        #region GET/SET Variables

        public float Health_Current
        {
            get { return health_current; }
            set
            {
                health_current = value;
                if (health_current < 0)
                {
                    health_current = 0;
                }
            }
        }


        public bool Slowed
        {
            get { return slowed; }
            set { slowed = value; }
        }

        public float Speed_Temp
        {
            get { return speed_temp; }
            set { speed_temp = value; }
        }

        public bool Immune
        {
            get { return immune; }
            set { immune = value; }
        }

        #endregion


        public Enemy(string image_file, PointF startposition, float animation_speed, float scaleFactor, float health, float speed, float Money_Dropped, bool immune)
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {
            // sets the money dropped from an enemy
            this.money_dropped = (int)Money_Dropped;

            speed_temp = speed;

            Stop_watch = new Stopwatch();


            //setting the special enemies
            this.immune = immune;




            //setting the brush
            brush = new SolidBrush(Color.White);
            pen = new Pen(Color.Black);

            //setting all of the health variables
            this.health_max = health;
            this.health_current = health;
            health_bar = new Rectangle((int)co_ords.X, (int)co_ords.Y + 16, 100, 4);
            dead = false;



        }


        public override void Update(float deltatime)
        {
            //used for enemies being slowed
            if(Stop_watch.ElapsedMilliseconds > 5000)
            {
                slowed = false;
                speed = speed_temp;
            }





            if (co_ords.X + (speed / 100 * 3) >= GameScreen.Path_Checkpoints[Check_Point_Checker].X && co_ords.X - (speed / 100 * 3) <= GameScreen.Path_Checkpoints[Check_Point_Checker].X && co_ords.Y + (speed / 100 * 3) >= GameScreen.Path_Checkpoints[Check_Point_Checker].Y && co_ords.Y - (speed / 100 * 3) <= GameScreen.Path_Checkpoints[Check_Point_Checker].Y)
            {

                //This will set the enemy close enough to the checkpoint
                //set target to the checkpoints
                co_ords.X = GameScreen.Path_Checkpoints[Check_Point_Checker].X;
                co_ords.Y = GameScreen.Path_Checkpoints[Check_Point_Checker].Y;


                //has to make sure that its not looking at the last checkpoint
                if(Check_Point_Checker + 1 < GameScreen.Path_Checkpoints.Count())
                {
                    Check_Point_Checker = Check_Point_Checker + 1;
                }
                else
                {

                    // makes the enemy loop back to the begining
                    Check_Point_Checker = 0;
                    co_ords.X = GameScreen.Path_Checkpoints[Check_Point_Checker].X;
                    co_ords.Y = GameScreen.Path_Checkpoints[Check_Point_Checker].Y;

                    GameScreen.Lives -= 1;

                }



            }
            else // this means that the enemy wasnt close enough
            {
                // this will then make the enemy move closer to the next checkpoint on the x axis
                if(co_ords.X != GameScreen.Path_Checkpoints[Check_Point_Checker].X)
                {
                    if(co_ords.X + 1 > GameScreen.Path_Checkpoints[Check_Point_Checker].X)
                    {
                        co_ords.X -= deltatime * speed;
                    }
                    else if(co_ords.X - 1 < GameScreen.Path_Checkpoints[Check_Point_Checker].X)
                    {
                        co_ords.X += deltatime * speed;
                    }

                }
                else if (co_ords.X + 1 >= GameScreen.Path_Checkpoints[Check_Point_Checker].X && co_ords.X - 1 <= GameScreen.Path_Checkpoints[Check_Point_Checker].X)
                {
                    co_ords.X = GameScreen.Path_Checkpoints[Check_Point_Checker].X;
                }

                // Y axis
                if (co_ords.Y != GameScreen.Path_Checkpoints[Check_Point_Checker].Y)
                {
                    if (co_ords.Y + 1 > GameScreen.Path_Checkpoints[Check_Point_Checker].Y)
                    {
                        co_ords.Y -= deltatime * speed;
                    }
                    else if (co_ords.Y - 1 < GameScreen.Path_Checkpoints[Check_Point_Checker].Y)
                    {
                        co_ords.Y += deltatime * speed;
                    }

                }
                else if (co_ords.Y + 1 >= GameScreen.Path_Checkpoints[Check_Point_Checker].Y && co_ords.Y - 1 <= GameScreen.Path_Checkpoints[Check_Point_Checker].Y)
                {
                    co_ords.Y = GameScreen.Path_Checkpoints[Check_Point_Checker].Y;
                }



            }



            base.Update(deltatime);
        }

        public void Death()
        {
            if(!dead)
            {
                GameScreen.Remove_Objects.Add(this);
                GameScreen.Money += money_dropped;

                dead = true;
            }
        }

        public void Slow_Enemy()
        {
            slowed = true;

            Stop_watch.Reset();
            Stop_watch.Start();
        }

        public override void Draw(Graphics dc)
        {



            percentage_health = (int)(((double)health_current / (double)health_max * 100));
            health_bar.X = (int)Positionxy.X - 2;
            health_bar.Y = (int)Positionxy.Y + 30;
            health_bar.Width = (int)((double)percentage_health * 0.4);

            brush.Color = Color.Black;
            dc.FillRectangle(brush, new RectangleF((int)health_bar.X, (int)health_bar.Y, (int)(100 * 0.4), 4));

            if (percentage_health >= 50)
            {
                brush.Color = Color.ForestGreen;
            }
            if (percentage_health < 50)
            {
                brush.Color = Color.Orange;
            }
            if (percentage_health < 20)
            {
                brush.Color = Color.Red;
            }

            dc.FillRectangle(brush, health_bar);

            base.Draw(dc);













        }




    }
}
