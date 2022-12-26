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
    /// Логика взаимодействия для SkockWindow.xaml
    /// </summary>
    public partial class SkockWindow : Window
    {
        Context db = new Context();
        public SkockWindow()
        {
            InitializeComponent();

            db.Stocks.Load();
            DataContext = db.Stocks.Local.ToObservableCollection();
        }
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Stock? stock = stocksList.SelectedItem as Stock;
            // если ни одного объекта не выделено, выходим
            if (stock is null) return;

            db.SaveChanges();
            stocksList.Items.Refresh();
        }
    }
}
