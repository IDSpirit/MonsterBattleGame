using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Interfaces;

namespace MonsterBattle.Class_Library
{
    public class MoveWithSelfStatChange : IMoveWithStatChange
    {
        public Stats_Types stat_change_type { get; set; }
        public float stat_chance { get; set; }
        public int stat_level { get; set; }

        public MoveWithSelfStatChange(Stats_Types self_stat_change_type, float self_stat_chance, int self_stat_level)
        {
            this.stat_level = self_stat_level;
            this.stat_change_type = self_stat_change_type;
            this.stat_chance = self_stat_chance;
        }
    }
}
