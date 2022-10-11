using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{
    class Tower_normal : Tower_base
    {

        public Tower_normal(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, bool placed, bool getting_placed, bool tower_selected)
            : base(image_file, startposition, animation_speed, scaleFactor, speed, placed, getting_placed, tower_selected)
        {

            tower_damage = 8;
            tower_range = 100;

            tower_price = 10;
            Value_Total = tower_price;


        }




        public override void Next_Upgrade()
        {
            if(level_upgrade < 30)
            {
                GameScreen.Money -= tower_price;

                level_upgrade = level_upgrade + 1;

                Value_Total += tower_price;

                tower_damage = (int)(tower_damage * 1.6);
                tower_price = (int)(tower_price * 1.6);

            }

        }




        public override void UpdateAnimation(float fps)
        {

            if((!placed && GameScreen.Money < tower_price))
            {
                Sprite = Image.FromFile(@"Sprites\towers\NoMoney.png");
            }
            else if (!placed && GameScreen.Money >= tower_price)
            {
                Sprite = Image.FromFile(@"Sprites\towers\MG.png");
            }

            if(level_upgrade == 2)
            {
                this.Sprite = Image.FromFile(@"Sprites\towers\MG2.png");
            }
            if(level_upgrade == 3)
            {
                this.Sprite = Image.FromFile(@"Sprites\towers\MG3.png");
            }

            base.UpdateAnimation(fps);
        }



    }
}
