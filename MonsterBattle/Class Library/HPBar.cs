using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle;

namespace MonsterBattle.Class_Library
{
    public class HPBar
    {
        public string colour { get; set; }
        public int length { get; set; }
        public int maxLength { get; set; }

        public HPBar(int hp, int maxHP)
        {
            double hpPercentage = ((double)hp / (double)maxHP);
            if (hpPercentage >= 0.5)
                colour = "#FF4EC14E";
            else if (hpPercentage >= 0.15 && hpPercentage < 0.5)
                colour = "#FFFFFF00";
            else
                colour = "#FFEC3214";

            maxLength = 200;

            length = (int) Math.Round(maxLength * hpPercentage, MidpointRounding.AwayFromZero);
        }
    }
}
