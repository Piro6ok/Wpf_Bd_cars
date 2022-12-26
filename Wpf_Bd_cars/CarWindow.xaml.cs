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
    /// Логика взаимодействия для CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {

        Context db = new Context();
        public CarWindow()
        {
            InitializeComponent();

            db.Cars.Load();
            DataContext = db.Cars.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            EditCarWindow EditCarWindow = new EditCarWindow(new Car());
            if (EditCarWindow.ShowDialog() == true)
            {
                Car Car = EditCarWindow.Car;
                db.Cars.Add(Car);
                db.SaveChanges();
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Car? car = carsList.SelectedItem as Car;
            // если ни одного объекта не выделено, выходим
            if (car is null) return;

            EditCarWindow EditCarWindow = new EditCarWindow(new Car
            {
                Id = car.Id,
                Name = car.Name,
                DataRelease = car.DataRelease,
                Cost = car.Cost,
                Remark = car.Remark,
                IsStock = car.IsStock,
                Stock = car.Stock
            });

            if (EditCarWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                car = db.Cars.Find(EditCarWindow.Car.Id);
                if (car != null)
                {
                    car.Name = EditCarWindow.Car.Name;
                    car.Cost = EditCarWindow.Car.Cost;
                    car.Remark = EditCarWindow.Car.Remark;
                    car.IsStock = EditCarWindow.Car.IsStock;
                    car.Stock = EditCarWindow.Car.Stock;
                    db.SaveChanges();
                    carsList.Items.Refresh();
                }
            }
        }

        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Car? car = carsList.SelectedItem as Car;
            // если ни одного объекта не выделено, выходим
            if (car is null) return;
            db.Cars.Remove(car);
            db.SaveChanges();
        }
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Car? car = carsList.SelectedItem as Car;
            // если ни одного объекта не выделено, выходим
            if (car is null) return;

            db.SaveChanges();
            carsList.Items.Refresh();
        }
    }
}
