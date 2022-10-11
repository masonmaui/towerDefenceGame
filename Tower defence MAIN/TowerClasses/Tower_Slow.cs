using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace Tower_defence_MAIN
{
    class Tower_Slow : Tower_base
    {



        private float slow_speed;

        public float Slow_Speed
        {
            get { return slow_speed; }
            set { slow_speed = value; }
        }

        public Tower_Slow(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, bool placed, bool getting_placed, bool tower_selected)
            : base(image_file, startposition, animation_speed, scaleFactor, speed, placed, getting_placed, tower_selected)
        {


            tower_damage = 5;
            tower_range = 70;

            tower_price = 15;
            Value_Total = tower_price;

            slow_speed = 0.80f;



        }



        public override void Next_Upgrade()
        {
            if(level_upgrade < 30)
            {
                GameScreen.Money -= tower_price;

                level_upgrade = level_upgrade + 1;

                Value_Total += tower_price;

                tower_damage = (int)(tower_damage * 1.6);
                slow_speed -= 0.05f;

                tower_price = (int)(tower_price * 1.6);

            }


        }

        public override void UpdateAnimation(float fps)
        {

            if ((!placed && GameScreen.Money < tower_price))
            {
                Sprite = Image.FromFile(@"Sprites\towers\NoMoney.png");
            }
            else if (!placed && GameScreen.Money >= tower_price)
            {
                Sprite = Image.FromFile(@"Sprites\towers\Cannon.png");
            }

            if (this.level_upgrade == 2)
            {
                this.Sprite = Image.FromFile(@"Sprites\towers\Cannon2.png");
            }

            if (this.level_upgrade == 2)
            {
                this.Sprite = Image.FromFile(@"Sprites\towers\Cannon3.png");
            }
            base.UpdateAnimation(fps);

        }
    }
}
