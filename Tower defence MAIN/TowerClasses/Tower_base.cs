using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace Tower_defence_MAIN
{
    public abstract class Tower_base : Objects_Game
    {
        #region Variables


        // checking wether ot not the tower is being placed or is placed
        protected bool placed;
        protected bool getting_placed;
        protected bool tower_selected;


        // stopwatch to use whe looking at shots fired
        Stopwatch stopwatch;



        protected Enemy target_enemy;


        // all of the possible variables for the tower
        protected int level_upgrade;
        protected int tower_range;
        protected int tower_price;
        protected float value_total;
        protected int tower_damage;



        // brushes
        SolidBrush brush;
        Pen pen;


        #endregion

        #region GET/SET methods



        public bool Placed
        {
            get { return placed; }
            set { placed = value; }
        }

        public bool Tower_Selected
        {
            get { return tower_selected; }
            set { tower_selected = value; }
        }

        public int Level_Upgrade
        {
            get { return level_upgrade; }
            set { level_upgrade = value; }
        }

        public int Tower_Price
        {
            get { return tower_price; }
            set { tower_price = value; }
        }

        public float Value_Total
        {
            get { return value_total; }
            set { value_total = value; }
        }

        #endregion



        public Tower_base(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, bool placed, bool getting_placed, bool tower_selected)
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {

            this.placed = placed;
            this.getting_placed = getting_placed;
            this.tower_selected = tower_selected;
            level_upgrade = 1;



            //set the brush and pen that need to be used
            brush = new SolidBrush(Color.White);
            pen = new Pen(Color.Red, 2);

            // stopwatch needs to be created/initialised
            stopwatch = new Stopwatch();



        }


        public override void Update(float deltatime)
        {



            if (getting_placed == true)
            {

                co_ords.X = GameScreen.Mouse_Position.X - Sprite.Width / 2;
                co_ords.Y = GameScreen.Mouse_Position.Y - Sprite.Height / 2;



            }

            if(stopwatch.Elapsed.Seconds > 60)
            {
                stopwatch.Stop();
            }

            if (placed == true && target_enemy == null)
            {
                foreach (Objects_Game monsters in GameScreen.Objects)
                {
                    if ((monsters as Enemy) != null)
                    {
                        if (((Enemy)monsters).Health_Current <= 0)
                        {
                            ((Enemy)monsters).Death();
                        }



                        // the tower needs to calculate the distance to an enemy
                        float Target_EnemyX = monsters.Positionxy.X + monsters.Sprite.Width / 2;
                        float Target_EnemyY = monsters.Positionxy.Y + monsters.Sprite.Height / 2;


                        float fromX = this.Positionxy.X + this.Sprite.Width / 2;
                        float fromY = this.Positionxy.Y + this.Sprite.Height / 2;

                        float x = Target_EnemyX - fromX;
                        float y = Target_EnemyY - fromY;
                        float Distance_to_target = (float)Math.Sqrt(x * x + y * y);

                        // the closest monster will be set as target

                        if(Distance_to_target < tower_range + 10 )
                        {
                            if((this as Tower_Slow) != null)
                            {
                                if(!((Enemy)monsters).Slowed)
                                {
                                    target_enemy = (Enemy)monsters;
                                    break;

                                }
                                else
                                {
                                    target_enemy = (Enemy)monsters;
                                }
                            }
                            else
                            {
                                target_enemy = (Enemy)monsters;
                                break;

                            }

                        }

                    }
                }
            }
            else if (target_enemy != null)
            {

                float Target_EnemyX = target_enemy.Positionxy.X + target_enemy.Sprite.Width / 2;
                float Target_EnemyY = target_enemy.Positionxy.Y + target_enemy.Sprite.Height / 2;


                float fromX = this.Positionxy.X + this.Sprite.Width / 2;
                float fromY = this.Positionxy.Y + this.Sprite.Height / 2;

                float x = Target_EnemyX - fromX;
                float y = Target_EnemyY - fromY;
                float Distance_to_target = (float)Math.Sqrt(x * x + y * y);


                if(target_enemy.Health_Current <= 0)
                {
                    target_enemy.Death();
                    foreach(Objects_Game monster in GameScreen.Objects)
                    {
                        if((monster as Enemy) != null)
                        {
                            if(((Enemy)monster).Health_Current <= 0)
                            {
                                ((Enemy)monster).Death();
                            }

                        }

                    }
                    target_enemy = null;

                }
                else if (Distance_to_target < tower_range + 10)
                {

                    if(stopwatch.ElapsedMilliseconds > speeds || !stopwatch.IsRunning)
                    {
                        if((this as Tower_normal) != null)
                        {
                            GameScreen.New_Objects.Add(new Bullet((@"sprites\towers\Bullet_MG.png"), new PointF(co_ords.X + (Sprite.Width / 2), co_ords.Y + (Sprite.Width / 2)), 1, 1, (2000 - speeds), target_enemy.Positionxy));
                            target_enemy.Health_Current -= tower_damage;

                        }


                        if((this as Tower_missile) != null)
                        {
                            GameScreen.New_Objects.Add(new Bullet((@"sprites\towers\Missile.png"), new PointF(co_ords.X + (Sprite.Width / 2), co_ords.Y + (Sprite.Width / 2)), 1, 1, (2000 - speeds), target_enemy.Positionxy));

                            foreach(Objects_Game monster in GameScreen.Objects)
                            {
                                if((monster as Enemy) != null)
                                {

                                    // distance needs to be calculated to the next enemy
                                    float monster_target_x = monster.Positionxy.X + monster.Sprite.Width / 2;
                                    float monster_target_y = monster.Positionxy.Y + monster.Sprite.Height / 2;

                                    float monster_from_x = target_enemy.Positionxy.X + target_enemy.Sprite.Width / 2;
                                    float monster_from_y = target_enemy.Positionxy.Y + target_enemy.Sprite.Height / 2;

                                    float monster_x = monster_target_x - monster_from_x;
                                    float monster_y = monster_target_y - monster_from_y;
                                    float monster_distance = (float)Math.Sqrt(monster_x * monster_x + monster_y * monster_y);

                                    if(monster_distance < ((Tower_missile)this).Splash_Range + 16)
                                    {
                                        ((Enemy)monster).Health_Current -= tower_damage;
                                    }
                                }
                            }

                        }


                        if((this as Tower_Slow) != null)
                        {
                            GameScreen.New_Objects.Add(new Bullet((@"sprites\towers\Bullet_Cannon.png"), new PointF(co_ords.X + (Sprite.Width / 2), co_ords.Y + (Sprite.Width / 2)), 1, 1, (2000 - speeds), target_enemy.Positionxy));



                            if(!target_enemy.Slowed)
                            {
                                if(!target_enemy.Immune)
                                {
                                    target_enemy.speed = target_enemy.speed * ((Tower_Slow)this).Slow_Speed;
                                    target_enemy.Slow_Enemy();
                                }
                            }
                            else
                            {
                                if (target_enemy.Speed_Temp * ((Tower_Slow)this).Slow_Speed < target_enemy.speed)
                                {
                                    target_enemy.speed = target_enemy.Speed_Temp;

                                    target_enemy.speed = target_enemy.speed * ((Tower_Slow)this).Slow_Speed;
                                    target_enemy.Slow_Enemy();

                                }
                            }

                            target_enemy.Health_Current -= tower_damage;

                            target_enemy = null;
                        }

                        stopwatch.Reset();
                        stopwatch.Start();
                    }
        
                }
                else
                {
                    target_enemy = null;
                }
            }




            base.Update(deltatime);
        }




        public void Selection()
        {
            tower_selected = true;

            GameScreen.New_Objects.Add(new Level_Up((@"Sprites\Buttons\upgrade.png"), new PointF(630, 460), 0, 1, 1, this));

            GameScreen.New_Objects.Add(new Tower_Sell((@"Sprites\Buttons\sell.png"), new PointF(630, 400), 0, 1 ,1 , this));
        }


        public void Deselection()
        {
            tower_selected = false;

            foreach(Objects_Game obj in GameScreen.Objects)
            {
                if((obj as Level_Up) != null || (obj as Tower_Sell) != null)
                {
                    GameScreen.Remove_Objects.Add(obj);
                }
            }
        }


        public override void Draw(Graphics dc)
        {

            base.Draw(dc);





            if( tower_selected || getting_placed)
            {

                // need to define the pens that are going to be used when showing tower stats
                Font bold = new Font("Arial", 13, FontStyle.Bold);
                Font Normal = new Font("Arial", 8, FontStyle.Regular);

                pen.Color = Color.Black;
                dc.DrawEllipse(pen, Positionxy.X - tower_range + Sprite.Size.Width / 2, Positionxy.Y - tower_range + Sprite.Size.Height / 2, tower_range * 2, tower_range * 2);


                if(tower_selected == true)
                {
                    dc.DrawRectangle(pen, Hit_box.X, Hit_box.Y, Hit_box.Width, Hit_box.Height);

                    //Current stats
                    dc.DrawString("Upgrade tower: Level " + level_upgrade, bold, Brushes.Black, 580, 280);

                    dc.DrawString("Now: ", bold, Brushes.Black, 580, 325);
                    dc.DrawString("Damage: ", bold, Brushes.Black, 580, 350);
                    dc.DrawString(tower_damage.ToString(), bold, Brushes.Black, 650, 350);

                    if((this as Tower_Slow) != null)
                    {

                        dc.DrawString("Slow: ", bold, Brushes.Black, 580, 370);
                        dc.DrawString((Math.Round((1 - (((Tower_Slow)this).Slow_Speed)) * 100, 0)).ToString() + "%", bold, Brushes.Black, 630, 370);
                    }





                    if((this as Tower_Slow) == null && level_upgrade < 20 || (this as Tower_Slow) != null)
                    {

                        dc.DrawString("After: ", bold, Brushes.Black, 680, 325);

                        if ((this as Tower_normal) != null)
                        {
                            dc.DrawString("Damage: ", bold, Brushes.Black, 680, 350);
                            dc.DrawString(((int)tower_damage * 1.6).ToString(), bold, Brushes.Black, 756, 351);
                        }
                        else if ((this as Tower_Slow) != null)
                        {
                            dc.DrawString("Damage: ", bold, Brushes.Black, 680, 350);
                            dc.DrawString(((int)tower_damage * 1.6).ToString(), bold, Brushes.Black, 756, 351);
                        }
                        else if((this as Tower_missile) != null)
                        {
                            dc.DrawString("Damage: ", bold, Brushes.Black, 680, 350);
                            dc.DrawString(((int)tower_damage * 1.6).ToString(), bold, Brushes.Black, 756, 351);
                        }

                        if((this as Tower_Slow) != null)
                        {
                            dc.DrawString("Slow: ", bold, Brushes.Black, 680, 370);
                            dc.DrawString((Math.Round((1 - (((Tower_Slow)this).Slow_Speed)) * 100, 0) + 5).ToString() + "%", bold, Brushes.Black, 725, 370);
                        }






                        dc.DrawString("Upgrade cost: ", bold, Brushes.Black, 580, 300);
                        dc.DrawString("£ " + tower_price, bold, Brushes.Black, 700, 300);
                    }

                    //

                }


            }





        }

        public abstract void Next_Upgrade();

       


    }
}
