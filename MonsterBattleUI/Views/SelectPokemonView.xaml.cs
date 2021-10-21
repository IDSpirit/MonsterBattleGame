using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonsterBattleUI.ViewModels;

namespace MonsterBattleUI.Views
{
    /// <summary>
    /// Interaction logic for SelectPokemonView.xaml
    /// </summary>
    public partial class SelectPokemonView : Window
    {
        public SelectPokemonView()
        {
            InitializeComponent();
            Button_Click_Event += ((SelectPokemonViewModel)(this.DataContext)).OnButtonClickHandler;
        }

        public delegate void Button_Click_Handler(object o);
        public static event Button_Click_Handler Button_Click_Event;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button_Click_Event(e);
        }
    }
}
