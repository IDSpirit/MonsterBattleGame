using System;
using System.Collections.Generic;
using MonsterBattle.Class_Library;
using MonsterBattleUI.Models;


namespace MonsterBattle
{
    public class Main_File
    {
        [STAThread]
        public static void Main() //This will be used for initialising the player and enemy pokemon objects for when there's a rematch
        {
            //Window qWindow = new Window();
            //qWindow.Title = "WPF in Console";
            //qWindow.Width = 400;
            //qWindow.Height = 300;
            //qWindow.ShowDialog();

            //var app = new App();
            //app.InitializeComponent();
            //app.Run();
        }

        //public static Moves storedEnemyAttack { get; set; }

        public static OutcomeModel Battling(ref Pokemon self, ref Pokemon other, Moves selected_move, Random randCrit)
        {

            var messages = new List<string>() {$"{self.isPlayerPokemon} {self.name} used {selected_move.move_name}..."};
            float modifier_stage = 1; //Maybe turn this to a field for either the Pokemon object or Move object

            if (self.undergoing_multi_move_tracker)
            {
                selected_move = self.undergoing_multi_move_value;
            }

            if (selected_move.multi_turns_number != 0)
            {
                self.undergoing_multi_move_tracker = true;
                self.undergoing_multi_move_value = selected_move;
            }

            //if (selected_move.attack_category == Stats_Types.ph_attack)
            //{
            //    modifier_stage = modifier_stage * self.stat_modifier_object.ph_attack_stat_change;
            //}
            //else if (selected_move.attack_category == Stats_Types.sp_attack)
            //{
            //    modifier_stage = modifier_stage * self.stat_modifier_object.sp_attack_stat_change;
            //}

            if (other.undergoing_multi_move_tracker == false)
                other.hp = Pokemon.Attacks(ref messages, selected_move, self, other, modifier_stage, randCrit);
            else if (other.undergoing_multi_move_tracker == true && selected_move.canHitFlying == true)
                other.hp = Pokemon.Attacks(ref messages, selected_move, self, other, modifier_stage, randCrit);
            else if (other.undergoing_multi_move_tracker == true && selected_move.move_power == 0 && selected_move.fixed_dmg == 0)
                other.hp = Pokemon.Attacks(ref messages, selected_move, self, other, modifier_stage, randCrit);
            else
                messages.Add($"{self.isPlayerPokemon} {self.name}'s {selected_move.move_name} can't hit the opponent as they're still in the air!");


            if (selected_move.multi_turns_tracker < selected_move.multi_turns_number) //Redo the "fly" and "dig" move implementation, however it does work fine rn
            {
                selected_move.multi_turns_tracker = selected_move.multi_turns_number + 1;
                self.undergoing_multi_move_tracker = false;
                self.undergoing_multi_move_value = null;
                messages.Add($"{self.isPlayerPokemon} {self.name}'s move {selected_move.move_name} has landed!");
                Pokemon.Move_Type_Interaction(ref messages, ref modifier_stage, self, other, selected_move);
            }

            if (other.hp == 0)
            {
                messages.Add("You won!");
            }
            else if (self.hp == 0)
            {
                messages.Add("You lost...");
            }
            else if (self.hp == 0 & other.hp == 0)
            {
                messages.Add("...It's a draw!");
            }

            //storedEnemyAttack = selected_move;
            return new OutcomeModel(false, messages);
        }
    }
}
