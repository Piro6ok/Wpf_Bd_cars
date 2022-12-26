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
    /// Логика взаимодействия для Query.xaml
    /// </summary>
    public partial class Query : Window
    {
        Context db = new Context();
        public Query()
        {
            InitializeComponent();

            db.Cars.Load();
            db.Stocks.Load();
        }

        public void Query1()
        {
            text.Text = "Найти на всех складах машину марки Alfa Romeo что имеются на складе (IsStock)";

            var bdcars = db.Cars.Include(c => c.Stock).Where(c => c.Name.Contains("Alfa Romeo")).ToList();
            DataContext = bdcars;
        }
        public void Query2()
        {
            text.Text = "Вывести все склады где есть машина BMW";

            var bdcars = db.Cars.Include(c => c.Stock)
                    .OrderBy(c => c.Stock)
                    .Where(c => c.Name.Contains("BMW"))
                    .GroupBy(c => c.Stock.Stocks)
                    .Select(g => new
                    {
                        Stocks = g.Key
                    }).ToList();
            DataContext = bdcars;
        }
        public void Query3()
        {
            text.Text = "Найти все машины которые стоят меньше 10000 $";

            var bdcars = db.Cars
                    .OrderBy(c => c.Cost)
                    .Where(c => c.Cost < 10000).ToList();
            DataContext = bdcars;
        }
        public void Query4()
        {
            text.Text = "Найти все машины с пометками для продажи (remark) и отсортировать по имени";

            var bdcars = db.Cars
                    .OrderBy(c => c.Name)
                    .Where(c => !string.IsNullOrEmpty(c.Remark)).ToList();
            DataContext = bdcars;
        }
        public void Query5()
        {
            text.Text = "Вывести все склады на которых есть машины с годом выпуска с 2000 до 2005 \n и отсортировать их по количеству машин на складе (сначала где их много)";

            var bdcars = db.Cars.Include(c => c.Stock)
                    .Where(c => c.DataRelease >= 2000 & c.DataRelease <= 2005)
                    .GroupBy(c => c.Stock.Stocks)
                    .Select(g => new
                    {
                        Stocks = g.Key,
                        Count = g.Count()
                    }).OrderByDescending(c => c.Count).ToList();
            DataContext = bdcars;
        }
        public void Query6()
        {
            text.Text = "Вывести все машины выпущенные до 2000 года и отсортировать по году выпуска";

            var bdcars = db.Cars.Include(c => c.Stock)
                    .OrderBy(c => c.DataRelease)
                    .Where(c => c.DataRelease < 2000).ToList();
            DataContext = bdcars;
        }
    }
}
