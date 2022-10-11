using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_defence_MAIN
{
    class Tower_Sell : Objects_Game
    {

        //variable to look for the tower that needs to be sold
        private Tower_base sell_target;


        public Tower_Sell(string image_file, PointF startposition, float animation_speed, float scaleFactor, float speed, Tower_base Sell_Target)
            : base(image_file, startposition, animation_speed, scaleFactor, speed)
        {
            this.sell_target = Sell_Target;

        }

        public void Sell_Tower()
        {
            GameScreen.Money +=(int)(sell_target.Value_Total * 0.75f);
            GameScreen.Remove_Objects.Add(sell_target);

        }
    }
}
