using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{
    class Level_Up : Objects_Game
    {


        private Tower_base target_level_up;


        public Level_Up(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, Tower_base Target_Level_Up )
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {
            this.target_level_up = Target_Level_Up;
        }


        public void Tower_Level_Up()
        {
            if(GameScreen.Money >= target_level_up.Tower_Price)
            {
                if((target_level_up as Tower_normal) != null)
                {
                    ((Tower_normal)target_level_up).Next_Upgrade();
                }
                if ((target_level_up as Tower_missile) != null)
                {
                    ((Tower_missile)target_level_up).Next_Upgrade();
                }
                if ((target_level_up as Tower_Slow != null))
                {
                    ((Tower_Slow)target_level_up).Next_Upgrade();
                }
            }



        }

        public override void UpdateAnimation(float fps)
        {

            if(GameScreen.Money < target_level_up.Tower_Price)
            {
                Sprite = Image.FromFile(@"Sprites\Buttons\upgradeOpac.png");
            }
            else if(GameScreen.Money >= target_level_up.Tower_Price)
            {
                Sprite = Image.FromFile(@"Sprites\Buttons\upgrade.png");
            }




            base.UpdateAnimation(fps);
        }


    }
}
