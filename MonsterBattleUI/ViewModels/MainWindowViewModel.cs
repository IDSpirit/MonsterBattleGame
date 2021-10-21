using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using MonsterBattle;
using MonsterBattle.Class_Library;
using MonsterBattleUI.Annotations;
using MonsterBattleUI;
using MonsterBattleUI.Models;
using MonsterBattleUI.ViewModels;

namespace MonsterBattleUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); //This updates the UI control for the property that we input when calling this event
            }

        }

        public void OnButtonClickHandlerMain(object o) //Add status effects?
        {
            var message = "";
            if (selected_move.move_name == "Select your move")
            {
                message = "You haven't selected a move yet!";
                return;
            }
            else
                message = $"You've selected {selected_move.move_name}";

            MessageBox.Show(message);

            OutcomeModel outcome = null;
            OutcomeModel enemyOutcome = null;
            OutcomeModel fasterOutcome = null;
            OutcomeModel slowerOutcome = null;
            Random playerRandCrit = new Random(Guid.NewGuid().GetHashCode());
            Random enemyRandCrit = new Random(Guid.NewGuid().GetHashCode());

            var enemySelectedMove = enemy_list_of_moves[new Random().Next(0, enemy_list_of_moves.Count)];

            if (selected_move.priority > enemySelectedMove.priority)
            {
                outcome =
                    Main_File.Battling(ref _player, ref _enemy, selected_move, playerRandCrit); //I've had to use the backing fields here bc "ref" and "out" can only be used for fields and not properties

                if (!hasBattleEnded(outcome.messages))
                {
                    enemyOutcome = Main_File.Battling(ref _enemy, ref _player, enemySelectedMove, enemyRandCrit);
                }

                fasterOutcome = outcome;
                slowerOutcome = enemyOutcome;
            }
            else if (selected_move.priority < enemySelectedMove.priority)
            {
                enemyOutcome = Main_File.Battling(ref _enemy, ref _player, enemySelectedMove, enemyRandCrit);

                if (!hasBattleEnded(enemyOutcome.messages))
                {
                    outcome =
                        Main_File.Battling(ref _player, ref _enemy, selected_move, playerRandCrit);
                }

                fasterOutcome = enemyOutcome;
                slowerOutcome = outcome;
            }
            else
            {
                if (player.speed * player.stat_modifier_object.speed_stat_change > enemy.speed * enemy.stat_modifier_object.speed_stat_change)
                {
                    outcome =
                        Main_File.Battling(ref _player, ref _enemy,
                            selected_move, playerRandCrit); //I've had to use the backing fields here bc "ref" and "out" can only be used for fields and not properties

                    if (!hasBattleEnded(outcome.messages))
                    {
                        enemyOutcome = Main_File.Battling(ref _enemy, ref _player, enemySelectedMove, enemyRandCrit);
                    }

                    fasterOutcome = outcome;
                    slowerOutcome = enemyOutcome;
                }
                else if (enemy.speed * enemy.stat_modifier_object.speed_stat_change > player.speed * player.stat_modifier_object.speed_stat_change)
                {
                    enemyOutcome = Main_File.Battling(ref _enemy, ref _player, enemySelectedMove, enemyRandCrit);

                    if (!hasBattleEnded(enemyOutcome.messages))
                    {
                        outcome =
                            Main_File.Battling(ref _player, ref _enemy, selected_move, playerRandCrit);
                    }

                    fasterOutcome = enemyOutcome;
                    slowerOutcome = outcome;
                }
                else
                {
                    Random rand = new Random();
                    if (rand.Next(0, 2) == 0)
                    {
                        outcome =
                            Main_File.Battling(ref _player, ref _enemy, selected_move, playerRandCrit);

                        if (!hasBattleEnded(outcome.messages))
                        {
                            enemyOutcome = Main_File.Battling(ref _enemy, ref _player, enemySelectedMove, enemyRandCrit);
                        }

                        fasterOutcome = outcome;
                        slowerOutcome = enemyOutcome;
                    }
                    else
                    {
                        enemyOutcome = Main_File.Battling(ref _enemy, ref _player, enemySelectedMove, enemyRandCrit);

                        if (!hasBattleEnded(enemyOutcome.messages))
                        {
                            outcome =
                                Main_File.Battling(ref _player, ref _enemy, selected_move, playerRandCrit);
                        }

                        fasterOutcome = enemyOutcome;
                        slowerOutcome = outcome;
                    }
                }
            }
            if (fasterOutcome != null && slowerOutcome != null)
            {
                if (fasterOutcome.messages.Contains("It's a critical hit!") && slowerOutcome.messages.Contains("It's a critical hit!"))
                    return;
            }

            turnNum = turnNum + 1;
            player = player; //This is here bc otherwise, OnNotifyPropertyChanged event isn't triggered by the "player" bc only "_player" is changing
            enemy = enemy;
            if (messages == "Good luck with your match!")
                messages = "";
            
            ListMessage = FinalMessageList(fasterOutcome, slowerOutcome);

            //ListMessage = finalMessageList;

            if (player.hp == 0 || enemy.hp == 0)
            {
                if (player.hp <= 0)
                    MessageBox.Show(
                        $"You've lost... That {enemy.name} absolutely destroyed your {player.name}, you really gonna just let that happen?");
                else if (enemy.hp <= 0)
                    MessageBox.Show($"You won! Nice, your {player.name} really showed that {enemy.name} what for :)");

                var windows = Application.Current.Windows;
                foreach (Window window in windows)
                {
                    if (window.Title == "Pokemon Battle")
                    {
                        window.Close();
                        //windows[3].Close();
                    }
                }
            }
        }

        private static List<string> FinalMessageList(OutcomeModel fasterOutcome, OutcomeModel slowerOutcome)
        {
            if (fasterOutcome.messages.Contains("It's a critical hit!") && slowerOutcome.messages.Contains("It's a critical hit!"))
                return null;

            var finalMessageList = fasterOutcome.messages;
            if (slowerOutcome != null)
            {
                finalMessageList.AddRange(slowerOutcome.messages);
            }

            finalMessageList.Add("\n-----------------------------\n");
            return finalMessageList;
        }

        private Pokemon _player;
        public Pokemon player {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                OnPropertyChanged("player");
            }
        }

        private Pokemon _enemy;

        public Pokemon enemy
        {
            get
            {
                return _enemy;
            }
            set
            {
                _enemy = value;
                OnPropertyChanged("enemy");
            }
        }

        public ObservableCollection<Moves> list_of_moves { get; set; }

        private Moves _selected_move;

        public Moves selected_move
        {
            get
            {
                if (_selected_move == null)
                {
                    return new Moves("Select your move", 0, All_types.unknown, 0, 0, Stats_Types.unknown);
                }

                return _selected_move;
            }
            set
            {
                _selected_move = value;

                OnPropertyChanged("selected_move");
            }
        }

        private string _messages = "Good luck with your match!";

        public string messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;

                OnPropertyChanged("messages");
            }
        }
        
        private List<string> _ListMessage;
        public List<string> ListMessage
        {
            get
            {
                return _ListMessage;
            }
            set
            {
                var stringConverter = new ListToStringConverter();
                value.Add(messages);
                var convertedMessage = stringConverter.Convert(value, typeof(string), turnNum);
                messages = (string)convertedMessage;
                
            }
        }

        private int _turnNum = 0;

        public int turnNum
        {
            get { return _turnNum; }
            set { _turnNum = value; }
        }

        private string _frontSprite;
        public string frontSprite
        {
            get
            {
                return _frontSprite;
            }
            set
            {
                _frontSprite = value;
            }
        }

        private string _backSprite;
        public string backSprite
        {
            get
            {
                return _backSprite;
            }
            set
            {
                _backSprite = value;
            }
        }

        public ObservableCollection<Moves> enemy_list_of_moves { get; set; }

        public MainWindowViewModel(ObservableCollection<Moves> selectedMoves, Pokemon playerPokemon, Pokemon enemyPokemon)
        {
            player = playerPokemon;
            enemy = enemyPokemon;

            list_of_moves = selectedMoves;

            frontSprite = enemy.front_sprite;
            backSprite = player.back_sprite;

            enemy_list_of_moves = getFourRandomMoves(enemy);
            player.isPlayerPokemon = "Your";
            enemy.isPlayerPokemon = "The opponent";
        }

        private ObservableCollection<Moves> getFourRandomMoves(Pokemon pokemon)
        {
            Random rand = new Random();
            var allEnemyMoves = Moveset_Database.Select_Moveset(pokemon.name);
            List<Moves> moves = allEnemyMoves[pokemon.name].ToList();
            var count = moves.Count;

            ObservableCollection<Moves> fourRandomMoves = new ObservableCollection<Moves>();
            for (int i = 0; i < 4; i++)
            {
                var randomNum = rand.Next(0, moves.Count);
                fourRandomMoves.Add(moves[randomNum]);
                moves.RemoveAt(randomNum);
            }

            return fourRandomMoves;
        }

        private bool hasBattleEnded(List<string> messages)
        {
            if (messages.Last() == "You won!" || messages.Last() == "You lost..." || messages.Last() == "...It's a draw!")
                return true;
            return false;
        }
    }
}