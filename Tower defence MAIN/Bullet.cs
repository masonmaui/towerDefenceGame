using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{
    class Bullet : Objects_Game
    {

        //used to check the destination of the projectiles and distances
        private PointF start_poition;
        private PointF target;
        private float distance;
        private float x_difference;
        private float y_difference;

        public Bullet(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, PointF destination)
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {

            co_ords.X -= 8;
            co_ords.Y -= 8;

            this.start_poition = this.co_ords;
            this.target = destination;



            // calculates the distance between the enemy and the tower for the x and y co_ords
            float target_x_position = destination.X;
            float target_y_position = destination.Y;


            // postion of the tower that is shooting
            float tower_x_position = co_ords.X;
            float tower_y_position = co_ords.Y;

            //difference between the 2 poistions
            x_difference = target_x_position - tower_x_position;
            y_difference = target_y_position - tower_y_position;

            distance = (float)Math.Sqrt(x_difference * x_difference + y_difference * y_difference);
        }

        public override void Update(float deltatime)
        {
            co_ords.X += deltatime * (speeds * x_difference / 150);
            co_ords.Y += deltatime * (speeds * y_difference / 150);


            float bullet_position_x = co_ords.X;
            float bullet_position_y = co_ords.Y;

            float starting_position_x = start_poition.X;
            float starting_position_y = start_poition.Y;


            float new_difference_x = bullet_position_x - starting_position_x;
            float new_difference_y = bullet_position_y - starting_position_y;

            float distance_travelled = (float)Math.Sqrt(new_difference_x * new_difference_x + new_difference_y * new_difference_y);

            if(distance_travelled >= distance)
            {
                GameScreen.Remove_Objects.Add(this);
            }
        }


    }
}
