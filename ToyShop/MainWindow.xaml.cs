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
using ToyShop.Pages;

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
            IEnumerable<Toy> Filtertoys = DBConnect.db.Toy;
            if(SortCb.SelectedIndex > 0) 
            {
                if (SortCb.SelectedIndex == 1)
                    Filtertoys = Filtertoys.OrderBy(x => x.Cost);
                else
                    Filtertoys = Filtertoys.OrderByDescending(x => x.Cost);
            }

            var DiscountCb = FilterCb.SelectedItem as ComboBoxItem;
            if(DiscountCb != null)
            {
                
            }

            if (NameSearchTb.Text.Length > 0)
                Filtertoys = Filtertoys.Where(x => x.Name.ToLower().StartsWith(NameSearchTb.Text.ToLower()) || x.Description.ToLower().StartsWith(NameSearchTb.Text.ToLower()));

            ShopList.ItemsSource = Filtertoys.ToList();
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

        private void Admin1_Click(object sender, RoutedEventArgs e)
        {
            if (CodTb.Text.Equals("0000") )
            {
                AddBtn.Visibility = Visibility.Visible;
                EditBtn.Visibility = Visibility.Visible; 
                DeleteBtn.Visibility = Visibility.Visible;
                Admin1.Visibility = Visibility.Collapsed;
            }
        }
    }
}
