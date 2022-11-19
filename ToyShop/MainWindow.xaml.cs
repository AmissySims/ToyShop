using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToyShop.Components;

namespace ToyShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public ObservableCollection<Toy> Toys
        {
            get { return (ObservableCollection<Toy>)GetValue(ToysProperty); }
            set { SetValue(ToysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToysProperty =
            DependencyProperty.Register("Toys", typeof(ObservableCollection<Toy>), typeof(MainWindow));


        public MainWindow()
        {
            DBConnect.db.Toy.Load();
            Toys = DBConnect.db.Toy.Local;

            InitializeComponent();
        }
        public void Refresh()
        {

        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void NameSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
