using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{

    class PlayerMouse : Objects_Game
    {
        // mouse clicked
        private bool click;
        // x and y poition it was clickd in
        private PointF Position_click;
        // if the space the user chooses is occupied
        private bool Tower_can_place;
        // if the user is placing a tower or not
        private bool Placing_tower;


        public PlayerMouse(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed)
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {
            // sets it so at the start of the programme the player is not set to placing
            Placing_tower = false;


        }


        public void HandlingMousePos()
        {
            this.Positionxy = GameScreen.Mouse_Position;



        }


        public override void Collision_True(Objects_Game other)
        {




            // check what towers are selected
            if ((other as Tower_base) != null)
            {
                if (click && !Placing_tower && GameScreen.Mouse_Position.X > 540)
                {
                    if ((other as Tower_normal) != null)
                    {
                        if (GameScreen.Money >= ((Tower_normal)other).Tower_Price)
                        {
                            Placing_tower = true;
                            GameScreen.Tower_Placing = new Tower_normal(@"Sprites\towers\MG.png", new PointF(Position_click.X - 16, Position_click.Y - 16), 0, 1, 1, false, true, false);
                            GameScreen.New_Objects.Add(GameScreen.Tower_Placing);
                        }
                    }


                    if ((other as Tower_missile) != null)
                    {
                        if (GameScreen.Money >= ((Tower_missile)other).Tower_Price)
                        {
                            Placing_tower = true;
                            GameScreen.Tower_Placing = new Tower_missile(@"Sprites\towers\Missile_Launcher.png", new PointF(Position_click.X - 16, Position_click.Y - 16), 0, 1, 1, false, true, false);
                            GameScreen.New_Objects.Add(GameScreen.Tower_Placing);
                        }
                    }

                    if ((other as Tower_Slow) != null)
                    {
                        if (GameScreen.Money >= ((Tower_Slow)other).Tower_Price)
                        {
                            Placing_tower = true;
                            GameScreen.Tower_Placing = new Tower_Slow(@"Sprites\towers\Cannon.png", new PointF(Position_click.X - 16, Position_click.Y - 16), 0, 1, 1, false, true, false);
                            GameScreen.New_Objects.Add(GameScreen.Tower_Placing);
                        }
                    }
                }
                else if (click && Placing_tower == true && GameScreen.Mouse_Position.X > 540)
                {
                    GameScreen.Remove_Objects.Add(GameScreen.Tower_Placing);
                    GameScreen.Tower_Placing = null;
                    Placing_tower = false;
                }




                if (click == true && Placing_tower == true && GameScreen.Mouse_Position.X < 520)
                {
                    // tower can be placed
                    Tower_can_place = true;

                    RectangleF New_Tower_Hit_Box = new RectangleF(Position_click.X - 16, Position_click.Y - 16, 32, 32);

                    foreach (Objects_Game objects in GameScreen.Objects)
                    {
                        if ((objects as Tower_base) != null && ((Tower_base)objects).Placed)
                        {
                            // this is the old hitbox
                            RectangleF Old_Tower_Hit_Box = objects.Hit_box;


                            if (New_Tower_Hit_Box.IntersectsWith(Old_Tower_Hit_Box))
                            {

                                Tower_can_place = false;
                                break;
                            }

                        }

                    }




                    for (int i = 0; i < (GameScreen.Path_Checkpoints.Count() - 1); i++)
                    {

                        // makes a Hit box for the checkpoints
                        RectangleF Path_Hit_Box;

                        Path_Hit_Box = new RectangleF(GameScreen.Path_Checkpoints[i].X, GameScreen.Path_Checkpoints[i].Y, ((GameScreen.Path_Checkpoints[(i + 1)].X - GameScreen.Path_Checkpoints[i].X) + 32), ((GameScreen.Path_Checkpoints[(i + 1)].Y - GameScreen.Path_Checkpoints[i].Y) + 32));


                        if (New_Tower_Hit_Box.IntersectsWith(Path_Hit_Box))
                        {
                            Tower_can_place = false;
                            break;
                        }
                    }


                    if (Tower_can_place == true)
                    {
                        // these will all remove the money from the players currency 
                        if ((other as Tower_normal) != null)
                        {
                            GameScreen.New_Objects.Add(new Tower_normal(@"Sprites\towers\MG.png", new PointF(Position_click.X - 16, Position_click.Y - 16), 0, 1, 400, true, false, false));

                            GameScreen.Money -= ((Tower_normal)other).Tower_Price;

                        }


                        if ((other as Tower_missile) != null)
                        {
                            GameScreen.New_Objects.Add(new Tower_missile(@"Sprites\towers\Missile_launcher.png", new PointF(Position_click.X - 16, Position_click.Y - 16), 0, 1, 400, true, false, false));

                            GameScreen.Money -= ((Tower_missile)other).Tower_Price;

                        }


                        if ((other as Tower_Slow) != null)
                        {
                            GameScreen.New_Objects.Add(new Tower_Slow(@"Sprites\towers\Cannon.png", new PointF(Position_click.X - 16, Position_click.Y - 16), 0, 1, 400, true, false, false));

                            GameScreen.Money -= ((Tower_Slow)other).Tower_Price;

                        }

                        GameScreen.Remove_Objects.Add(GameScreen.Tower_Placing);
                        GameScreen.Tower_Placing = null;
                        Placing_tower = false;


                    }

                }
            }

            // if object that is clicked is placed and is a tower and cursor is not placing
            if((other as Tower_base) != null && (other as Tower_base).Placed && click == true && !Placing_tower && GameScreen.Mouse_Position.X < 520)
            {
                //checks to see if the tower has been slected
                if(!(other as Tower_base).Tower_Selected)
                {
                    // need to remove the other towers if they are selected
                    foreach(Objects_Game objects in GameScreen.Objects)
                    {
                        if((objects as Tower_base) != null)
                        {
                            if((objects as Tower_base).Placed && (objects as Tower_base).Tower_Selected)
                            {
                                (objects as Tower_base).Deselection();
                            }
                        }

                    }

                    (other as Tower_base).Selection();
                }
                else
                {

                    // remove the selection from the tower
                    (other as Tower_base).Deselection();

                }

            }


            //used to see if user clicks on the upgrade button
            if((other as Level_Up) != null)
            {
                if(click == true && !Placing_tower && GameScreen.Mouse_Position.X > 520)
                {
                    ((Level_Up)other).Tower_Level_Up();
                }
                if(click == true && Placing_tower && GameScreen.Mouse_Position.X > 520)
                {
                    GameScreen.Remove_Objects.Add(GameScreen.Tower_Placing);
                    GameScreen.Tower_Placing = null;
                    Placing_tower = false;
                }
            }



            // checks to see if the user has clicke the sell button
            if ((other as Tower_Sell) != null)
            {
                if (click == true && !Placing_tower && GameScreen.Mouse_Position.X > 520)
                {
                    ((Tower_Sell)other).Sell_Tower();

                    foreach(Objects_Game obj in GameScreen.Objects)
                    {
                        if((obj as Level_Up) != null || (obj as Tower_Sell) != null)
                        {
                            GameScreen.Remove_Objects.Add(obj);
                        }
                    }
                }

                if (click == true && Placing_tower && GameScreen.Mouse_Position.X > 520)
                {
                    GameScreen.Remove_Objects.Add(GameScreen.Tower_Placing);
                    GameScreen.Tower_Placing = null;
                    Placing_tower = false;
                }

            }
        }

        public override void Draw(Graphics dc)
        {
            //Font F = new Font("Arial", 25);
            //dc.DrawString("PointerPos " + (Positionxy.X + ":" + Positionxy.Y), F, Brushes.Gold, 160, 0);

            base.Draw(dc);
        }

        public void Mouse_click(bool clicked, float deltatime)
        {

            if (clicked == true)
            {
                this.click = true;
                Position_click = GameScreen.Mouse_Position;
                base.Update(deltatime);

                this.click = false;
            }



        }
    }
}
