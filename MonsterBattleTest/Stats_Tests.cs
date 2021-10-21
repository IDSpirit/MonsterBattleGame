using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterBattle.Class_Library;
using System.Reflection;
using Moq;

namespace MonsterBattleTest
{
    [TestClass]
    public class Stats_Tests
    {
        [TestMethod]
        public void stat_modifier_method_test()
        {
            //Arrange
            var expected = 2f;
            var stats = new Stats();

            //Act
            stats.stat_modifier_method(2, Stats_Types.ph_attack);
            var actual = stats.ph_attack_stat_change;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void stat_modifier_method_test2()
        {
            //Arrange
            var expected = 0.5f;
            var stats = new Stats();

            //Act
            stats.stat_modifier_method(-2, Stats_Types.ph_defense);
            var actual = stats.ph_defense_stat_change;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void stat_modifier_method_test3()
        {
            //Arrange
            var stats = new Stats();

            //Act
            stats.stat_modifier_method(2, Stats_Types.sp_attack);
            stats.stat_modifier_method(3, Stats_Types.sp_attack);
            stats.stat_modifier_method(3, Stats_Types.sp_defense);
            var actual = stats.sp_attack_stat_change;

            //Assert
            Assert.AreEqual(3.5, stats.sp_attack_stat_change);
            Assert.AreEqual(2.5, stats.sp_defense_stat_change);
        }

        [TestMethod]
        public void stat_modifier_method_test4()
        {
            //Arrange
            var stats = new Stats();

            //Act
            stats.stat_modifier_method(2, Stats_Types.speed);
            stats.stat_modifier_method(9, Stats_Types.speed);
            var actual = stats.speed_stat_change;

            //Assert
            Assert.AreEqual(4, stats.speed_stat_change);
        }
    }
}
