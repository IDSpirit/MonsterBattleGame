using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Class_Library;
using MonsterBattle.Interfaces;

//using MonsterBattle;

namespace MonsterBattle
{
    public class Moveset_Database
    {
        public static Dictionary<string, Moves[]> Select_Moveset(string pokemonName) //Might remove the dictionary to make it easier, since we already know the pokemon's moves that we want through the parameter
        {
            Dictionary<string, Moves[]> pokemon_moveset = new Dictionary<string, Moves[]>();

            Moves tackle = new Moves("Tackle", 40, move_type: All_types.normal, accuracy: 100, pp_max: 40, attack_category: Stats_Types.ph_attack, contact: true); //Can set moveset to a list of object "Moves" which is a field of the Pokemon class?
            Moves fiery_dance = new Moves(move_name: "Fiery Dance", move_power: 80, move_type: All_types.fire, accuracy: 100, pp_max: 10, attack_category: Stats_Types.sp_attack,
                moves_with_self_stat_change: new List<IMoveWithStatChange>() { new MoveWithSelfStatChange(self_stat_change_type: Stats_Types.sp_attack, self_stat_chance: 0.5f, self_stat_level: 1) });
            Moves fly = new Moves("Fly", 90, move_type: All_types.flying, accuracy: 95, pp_max: 15, attack_category: Stats_Types.ph_attack, contact: true, multi_turns_number: 1);
            Moves flame_wheel = new Moves("Flame Wheel", 50, move_type: All_types.fire, accuracy: 100, pp_max: 20, attack_category: Stats_Types.ph_attack, contact: true,
                moves_with_self_stat_change: new List<IMoveWithStatChange>() { new MoveWithSelfStatChange(self_stat_level: 1, self_stat_chance: 1, self_stat_change_type: Stats_Types.speed) });
            Moves psychic = new Moves("Psychic", 90, All_types.psychic, 100, 10, Stats_Types.sp_attack);
            Moves steelWing = new Moves("Steel Wing", 70, All_types.steel, 90, 25, Stats_Types.ph_attack,
                moves_with_self_stat_change: new List<IMoveWithStatChange>(new List<IMoveWithStatChange>() {new MoveWithSelfStatChange(self_stat_level: 1, self_stat_chance: 0.1f, self_stat_change_type: Stats_Types.ph_defense)}));
            Moves megaPunch = new Moves("Mega Punch", 80, move_type: All_types.normal, accuracy: 85, pp_max: 20, attack_category: Stats_Types.ph_attack, contact: true); //Can set moveset to a list of object "Moves" which is a field of the Pokemon class?
            Moves thunderbolt = new Moves("Thunderbolt", 90, All_types.electric, 100, 15, Stats_Types.sp_attack);
            Moves flamethrower = new Moves("Flamethrower", 90, All_types.fire, 100, 15, Stats_Types.sp_attack);
            Moves playRough = new Moves("Play Rough", 90, All_types.fairy, 90, 10, Stats_Types.ph_attack, contact: true, 
                moves_with_other_stat_change: new List<IMoveWithStatChange>() {new MoveWithOtherStatChange(Stats_Types.ph_attack, 0.1f, 1)});
            Moves agility = new Moves("Agility", All_types.psychic, 100, 30, moves_with_self_stat_change: new List<IMoveWithStatChange>() { new MoveWithSelfStatChange(Stats_Types.speed, 1f, 2) });
            Moves quickAttack = new Moves("Quick Attack", 40, move_type: All_types.normal, accuracy: 100, pp_max: 40, attack_category: Stats_Types.ph_attack, contact: true, priority: 1); //Can set moveset to a list of object "Moves" which is a field of the Pokemon class?
            Moves megaKick = new Moves(move_name: "Mega Kick", move_power: 120, move_type: All_types.normal, accuracy: 75, pp_max: 5, Stats_Types.ph_attack, contact: true);
            Moves swordsDance = new Moves(move_name: "Swords Dance", move_type: All_types.fighting, 100, 20, moves_with_self_stat_change: new List<IMoveWithStatChange>() { new MoveWithSelfStatChange(self_stat_change_type: Stats_Types.ph_attack, 100, 2) });
            Moves drainPunch = new Moves(move_name: "Drain Punch", move_power: 75, move_type: All_types.fighting, accuracy: 100, pp_max: 10, attack_category: Stats_Types.ph_attack, contact: true, percentageDrain: 50);
            Moves nightSlash = new Moves("Night Slash", 70, All_types.dark, 100, 15, Stats_Types.ph_attack, contact: true, critDenominator: 8);
            Moves scaryFace = new Moves(move_name: "Scary Face", All_types.normal, 100, 10, moves_with_other_stat_change: new List<IMoveWithStatChange>() { new MoveWithOtherStatChange(Stats_Types.speed, 1f, -2) });
            Moves lowSweep = new Moves("Low Sweep", 65, All_types.fighting, 100, 20, Stats_Types.ph_attack, contact: true, moves_with_other_stat_change: new List<IMoveWithStatChange>() { new MoveWithOtherStatChange(Stats_Types.speed, 1f, -1) });
            Moves ironDefense = new Moves("Iron Defense", All_types.steel, 100, 15, moves_with_self_stat_change: new List<IMoveWithStatChange> { new MoveWithOtherStatChange(Stats_Types.ph_defense, 100f, 2) });
            Moves leechLife = new Moves("Leech Life", 80, All_types.bug, 100, 10, Stats_Types.ph_attack, contact: true, percentageDrain: 50);

            switch (pokemonName)
            {
                case "Volcarona":
                    pokemon_moveset.Add(pokemonName, new Moves[] { fiery_dance, fly, flame_wheel, psychic, leechLife });
                    break;
                case "Skarmory":
                    pokemon_moveset.Add(pokemonName, new Moves[] { tackle, steelWing, fly, agility, swordsDance, ironDefense, nightSlash });
                    break;
                case "Buneary":
                    pokemon_moveset.Add(pokemonName, new Moves[] { quickAttack, megaPunch, agility, megaKick, drainPunch });
                    break;
                case "Wigglytuff":
                    pokemon_moveset.Add(pokemonName, new Moves[] { tackle, psychic, thunderbolt, flamethrower, playRough });
                    break;
                case "Bisharp":
                    pokemon_moveset.Add(pokemonName, new Moves[] { swordsDance, nightSlash, scaryFace, ironDefense, lowSweep });
                    break;
                default:
                    break;

            }

            Dictionary<string, Array> list_of_pokemon = new Dictionary<string, Array>();

            return pokemon_moveset; //I think it might be better to only return the movesets of each of the pokemon that are playing which would decrease the number of objects made (The returned movesets would depend on the pokemon that was playing)
        }
    }
}
