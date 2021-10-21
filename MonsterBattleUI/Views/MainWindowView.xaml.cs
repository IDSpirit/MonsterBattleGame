using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MonsterBattle;
using MonsterBattle.Class_Library;
using MonsterBattleUI.Annotations;
using MonsterBattleUI.ViewModels;

namespace MonsterBattleUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ObservableCollection<Moves> playerMoves, Pokemon player, Pokemon enemy)
        {
            InitializeComponent();
            //var window = new MainWindowViewModel(); This creates a second vm since we already have the one we're working with that's set as the data context, so this vm object isn't the same as the one we're using for the UI

            this.DataContext = new MainWindowViewModel(playerMoves, player, enemy);

            Button_Click_Event = null; //This is necessary bc otherwise, the OnButtonClickHandlerMain gets subscribed to more than once when you do more than one match

            Button_Click_Event += ((MainWindowViewModel) (this.DataContext)).OnButtonClickHandlerMain; //This accesses the vm object that we've been using and is the same one that's attached as the data context to the ui

        }

        public delegate void Button_Click_Handler(object o);
        public static event Button_Click_Handler Button_Click_Event;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button_Click_Event(e);
        }

    }
}
