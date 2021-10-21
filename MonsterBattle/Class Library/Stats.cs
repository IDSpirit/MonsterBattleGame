using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Class_Library.Enums;

namespace MonsterBattle.Class_Library
{
    public class Stats
    {
        #region Properties And Fields
        public float ph_attack_stat_change = 1f;
        public float ph_defense_stat_change = 1f;
        public float sp_attack_stat_change = 1f;
        public float sp_defense_stat_change = 1f;
        public float speed_stat_change = 1f;
        //public Stat_Modifiers_Levels stat_Modifiers_Unchanged { get; set; }
        public float stat_Modifiers_Changed { get; set; }
        private int _ph_attack_modifier = 0;
        public int ph_attack_modifier
        {
            get { return _ph_attack_modifier; } //The private backing field is necessary to avoid an infinite recursion error
            set 
            {
                if (value >= 6)
                {
                    _ph_attack_modifier = 6;
                }
                else if (value <= -6)
                {
                    _ph_attack_modifier = -6;
                }
                else
                {
                    _ph_attack_modifier = value;
                }

                if (ph_attack_modifier > 0)
                    ph_attack_modifier_string = $"+{ph_attack_modifier}";
                else if (ph_attack_modifier < 0)
                    ph_attack_modifier_string = $"{ph_attack_modifier}";
                else
                    ph_attack_modifier_string = "~";
            }
        }

        private string _ph_attack_modifer_string;
        public string ph_attack_modifier_string
        {
            get
            {
                return _ph_attack_modifer_string;
            }
            set
            {
                _ph_attack_modifer_string = $"({value} stage)";
            }
        }

        private int _ph_defense_modifier = 0;
        public int ph_defense_modifier
        {
            get { return _ph_defense_modifier; } //The private backing field is necessary to avoid an infinite recursion error
            set
            {
                if (value >= 6)
                {
                    _ph_defense_modifier = 6;
                }
                else if (value <= -6)
                {
                    _ph_defense_modifier = -6;
                }
                else
                {
                    _ph_defense_modifier = value;
                }


                if (ph_defense_modifier > 0)
                    ph_defense_modifier_string = $"+{ph_defense_modifier}";
                else if (ph_defense_modifier < 0)
                    ph_defense_modifier_string = $"{ph_defense_modifier}";
                else
                    ph_defense_modifier_string = "~";
            }
        }

        private string _ph_defense_modifier_string;
        public string ph_defense_modifier_string
        {
            get
            {
                return _ph_defense_modifier_string;
            }
            set
            {
                _ph_defense_modifier_string = $"({value} stage)";
            }
        }

        private int _sp_attack_modifier = 0;
        public int sp_attack_modifier
        {
            get { return _sp_attack_modifier; } //The private backing field is necessary to avoid an infinite recursion error
            set
            {
                if (value >= 6)
                {
                    _sp_attack_modifier = 6;
                }
                else if (value <= -6)
                {
                    _sp_attack_modifier = -6;
                }
                else
                {
                    _sp_attack_modifier = value;
                }

                if (sp_attack_modifier > 0)
                    sp_attack_modifier_string = $"+{sp_attack_modifier}";
                else if (sp_attack_modifier < 0)
                    sp_attack_modifier_string = $"{sp_attack_modifier}";
                else
                    sp_attack_modifier_string = "~";
            }
        }

        private string _sp_attack_modifer_string;
        public string sp_attack_modifier_string
        {
            get
            {
                return _sp_attack_modifer_string;
            }
            set
            {
                _sp_attack_modifer_string = $"({value} stage)";
            }
        }

        private int _sp_defense_modifier = 0;
        public int sp_defense_modifier
        {
            get { return _sp_defense_modifier; } //The private backing field is necessary to avoid an infinite recursion error
            set
            {
                if (value >= 6)
                {
                    _sp_defense_modifier = 6;
                }
                else if (value <= -6)
                {
                    _sp_defense_modifier = -6;
                }
                else
                {
                    _sp_defense_modifier = value;
                }

                if (sp_defense_modifier > 0)
                    sp_defense_modifier_string = $"+{sp_defense_modifier}";
                else if (sp_defense_modifier < 0)
                    sp_defense_modifier_string = $"{sp_defense_modifier}";
                else
                    sp_defense_modifier_string = "~";
            }
        }

        private string _sp_defense_modifer_string;
        public string sp_defense_modifier_string
        {
            get
            {
                return _sp_defense_modifer_string;
            }
            set
            {
                _sp_defense_modifer_string = $"({value} stage)";
            }
        }

        private int _speed_modifier = 0;
        public int speed_modifier
        {
            get { return _speed_modifier; } //The private backing field is necessary to avoid an infinite recursion error
            set
            {
                if (value >= 6)
                {
                    _speed_modifier = 6;
                }
                else if (value <= -6)
                {
                    _speed_modifier = -6;
                }
                else
                {
                    _speed_modifier = value;
                }

                if (speed_modifier > 0)
                    speed_modifier_string = $"+{speed_modifier}";
                else if (speed_modifier < 0)
                    speed_modifier_string = $"{speed_modifier}";
                else
                    speed_modifier_string = "~";
            }
        }

        private string _speed_modifer_string;
        public string speed_modifier_string
        {
            get
            {
                return _speed_modifer_string;
            }
            set
            {
                _speed_modifer_string = $"({value} stage)";
            }
        }
        #endregion

        #region Main Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stat_Change_Level"> This is the stat level of a particular stat</param>
        /// <param name="stats_Types">This is the type that the stat change is for (e.g. physical attack)</param>
        public void stat_modifier_method(int stat_Change_Level, Stats_Types stats_Types)
        {

            var modifier = 0;
            var indicator = "";

            Modifier_Method_1(stats_Types, ref modifier, ref indicator);

            modifier = modifier + stat_Change_Level;

            Modifier_Method_2(ref modifier);

            Modifier_Method_3(ref indicator, ref modifier);

        }
        #endregion

        #region Supporting Methods
        private void Modifier_Method_3(ref string indicator, ref int modifier)
        {
            switch (indicator)
            {
                case "ph_attack":
                    ph_attack_stat_change = stat_Modifiers_Changed;
                    ph_attack_modifier = modifier;
                    return;
                case "ph_defense":
                    ph_defense_stat_change = stat_Modifiers_Changed;
                    ph_defense_modifier = modifier;
                    return;
                case "sp_attack":
                    sp_attack_stat_change = stat_Modifiers_Changed;
                    sp_attack_modifier = modifier;
                    return;
                case "sp_defense":
                    sp_defense_stat_change = stat_Modifiers_Changed;
                    sp_defense_modifier = modifier;
                    return;
                case "speed":
                    speed_stat_change = stat_Modifiers_Changed;
                    speed_modifier = modifier;
                    return;
            }
        }
       

        private void Modifier_Method_2(ref int modifier)
        {
            switch (modifier)
            {
                case -6:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_6 / 100);
                    break;
                case -5:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_5 / 100);
                    break;
                case -4:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_4 / 100);
                    break;
                case -3:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_3 / 100);
                    break;
                case -2:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_2 / 100);
                    break;
                case -1:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_1 / 100);
                    break;
                case 0:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level0 / 100);
                    break;
                case 1:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level1 / 100);
                    break;
                case 2:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level2 / 100);
                    break;
                case 3:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level3 / 100);
                    break;
                case 4:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level4 / 100);
                    break;
                case 5:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level5 / 100);
                    break;
                case 6:
                    stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level6 / 100);
                    break;
                default:
                    if (modifier > 6)
                    {
                        stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level6 / 100);
                        break;
                    }
                    else if (modifier < 6)
                    {
                        stat_Modifiers_Changed = ((float) Stat_Modifiers_Levels.level_6 / 100);
                        break;
                    }

                    break;
            }
        }

        private void Modifier_Method_1(Stats_Types stats_Types, ref int modifier, ref string indicator)
        {
            switch (stats_Types)
            {
                case Stats_Types.ph_attack:
                    stat_Modifiers_Changed = ph_attack_stat_change;
                    modifier = ph_attack_modifier;
                    indicator = "ph_attack";
                    break;
                case Stats_Types.ph_defense:
                    stat_Modifiers_Changed = ph_defense_stat_change;
                    modifier = ph_defense_modifier;
                    indicator = "ph_defense";
                    break;
                case Stats_Types.sp_attack:
                    stat_Modifiers_Changed = sp_attack_stat_change;
                    modifier = sp_attack_modifier;
                    indicator = "sp_attack";
                    break;
                case Stats_Types.sp_defense:
                    stat_Modifiers_Changed = sp_defense_stat_change;
                    modifier = sp_defense_modifier;
                    indicator = "sp_defense";
                    break;
                case Stats_Types.speed:
                    stat_Modifiers_Changed = speed_stat_change;
                    modifier = speed_modifier;
                    indicator = "speed";
                    break;
            }
        }
        #endregion
    }
}

