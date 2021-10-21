using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Class_Library;

namespace MonsterBattle.Interfaces
{
    public interface IMoveWithStatChange
    {
        Stats_Types stat_change_type { get; set; }
        float stat_chance { get; set; }
        int stat_level { get; set; }
    }
}
