using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterBattle.Class_Library;

namespace MonsterBattle
{
    public class Pokemon_Database
    {
        public static Pokemon Select_Pokemon(string poke_choice)
        { //I could make a dictionary with the pokemon name as a key, and then the pokemon object as the value
            switch (poke_choice)
            {
                case "Volcarona":
                    return new Pokemon(name: "Volcarona", hp: 280, ph_attack: 112, ph_defense: 121, sp_attack: 247, sp_defense: 193, speed: 184, "When volcanic ash darkened the atmosphere, it is said that Volcarona’s fire provided a replacement for the sun.", All_types.bug, All_types.fire); //I'll replace the argument with a call to the pokemon_list for the chosen pokemon
                case "Skarmory":
                    return new Pokemon(name: "Skarmory", hp: 240, ph_attack: 148, ph_defense: 256, sp_attack: 76, sp_defense: 130, speed: 130, "Its sturdy wings look heavy, but they are actually hollow and light, allowing it to fly freely in the sky.", All_types.flying, All_types.steel); //240
                case "Buneary":
                    return new Pokemon(name: "Buneary", hp: 220, ph_attack: 123, ph_defense: 83, sp_attack: 83, sp_defense: 105, speed: 157, "It slams foes by sharply uncoiling its rolled ears. It stings enough to make a grown-up cry in pain.", All_types.normal);
                case "Wigglytuff":
                    return new Pokemon(name: "Wigglytuff", hp: 392, ph_attack: 130, ph_defense: 85, sp_attack: 157, sp_defense: 94, speed: 85, "The body is soft and rubbery. When angered, it will suck in air and inflate itself to an enormous size.", All_types.normal, All_types.fairy);
                case "Bisharp":
                    return new Pokemon(name: "Bisharp", hp: 240, ph_attack: 229, ph_defense: 184, sp_attack: 112, sp_defense: 139, speed: 130, pokedex: "It’s accompanied by a large retinue of Pawniard. Bisharp keeps a keen eye on its minions, ensuring none of them even think of double-crossing it.", All_types.dark, All_types.steel);
                default:
                    return null;
            }

        }
        
    }
}
