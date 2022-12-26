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

namespace Wpf_Bd_cars
{
    /// <summary>
    /// Логика взаимодействия для EditCarWindow.xaml
    /// </summary>
    public partial class EditCarWindow : Window
    {
        Context db = new Context();
        public Car Car { get; private set; }
        public EditCarWindow(Car car)
        {
            InitializeComponent();

            Car = car;
            DataContext = Car;
            db.Stocks.Load();

            stockList.ItemsSource = db.Stocks.Local.ToObservableCollection();
            stockList.SelectedValuePath = "Id";
            stockList.DisplayMemberPath = "Stocks";
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            var stock = db.Stocks.Find(stockList.SelectedValue);

            Car.Stock = stock;
            DialogResult = true;
        }
    }
}
