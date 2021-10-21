using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MonsterBattle;
using MonsterBattle.Class_Library;
using MonsterBattleUI.Annotations;
using MonsterBattleUI.Models;

namespace MonsterBattleUI.ViewModels
{
    public class SelectPokemonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnButtonClickHandler(object o)
        {
            MainWindow = null;
            if (playerPokemon != null && enemyPokemon != null)
            {
                if (move1 != null && move2 != null && move2 != null && move4 != null)
                {
                    if (move1 != move2 && move1 != move3 && move1 != move4 && move2 != move3 && move2 != move4 &&
                        move3 != move4)
                    {
                        var y = Application.Current.Windows;

                        selectedPlayerMoves = new ObservableCollection<Moves>();
                        selectedPlayerMoves.Add(move1);
                        selectedPlayerMoves.Add(move2);
                        selectedPlayerMoves.Add(move3);
                        selectedPlayerMoves.Add(move4);

                        MainWindow = new MainWindow(new ObservableCollection<Moves>() { move1, move2, move3, move4 }, playerPokemon, enemyPokemon);
                        MainWindow.ShowDialog();

                        InstantiatePokemonObjects();
                    }
                    else
                        MessageBox.Show("You've selected the same move more than once!");
                }
                else
                    MessageBox.Show("You have to select 4 moves!");
            }
            else
                MessageBox.Show("You have to select both pokemon!");
        }

        private ObservableCollection<Pokemon> _allPlayerPokemonList;

        public ObservableCollection<Pokemon> allPlayerPokemonList
        {
            get
            {
                return _allPlayerPokemonList;
            }
            set
            {
                _allPlayerPokemonList = value;
                OnPropertyChanged("allPlayerPokemonList");
            }
        }

        private ObservableCollection<Pokemon> _allEnemyPokemonList;

        public ObservableCollection<Pokemon> allEnemyPokemonList
        {
            get
            {
                return _allEnemyPokemonList;
            }
            set
            {
                _allEnemyPokemonList = value;
                OnPropertyChanged("allEnemyPokemonList");
            }
        }

        public ObservableCollection<Moves> selectedPlayerMoves { get; set; }

        private Pokemon _playerPokemon;
        public Pokemon playerPokemon
        {
            get
            {
                return _playerPokemon;
            }
            set
            {
                _playerPokemon = value;
                if (value != null)
                {
                    UpdateSelectedPokemonMoves(playerPokemon.name);
                    playerType1 = CapitaliseExtensions.CapitaliseFirstLetter(playerPokemon.type_1);

                    if (playerPokemon.type_1 != playerPokemon.type_2)
                    {
                        playerType2 = CapitaliseExtensions.CapitaliseFirstLetter(playerPokemon.type_2);
                    }
                    else
                        playerType2 = "";
                }

                OnPropertyChanged("playerPokemon");
            }
        }

        private Pokemon _enemyPokemon;

        public Pokemon enemyPokemon
        {
            get
            {
                return _enemyPokemon;
            }
            set
            {
                _enemyPokemon = value;
                if (value != null)
                {
                    enemyType1 = CapitaliseExtensions.CapitaliseFirstLetter(enemyPokemon.type_1);

                    if (enemyPokemon.type_1 != enemyPokemon.type_2)
                    {
                        enemyType2 = CapitaliseExtensions.CapitaliseFirstLetter(enemyPokemon.type_2);
                    }
                    else
                        enemyType2 = "";
                }

                OnPropertyChanged("enemyPokemon");
            }
        }
        private ObservableCollection<Moves> _playerMoves;
        public ObservableCollection<Moves> playerMoves 
        {
            get
            {
                return _playerMoves;
            }
            set
            {
                _playerMoves = value;
                OnPropertyChanged("playerMoves");
            }
        }

        private Moves _move1;
        public Moves move1
        {
            get
            {
                return _move1;
            }
            set
            {
                _move1 = value;
                OnPropertyChanged("move1");
            }
        }

        private Moves _move2;
        public Moves move2
        {
            get
            {
                return _move2;
            }
            set
            {
                _move2 = value;
                OnPropertyChanged("move2");
            }
        }

        private Moves _move3;

        public Moves move3
        {
            get
            {
                return _move3;
            }
            set
            {
                _move3 = value;
                OnPropertyChanged("move3");
            }
        }

        private Moves _move4;

        public Moves move4
        {
            get
            {
                return _move4;
            }
            set
            {
                _move4 = value;
                OnPropertyChanged("move4");
            }
        }

        private string _playerType1;
        public string playerType1
        {
            get
            {
                return _playerType1;
            }
            set
            {
                if (value != "")
                    _playerType1 = $"Type 1: {value}";
                else
                    _playerType1 = ""; //Needed so we can initialise this when doing a second match
                OnPropertyChanged("playerType1");
            }
        }

        private string _playerType2;
        public string playerType2
        {
            get
            {
                return _playerType2;
            }
            set
            {
                if (value != "")
                    _playerType2 = $"Type 2: {value}";
                else
                    _playerType2 = "";
                OnPropertyChanged("playerType2");
            }
        }

        private string _enemyType1;
        public string enemyType1
        {
            get
            {
                return _enemyType1;
            }
            set
            {
                if (value != "")
                    _enemyType1 = $"Type 1: {value}";
                else
                    _enemyType1 = "";
                OnPropertyChanged("enemyType1");
            }
        }

        private string _enemyType2;
        public string enemyType2
        {
            get
            {
                return _enemyType2;
            }
            set
            {
                if (value != "")
                    _enemyType2 = $"Type 2: {value}";
                else
                    _enemyType2 = "";
                OnPropertyChanged("enemyType2");
            }
        }

        private MainWindowViewModel _MainWindowViewModel;

        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return _MainWindowViewModel;
            }
            set
            {
                _MainWindowViewModel = value;
                OnPropertyChanged("MainWindowViewModel");
            }
        }

        private MainWindow _MainWindow;

        public MainWindow MainWindow
        {
            get
            {
                return _MainWindow;
            }
            set
            {
                _MainWindow = value;
                OnPropertyChanged("MainWindow");
            }
        }

        public SelectPokemonViewModel()
        {
            InstantiatePokemonObjects();
        }

        private void InstantiatePokemonObjects()
        {
            var pokemonList = new List<string>() {"Volcarona", "Skarmory", "Buneary", "Wigglytuff", "Bisharp"};
            playerPokemon = null;
            enemyPokemon = null;
            playerType1 = "";
            playerType2 = "";
            enemyType1 = "";
            enemyType2 = "";

            move1 = null;
            move2 = null;
            move3 = null;
            move4 = null;

            allPlayerPokemonList = null;
            allEnemyPokemonList = null;

            allPlayerPokemonList = new ObservableCollection<Pokemon>();

            foreach (var pokemon in pokemonList)
            {
                allPlayerPokemonList.Add(Pokemon_Database.Select_Pokemon(pokemon));
            }

            allEnemyPokemonList = null;
            allEnemyPokemonList = new ObservableCollection<Pokemon>();
            foreach (var pokemon in pokemonList)
            {
                allEnemyPokemonList.Add(Pokemon_Database.Select_Pokemon(pokemon));
            }
        }

        private void UpdateSelectedPokemonMoves(string pokemon)
        {
            var moveDictionary = Moveset_Database.Select_Moveset(pokemon).Values;
            playerMoves = new ObservableCollection<Moves>();
            foreach (var move in moveDictionary)
            {
                for (int i = 0; i < move.Length; i++)
                {
                    playerMoves.Add(move[i]);
                }
            }
        }
    }
}
