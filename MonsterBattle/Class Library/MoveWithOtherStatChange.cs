using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Interfaces;

namespace MonsterBattle.Class_Library
{
    public class MoveWithOtherStatChange : IMoveWithStatChange
    {
        public Stats_Types stat_change_type { get; set; }
        public float stat_chance { get; set; }
        public int stat_level { get; set; }

        public MoveWithOtherStatChange(Stats_Types other_stat_change_type, float other_stat_chance, int other_stat_level)
        {
            this.stat_level = other_stat_level;
            this.stat_change_type = other_stat_change_type;
            this.stat_chance = other_stat_chance;
        }
    }
}
