using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle;
using MonsterBattle.Class_Library;

namespace MonsterBattleUI.Models
{
    public static class CapitaliseExtensions
    {
        public static string CapitaliseFirstLetter(All_types pokemonType)
        {
            var pType1 = pokemonType.ToString().ToLower();
            var newPType1 = pType1.Substring(1);
            return newPType1.Insert(0, pType1[0].ToString().ToUpper());
        }
    }
}
