using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{
    class Tower_missile : Tower_base
    {

        private int splash_range;

        public int Splash_Range
        {
            get { return splash_range; }
            set { splash_range = value; }
        }

        public Tower_missile(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, bool placed, bool getting_placed, bool tower_selected)
            : base(image_file, startposition, animation_speed, scaleFactor, speed, placed, getting_placed, tower_selected)
        {

            tower_damage = 10;
            tower_range = 70;
            splash_range = 50;
            tower_price = 30;
            Value_Total = tower_price;


        }


        public override void Next_Upgrade()
        {
            if(level_upgrade < 20)
            {
                GameScreen.Money -= tower_price;

                level_upgrade = level_upgrade + 1;

                value_total += tower_price;

                tower_damage = Convert.ToInt32(tower_damage * 1.7);
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
                Sprite = Image.FromFile(@"Sprites\towers\Missile_Launcher.png");
            }

            if(this.level_upgrade == 2)
            {
                this.Sprite = Image.FromFile(@"Sprites\towers\Missile_Launcher2.png");
            }
            if (this.level_upgrade == 3)
            {
                this.Sprite = Image.FromFile(@"Sprites\towers\Missile_Launcher3.png");
            }

            base.UpdateAnimation(fps);
        }
    }

}
