using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using MonsterBattle.Class_Library;
using MonsterBattle;
using MonsterBattle.Interfaces;
using MonsterBattleUI.Models;

namespace MonsterBattle
{
    public class Pokemon
    {
        #region Properties And Fields
        public string name { get; set; }
        private int _hp;
        public int hp
        {
            set
            {
                _hp = value;

                if (_hp <= 0)
                {
                    _hp = 0;
                }
                if (_hp >= maxHP)
                {
                    _hp = maxHP;
                }

                HpBar = new HPBar(hp, maxHP);
            }

            get
            {
                return _hp;
            }

        }
        public int maxHP { get; set; }
        public int ph_attack { get; set; } //I could replace a bunch of these with objects that hold several properties
        public int sp_attack { get; set; }
        public int ph_defense { get; set; }
        public int sp_defense { get; set; }
        public int speed { get; set; }
        public Stats stat_modifier_object { get; set; }
        public All_types type_1 { get; set; }
        public All_types type_2 { get; set; }
        public HPBar HpBar { get; set; }
        public Moves undergoing_multi_move_value = null;
        public bool undergoing_multi_move_tracker = false;
        public string front_sprite { get; set; }
        public string back_sprite { get; set; }
        public string isPlayerPokemon { get; set; }
        public string pokedex { get; set; }

        #endregion

        #region Contructor
        public Pokemon(string name, int hp, int ph_attack, int ph_defense, int sp_attack, int sp_defense, int speed, string pokedex, All_types type_1, All_types type_2 = All_types.unknown)
        {
            this.name = name;
            maxHP = hp;
            this.hp = hp;
            this.ph_attack = ph_attack;
            this.ph_defense = ph_defense;
            this.sp_attack = sp_attack;
            this.sp_defense = sp_defense;
            this.speed = speed;
            this.stat_modifier_object = stat_modifier_object;
            stat_modifier_object = new Stats();
            this.type_1 = type_1;
            if (type_2 == All_types.unknown)
            {
                this.type_2 = type_1;
            }
            else
            {
                this.type_2 = type_2;
            }
            this.pokedex = pokedex;
            front_sprite = $"/Sprites/{name.ToLower()}_front_sprite.png";
            back_sprite = $"/Sprites/{name.ToLower()}_back_sprite.png";
        }
        #endregion

        #region Main Method
        public static int Attacks(ref List<string> messages, Moves chosen_move, Pokemon self, Pokemon other, float modifier, Random randCrit)
        {
            //float? damage = null;
            int? attack = null;
            int? defense = null;
            Random rand = new Random();
            float rand_range = rand.Next(85, 101);

            Stats_Types category = Stats_Types.unknown;

            if (Multi_Turn_Tracker(ref messages, chosen_move, self, other, out var otherHp))
                return other.hp;

            if (!Accuracy_Success(chosen_move))
            {
                messages.Add($"{self.isPlayerPokemon} {self.name}'s {chosen_move.move_name} missed!");
                return other.hp;
            }

            if (chosen_move.move_power != 0)
                Move_Type_Interaction(ref messages, ref modifier, self, other, chosen_move);

            Sort_Attack_And_Defense(chosen_move, self, other, ref attack, ref defense, ref category);

            Crit_Chance(ref modifier, ref messages, attack, defense, randCrit, chosen_move);

            CheckForSTAB(ref modifier, self, chosen_move);

            Damage_Calc(chosen_move, ref self, other, modifier, attack, defense, rand_range);

            Check_For_Stat_Changes(ref messages, chosen_move, self, other, rand);

            return other.hp;
        }

        #endregion

        #region Supporting Methods

        private static void CheckForSTAB(ref float modifier, Pokemon self, Moves chosenMove)
        {
            if (chosenMove.move_type == self.type_1 || chosenMove.move_type == self.type_2)
                modifier = modifier * 1.5f;
        }

        internal static bool Accuracy_Success(Moves chosen_move)
        {
            if (chosen_move.accuracy < 100)
            {
                Random r = new Random();
                if (r.Next(1,101) <= chosen_move.accuracy)
                    return true;
                return false;
            }

            return true;
        }
        internal static void Move_Type_Interaction(ref List<string> messages, ref float modifier, Pokemon self, Pokemon other, Moves chosenMove) =>
            modifier = modifier * Moves.Move_Effectiveness(ref messages, self, other, chosenMove.move_type, other.type_1, other.type_2);

        internal static void Check_For_Stat_Changes(ref List<string> messages, Moves chosen_move, Pokemon self, Pokemon other, Random rand) //Defo need to write tests for this method (and likely for other ones too
        {
            if (chosen_move.self_stat_change == true)
            {
                //Add a foreach at the start to account for multiple stats changing, could create an object of stat, chance, and amount of change, with a collection of these stored in a list
                float rand_num = rand.Next(1, 100);
                Looping_Over_Stat_Changes(messages, self, rand_num, movesWithSelfStatChange: chosen_move.moves_with_self_stat_change);

                //Looping_Over_Stat_Changes(messages, chosen_move, self, rand_num, movesWithOtherStatChange: chosen_move.moves_with_other_stat_change);
            }

            if (chosen_move.other_stat_change == true)
            {
                float rand_num = rand.Next(1, 100);
                Looping_Over_Stat_Changes(messages, other, rand_num, movesWithOtherStatChange: chosen_move.moves_with_other_stat_change);
            }

            void Looping_Over_Stat_Changes(List<string> list, Pokemon pokemon, float random, List<IMoveWithStatChange> movesWithSelfStatChange = null, List<IMoveWithStatChange> movesWithOtherStatChange = null)
            {
                List<IMoveWithStatChange> movesWithStatChange = new List<IMoveWithStatChange>();
                string statChange;
                if (movesWithSelfStatChange != null && movesWithOtherStatChange == null)
                {
                    movesWithStatChange = movesWithSelfStatChange;
                    statChange = "self stat";
                }
                else if (movesWithSelfStatChange == null && movesWithOtherStatChange != null)
                {
                    movesWithStatChange = movesWithOtherStatChange;
                    statChange = "other stat";
                }
                else
                    return;

                for (int i = 0; i < movesWithStatChange.Count; i++)
                {
                    random = random / 100;
                    if ((random) <= movesWithStatChange[i].stat_chance)
                    { 
                        string stat_change_name = movesWithStatChange[i].stat_change_type.GetType()
                            .GetEnumName(movesWithStatChange[i].stat_change_type);
                        int previousStatLevel = 0;
                        int currentStatLevel = 0;

                        switch (stat_change_name)
                        {
                            case "ph_attack":
                                previousStatLevel = pokemon.stat_modifier_object.ph_attack_modifier;
                                pokemon.stat_modifier_object.stat_modifier_method(movesWithStatChange[i].stat_level,
                                    Stats_Types.ph_attack);
                                currentStatLevel = pokemon.stat_modifier_object.ph_attack_modifier;
                                stat_change_name = "Physical Attack";
                                break;
                            case "ph_defense":
                                previousStatLevel = pokemon.stat_modifier_object.ph_defense_modifier;
                                pokemon.stat_modifier_object.stat_modifier_method(movesWithStatChange[i].stat_level,
                                    Stats_Types.ph_defense);
                                currentStatLevel = pokemon.stat_modifier_object.ph_defense_modifier;
                                stat_change_name = "Physical Defense";
                                break;
                            case "sp_attack":
                                previousStatLevel = pokemon.stat_modifier_object.sp_attack_modifier;
                                pokemon.stat_modifier_object.stat_modifier_method(movesWithStatChange[i].stat_level,
                                    Stats_Types.sp_attack);
                                currentStatLevel = pokemon.stat_modifier_object.sp_attack_modifier;
                                stat_change_name = "Special Attack";
                                break;
                            case "sp_defense":
                                previousStatLevel = pokemon.stat_modifier_object.sp_defense_modifier;
                                pokemon.stat_modifier_object.stat_modifier_method(movesWithStatChange[i].stat_level,
                                    Stats_Types.sp_defense);
                                currentStatLevel = pokemon.stat_modifier_object.sp_defense_modifier;
                                stat_change_name = "Special Defense";
                                break;
                            case "speed":
                                previousStatLevel = pokemon.stat_modifier_object.speed_modifier;
                                pokemon.stat_modifier_object.stat_modifier_method(movesWithStatChange[i].stat_level,
                                    Stats_Types.speed);
                                currentStatLevel = pokemon.stat_modifier_object.speed_modifier;
                                stat_change_name = "Speed";
                                break;
                        }

                        var plural = "";
                        string statChangeWord = "";
                        string isPlayersPokemon = "";

                        if (Math.Abs(movesWithStatChange[i].stat_level) > 1)
                            plural = "s";

                        if (statChange == "self stat")
                            statChangeWord = "increased";
                        else if (statChange == "other stat")
                            statChangeWord = "decreased";
                        if (movesWithOtherStatChange == null)
                            isPlayersPokemon = self.isPlayerPokemon;
                        else if (movesWithSelfStatChange == null)
                            isPlayersPokemon = other.isPlayerPokemon;

                        list.Add(
                            $"{isPlayersPokemon} {pokemon.name}'s {stat_change_name} has {statChangeWord} by {Math.Abs(movesWithStatChange[i].stat_level)} stage{plural}!");
                        if (currentStatLevel == 6 && previousStatLevel == 6)
                        {
                            list.Add($"{self.isPlayerPokemon} {pokemon.name}'s {stat_change_name} can't raise any higher!");
                        }
                        else if (currentStatLevel == -6 && previousStatLevel == -6)
                        {
                            list.Add($"{self.isPlayerPokemon} {pokemon.name}'s {stat_change_name} can't fall any lower!");
                        }
                    }
                }
            }
        }

        internal static void Damage_Calc(Moves chosen_move, ref Pokemon self, Pokemon other, float modifier, int? attack, int? defense,
            float rand_range)
        {
            float? damage = null; /////////////
            if (attack == null || defense == null)
            {
                return;
            }
            else
            {
                if (chosen_move.defense_category == Stats_Types.fixed_dmg)
                {
                    damage = chosen_move
                        .fixed_dmg; //Will replace this with the fixed damage field from the "Moves" class
                }
                else
                {
                    damage =
                        ((((((2 * 50) / 5) + 2) * chosen_move.move_power * ( (float)attack / (float)defense)) / 50) +
                         2) *
                        modifier * (rand_range / 100);
                }

                Drain_Health(ref self, chosen_move, damage);

                other.hp = (int) Math.Round(other.hp - (float) damage);
            }
        }

        internal static void Drain_Health(ref Pokemon self, Moves selectedMove, float? damage)
        {
            if (selectedMove.percentageDrain != 0)
                self.hp = self.hp + (int)Math.Round((float)damage * ((float)selectedMove.percentageDrain / 100));
        }

        internal static bool Multi_Turn_Tracker(ref List<string> messages, Moves chosen_move, Pokemon self, Pokemon other, out int otherHp)
        {
            if (chosen_move.multi_turns_tracker == (chosen_move.multi_turns_number + 1))
            {
                messages.Add($"{self.isPlayerPokemon} {self.name} flew up to the air!");
                chosen_move.multi_turns_tracker = chosen_move.multi_turns_tracker - 1;
                {
                    otherHp = other.hp;
                    return true;
                }
            }

            //if (chosen_move.multi_turns_tracker == (chosen_move.multi_turns_number + 1))
            //{
            //    messages.Add($"{self.isPlayerPokemon} {self.name} is in the air and can't attack!");
            //    chosen_move.multi_turns_tracker = chosen_move.multi_turns_tracker - 1;
            //    {
            //        otherHp = other.hp;
            //        return true;
            //    }
            //}

            if (chosen_move.multi_turns_tracker == (chosen_move.multi_turns_number) && (self.undergoing_multi_move_tracker))
            {
                chosen_move.multi_turns_tracker = chosen_move.multi_turns_tracker - 1;
                //return other.hp;
            }

            otherHp = other.hp;
            return false;
        }

        internal static void Crit_Chance(ref float modifier, ref List<string> messages, int? attack, int? defense, Random randCrit, Moves chosenMove)
        {
            if (attack == null || defense == null)
            {
                return;
            }
            else
            {
                if (randCrit.Next(1, chosenMove.critDenominator) == 6)
                {
                    modifier = modifier * 2;
                    messages.Add("It's a critical hit!");
                }
            }
        }

        public static void Sort_Attack_And_Defense(Moves chosen_move, Pokemon self, Pokemon other, ref int? attack, ref int? defense, ref Stats_Types category)
        {
            if (chosen_move.attack_category == Stats_Types.ph_attack)
            {
                //attack = (int?)Math.Round((float)self.ph_attack * (float)self.stat_modifier_object.ph_attack_stat_change);
                attack = (int?)Math.Round(self.ph_attack * self.stat_modifier_object.ph_attack_stat_change);
                defense = (int?)Math.Round(other.ph_defense * other.stat_modifier_object.ph_defense_stat_change);
                category = Stats_Types.ph_defense;
            }
            else if (chosen_move.attack_category == Stats_Types.sp_attack)
            {
                attack = (int?)Math.Round(self.sp_attack * self.stat_modifier_object.sp_attack_stat_change);
                defense = (int?)Math.Round(other.sp_defense * other.stat_modifier_object.sp_defense_stat_change);
                category = Stats_Types.sp_defense;
            }

            if (chosen_move.defense_category != chosen_move.attack_category)
            {
                if (category == Stats_Types.ph_defense)
                {
                    defense = (int?)Math.Round(other.sp_defense * other.stat_modifier_object.sp_defense_stat_change);
                }
                else if (category == Stats_Types.sp_defense)
                {
                    defense = (int?)Math.Round(other.ph_defense * other.stat_modifier_object.ph_defense_stat_change);
                }
            }
        }

        #endregion
    }
}
