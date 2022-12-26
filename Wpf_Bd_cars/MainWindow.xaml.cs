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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Bd_cars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context db = new Context();
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Cars.Load();
            db.Stocks.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Cars.Local.ToObservableCollection();
        }

        // Кнопки
        private void Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CarWindow(object sender, RoutedEventArgs e)
        {
            CarWindow CarWindow = new CarWindow();
            CarWindow.Owner = this;

            CarWindow.Show();
        }
        private void StockWindow(object sender, RoutedEventArgs e)
        {
            SkockWindow StockWindow = new SkockWindow();
            StockWindow.Owner = this;

            StockWindow.Show();
        }

        private void Query1(object sender, RoutedEventArgs e)
        {
            Query query = new Query();
            query.Show();
            query.Query1();
        }

        private void Query2(object sender, RoutedEventArgs e)
        {
            Query query = new Query();
            query.Show();
            query.Query2();
        }

        private void Query3(object sender, RoutedEventArgs e)
        {
            Query query = new Query();
            query.Show();
            query.Query3();
        }

        private void Query4(object sender, RoutedEventArgs e)
        {
            Query query = new Query();
            query.Show();
            query.Query4();
        }

        private void Query5(object sender, RoutedEventArgs e)
        {
            Query query = new Query();
            query.Show();
            query.Query5();
        }

        private void Query6(object sender, RoutedEventArgs e)
        {
            Query query = new Query();
            query.Show();
            query.Query6();
        }
    }
}
