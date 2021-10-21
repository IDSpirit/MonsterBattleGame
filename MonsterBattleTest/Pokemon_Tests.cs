using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterBattle.Class_Library;
using MonsterBattle;
using Moq;

namespace MonsterBattleTest
{
    [TestClass]
    public class Pokemon_Tests
    {
        [TestMethod]
        public void Given_ph_attack_move_Then_will_set_ph_defense_for_the_move()
        {
            //Arrange
            var self = new Pokemon(name: "Volcarona", hp: 280, ph_attack: 112, ph_defense: 121, sp_attack: 247, sp_defense: 193, speed: 184, "When volcanic ash darkened the atmosphere, it is said that Volcarona’s fire provided a replacement for the sun.", All_types.bug, All_types.fire); //I'll replace the argument with a call to the pokemon_list for the chosen pokemon
            var other = new Pokemon(name: "Skarmory", hp: 1000, ph_attack: 148, ph_defense: 256, sp_attack: 76, sp_defense: 130, speed: 130, "Its sturdy wings look heavy, but they are actually hollow and light, allowing it to fly freely in the sky.", All_types.flying, All_types.steel); //240
            var pokemon_moveset = Moveset_Database.Select_Moveset("Volcarona");
            pokemon_moveset.TryGetValue(self.name, out var user_moves);
            var user_chosen_move = user_moves[1];

            int? attack = null;
            int? defense = null;
            Stats_Types category = Stats_Types.unknown;

            var expected = other.ph_defense;

            //Assert
            Pokemon.Sort_Attack_And_Defense(user_chosen_move, self, other, ref attack, ref defense, ref category);
            var actual = defense;

            //Act
            Assert.AreEqual(expected, actual);
        }
    }
}
