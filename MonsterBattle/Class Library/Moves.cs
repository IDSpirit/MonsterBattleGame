using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Class_Library;
using MonsterBattle.Interfaces;

namespace MonsterBattle.Class_Library
{
    public class Moves
    {
        #region Properties And Fields

        public string move_name { get; set; }
        private int _move_power = 0;

        public int move_power
        {
            get
            {
                return _move_power;
            }
            set
            {
                _move_power = value;
            }
        }

        public All_types move_type { get; set; }
        private int _accuracy;

        public int accuracy
        {
            get
            {
                return _accuracy;
            }
            set
            {
                if (value < 0)
                    _accuracy = 0;
                else
                    _accuracy = value;
            }
        }

        public int pp_max { get; set; }
        private int _pp_available;
        public int pp_available
        {
            get { return _pp_available; }
            set
            {
                if (value <= 0)
                {
                    _pp_available = 0;
                }
                else if (value >= pp_max)
                {
                    _pp_available = pp_max;
                }
                else 
                {
                    _pp_available = value;
                }
            }
        }
        private bool _contact = false;
        public bool contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
            }
        }
        public Stats_Types attack_category { get; set; }
        private Stats_Types _defense_category = Stats_Types.unknown;
        public Stats_Types defense_category
        {
            get
            {
                return _defense_category;
            }
            set
            {
                _defense_category = value;
            }
        }
        private int _priority = 0;
        public int priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
            }
        }

        private bool _self_stat_change = false;
        public bool self_stat_change
        {
            get
            {
                return _self_stat_change;
            }
            set
            {
                _self_stat_change = value;
            }
        }
        private bool _other_stat_change = false;

        public bool other_stat_change
        {
            get
            {
                return _other_stat_change;
            }
            set
            {
                _other_stat_change = value;
            }
        }

        private int _fixed_dmg = 0;
        public int fixed_dmg
        {
            get
            {
                return _fixed_dmg;
            }
            set
            {
                _fixed_dmg = value;
            }
        }
        private int _multi_turns_number = 0;
        public int multi_turns_number
        {
            get
            {
                return _multi_turns_number;
            }
            set
            {
                _multi_turns_number = value;
            }
        }
        private int _multi_turns_tracker = 0;
        public int multi_turns_tracker
        {
            get
            {
                return _multi_turns_tracker;
            }
            set
            {
                _multi_turns_tracker = value;
            }
        }
        public string attack_category_icon { get; set; }
        private List<IMoveWithStatChange> _moves_with_self_stat_change = null;
        public List<IMoveWithStatChange> moves_with_self_stat_change
        {
            get
            {
                return _moves_with_self_stat_change;
            }
            set
            {
                _moves_with_self_stat_change = value;
            }
        }

        private List<IMoveWithStatChange> _moves_with_other_stat_change = null;
        public List<IMoveWithStatChange> moves_with_other_stat_change
        {
            get
            {
                return _moves_with_other_stat_change;
            }
            set
            {
                _moves_with_other_stat_change = value;
            }
        }
        private bool _canHitFlying = false;
        public bool canHitFlying
        {
            get
            {
                return _canHitFlying;
            }
            set
            {
                _canHitFlying = value;
            }
        }
        private bool _canHitUnderground = false;
        public bool canHitUnderground
        {
            get
            {
                return _canHitUnderground;
            }
            set
            {
                _canHitUnderground = value;
            }
        }
        private int _percentageDrain = 0;
        public int percentageDrain
        {
            get
            {
                return _percentageDrain;
            }
            set
            {
                _percentageDrain = value;
            }
        }
        private int _critDenominator = 25;
        public int critDenominator { 
            get 
            {
                return _critDenominator;
            } 
            set
            {
                _critDenominator = value + 1;
            } 
        }
        public string attack_type_icon { get; set; }

        #endregion

        #region Constructor
        public Moves(string move_name, int move_power, All_types move_type, int accuracy, int pp_max, Stats_Types attack_category, Stats_Types defense_category = Stats_Types.unknown, bool contact = false, int priority = 0,
            int multi_turns_number = 0, int fixed_dmg = 0, List<IMoveWithStatChange> moves_with_self_stat_change = null, List<IMoveWithStatChange> moves_with_other_stat_change = null, bool canHitFlying = false, 
            bool canHitUnderground = false, int percentageDrain = 0, int critDenominator = 24)
        {
            this.move_name = move_name;
            this.move_power = move_power;
            this.move_type = move_type;
            this.accuracy = accuracy;
            this.pp_max = pp_max;
            this.pp_available = pp_max;
            this.contact = contact;
            this.priority = priority;
            this.canHitFlying = canHitFlying;
            this.canHitUnderground = canHitUnderground;
            this.self_stat_change = self_stat_change;
            this.other_stat_change = other_stat_change;
            this.multi_turns_number = multi_turns_number;
            this.multi_turns_tracker = multi_turns_tracker;
            this.percentageDrain = percentageDrain;
            this.critDenominator = critDenominator;

            if (this.multi_turns_number != 0)
                multi_turns_tracker = multi_turns_number + 1;

            if (moves_with_self_stat_change != null)
                this.self_stat_change = true;
            else
                this.self_stat_change = false;

            if (moves_with_other_stat_change != null)
                this.other_stat_change = true;
            else
                this.other_stat_change = false;

            this.fixed_dmg = fixed_dmg;
            this.attack_category = attack_category;
            if (defense_category == Stats_Types.unknown)
                this.defense_category = attack_category;
            else
                this.defense_category = defense_category;

            if (this.attack_category == Stats_Types.ph_attack)
                this.attack_category_icon = "/MonsterBattleUI;component/Icons/Physical_Move.png";
            else if (this.attack_category == Stats_Types.sp_attack)
                this.attack_category_icon = "/MonsterBattleUI;component/Icons/Special_Move.png";

            attack_type_icon = $"/Icons/{move_type.ToString().ToLower()}IC_BW.png";
            this.moves_with_self_stat_change = moves_with_self_stat_change;
            this.moves_with_other_stat_change = moves_with_other_stat_change;
        }

        public Moves(string move_name, All_types move_type, int accuracy, int pp_max,
            bool contact = false, int priority = 0,
            List<IMoveWithStatChange> moves_with_self_stat_change = null,
            List<IMoveWithStatChange> moves_with_other_stat_change = null)
        {
            this.move_name = move_name;
            this.move_type = move_type;
            this.accuracy = accuracy;
            this.pp_max = pp_max;
            this.attack_category = attack_category;
            this.contact = contact;
            this.priority = 0;
            this.moves_with_self_stat_change = moves_with_self_stat_change;
            this.moves_with_other_stat_change = moves_with_other_stat_change;

            if (moves_with_self_stat_change != null)
                this.self_stat_change = true;
            else
                this.self_stat_change = false;

            if (moves_with_other_stat_change != null)
                this.other_stat_change = true;
            else
                this.other_stat_change = false;

            attack_category_icon = "/MonsterBattleUI;component/Icons/Status_Move.png";
            attack_type_icon = $"/Icons/{move_type.ToString().ToLower()}IC_BW.png";

        }
        #endregion

        #region Main Method
        public static float Move_Effectiveness(ref List<string> messages, Pokemon self, Pokemon other, All_types attack_type, All_types def_type1, All_types def_type2 = All_types.unknown)
        {
            float modifier_stage = 1f;

            Dictionary<All_types, float> fire_defense = new Dictionary<All_types, float>()
            {
                { All_types.bug, 0.5f },
                { All_types.fairy, 0.5f },
                { All_types.fire, 0.5f },
                { All_types.steel, 0.5f },
                { All_types.grass, 0.5f},
                { All_types.ice, 0.5f},
                { All_types.ground, 2f},
                { All_types.rock, 2f},
                { All_types.water, 2f}
            };

            Dictionary<All_types, float> water_defense = new Dictionary<All_types, float>()
            {
                { All_types.fire, 0.5f },
                { All_types.ice, 0.5f },
                { All_types.steel, 0.5f },
                { All_types.water, 0.5f},
                { All_types.electric, 2f},
                { All_types.grass, 2f}
            };

            Dictionary<All_types, float> grass_defense = new Dictionary<All_types, float>()
            {
                { All_types.electric, 0.5f },
                { All_types.grass, 0.5f },
                { All_types.ground, 0.5f },
                { All_types.water, 0.5f },
                { All_types.bug, 2f},
                { All_types.flying, 2f},
                { All_types.fire, 2f},
                { All_types.ice, 2f},
                { All_types.poison, 2f}
            };

            Dictionary<All_types, float> electric_defense = new Dictionary<All_types, float>()
            {
                { All_types.flying, 0.5f },
                { All_types.electric, 0.5f },
                { All_types.steel, 0.5f},
                { All_types.ground, 2f}

            };

            Dictionary<All_types, float> psychic_defense = new Dictionary<All_types, float>()
            {
                { All_types.fighting, 0.5f },
                { All_types.psychic, 0.5f},
                { All_types.bug, 2f},
                { All_types.ghost, 0f}
            };

            Dictionary<All_types, float> ice_defense = new Dictionary<All_types, float>()
            {
                { All_types.ice, 0.5f },
                { All_types.fighting, 2f },
                { All_types.fire, 2f },
                { All_types.rock, 2f },
                { All_types.steel, 2f},
            };

            Dictionary<All_types, float> dragon_defense = new Dictionary<All_types, float>()
            {
                { All_types.electric, 0.5f },
                { All_types.fire, 0.5f },
                { All_types.grass, 0.5f },
                { All_types.water, 0.5f },
                { All_types.dragon, 2f },
                { All_types.ice, 0.5f },
                { All_types.fairy, 0.5f }
            };

            Dictionary<All_types, float> dark_defense = new Dictionary<All_types, float>()
            {
                { All_types.dark, 0.5f },
                { All_types.ghost, 0.5f },
                { All_types.bug, 2f},
                { All_types.fairy, 2f},
                { All_types.fighting, 2f},
                { All_types.psychic, 0f}
            };

            Dictionary<All_types, float> fairy_defense = new Dictionary<All_types, float>()
            {
                { All_types.dark, 0.5f },
                { All_types.bug, 0.5f },
                { All_types.fighting, 0.5f },
                { All_types.poison, 2f},
                { All_types.steel, 2f},
                { All_types.dragon, 0f}
            };

            Dictionary<All_types, float> normal_defense = new Dictionary<All_types, float>()
            {
                { All_types.fighting, 2f},
                { All_types.ghost, 0f}
            };

            Dictionary<All_types, float> fighting_defense = new Dictionary<All_types, float>()
            {
                { All_types.dark, 0.5f },
                { All_types.bug, 0.5f },
                { All_types.rock, 0.5f },
                { All_types.fairy, 2f},
                { All_types.flying, 2f},
                { All_types.psychic, 0.5f},
            };

            Dictionary<All_types, float> flying_defense = new Dictionary<All_types, float>()
            {
                { All_types.bug, 0.5f },
                { All_types.fighting, 0.5f },
                { All_types.grass, 0.5f },
                { All_types.electric, 2f},
                { All_types.ice, 2f},
                { All_types.rock, 2f},
                { All_types.ground, 0f}
            };

            Dictionary<All_types, float> poison_defense = new Dictionary<All_types, float>()
            {
                { All_types.fighting, 0.5f },
                { All_types.bug, 0.5f },
                { All_types.poison, 0.5f},
                { All_types.grass, 0.5f},
                { All_types.fairy, 0.5f},
                { All_types.ground, 2f},
                { All_types.psychic, 2f}

            };

            Dictionary<All_types, float> ground_defense = new Dictionary<All_types, float>()
            {
                { All_types.rock, 0.5f },
                { All_types.poison, 0.5f },
                { All_types.grass, 2f},
                { All_types.ice, 2f},
                { All_types.water, 2f},
                { All_types.electric, 0f}
            };

            Dictionary<All_types, float> rock_defense = new Dictionary<All_types, float>()
            {
                { All_types.normal, 0.5f },
                { All_types.fire, 0.5f },
                { All_types.flying, 0.5f },
                { All_types.poison, 0.5f },
                { All_types.fighting, 2f},
                { All_types.ground, 2f},
                { All_types.steel, 2f},
                { All_types.grass, 2f},
                { All_types.water, 2f}

            };

            Dictionary<All_types, float> bug_defense = new Dictionary<All_types, float>()
            {
                { All_types.fighting, 0.5f },
                { All_types.grass, 0.5f },
                { All_types.ground, 0.5f },
                { All_types.poison, 2f },
                { All_types.fire, 2f },
                { All_types.rock, 2f },
                { All_types.flying, 2f },
            };

            Dictionary<All_types, float> ghost_defense = new Dictionary<All_types, float>()
            {
                { All_types.bug, 0.5f },
                { All_types.poison, 0.5f },
                { All_types.dark, 2f},
                { All_types.ghost, 2f},
                { All_types.normal, 0f},
                { All_types.fighting, 0f}
            };

            Dictionary<All_types, float> steel_defense = new Dictionary<All_types, float>()
            {
                { All_types.bug, 0.5f },
                { All_types.dragon, 0.5f },
                { All_types.fairy, 0.5f },
                { All_types.flying, 0.5f },
                { All_types.ice, 0.5f },
                { All_types.normal, 0.5f },
                { All_types.psychic, 0.5f},
                { All_types.rock, 0.5f},
                { All_types.steel, 0.5f },
                { All_types.fighting, 2f },
                { All_types.fire, 2f },
                { All_types.ground, 2f },
                { All_types.poison, 0f }
            };

            if (def_type1 == All_types.fire || def_type2 == All_types.fire)
            {
                if (fire_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * fire_defense[attack_type];
                }
            }
            if (def_type1 == All_types.water || def_type2 == All_types.water)
            {
                if (water_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * water_defense[attack_type];
                }
            }
            if (def_type1 == All_types.grass || def_type2 == All_types.grass)
            {
                if (grass_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * grass_defense[attack_type];
                }
            }
            if (def_type1 == All_types.electric || def_type2 == All_types.electric)
            {
                if (electric_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * electric_defense[attack_type];
                }
            }
            if (def_type1 == All_types.psychic || def_type2 == All_types.psychic)
            {
                if (psychic_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * psychic_defense[attack_type];
                }
            }
            if (def_type1 == All_types.ice || def_type2 == All_types.ice)
            {
                if (ice_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * ice_defense[attack_type];
                }
            }
            if (def_type1 == All_types.dragon || def_type2 == All_types.dragon)
            {
                if (dragon_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * dragon_defense[attack_type];
                }
            }
            if (def_type1 == All_types.dark || def_type2 == All_types.dark)
            {
                if (dark_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * dark_defense[attack_type];
                }
            }
            if (def_type1 == All_types.fairy || def_type2 == All_types.fairy)
            {
                if (fairy_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * fairy_defense[attack_type];
                }
            }
            if (def_type1 == All_types.normal || def_type2 == All_types.normal)
            {
                if (normal_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * normal_defense[attack_type];
                }
            }
            if (def_type1 == All_types.fighting || def_type2 == All_types.fighting)
            {
                if (fighting_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * fighting_defense[attack_type];
                }
            }
            if (def_type1 == All_types.flying || def_type2 == All_types.flying)
            {
                if (flying_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * flying_defense[attack_type];
                }
            }
            if (def_type1 == All_types.poison || def_type2 == All_types.poison)
            {
                if (poison_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * poison_defense[attack_type];
                }
            }
            if (def_type1 == All_types.ground || def_type2 == All_types.ground)
            {
                if (ground_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * ground_defense[attack_type];
                }
            }
            if (def_type1 == All_types.rock || def_type2 == All_types.rock)
            {
                if (rock_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * rock_defense[attack_type];
                }
            }
            if (def_type1 == All_types.bug || def_type2 == All_types.bug)
            {
                if (bug_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * bug_defense[attack_type];
                }
            }
            if (def_type1 == All_types.ghost || def_type2 == All_types.ghost)
            {
                if (ghost_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * ghost_defense[attack_type];
                }
            }
            if (def_type1 == All_types.steel || def_type2 == All_types.steel)
            {
                if (steel_defense.ContainsKey(attack_type))
                {
                    modifier_stage = modifier_stage * steel_defense[attack_type];
                }
            }

            if (self.undergoing_multi_move_tracker)
            {
            }
            else
            {
                if (modifier_stage > 1)
                {
                    messages.Add("It's super effective!");
                }
                else if (modifier_stage == 0)
                {
                    messages.Add("It had no effect!");
                }
                else if (modifier_stage < 1)
                {
                    messages.Add("It's not very effective...");
                }
            }

            return modifier_stage;
        }
        #endregion
    }
}
