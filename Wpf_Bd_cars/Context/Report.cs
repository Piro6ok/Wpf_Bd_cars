

namespace Wpf_Bd_cars
{
    public static class Report
    {
        public static void SQL()
        {
            //Файл

            DateTime thisDay = DateTime.Now;
            var thisDate = thisDay.ToString("dd.MM.yyyy");

            var thisHour = thisDay.ToString("hh");
            var thisMinute = thisDay.ToString("mm");
            var thisSecond = thisDay.ToString("ss");

            string path = $@"C:\Флешка\C#\Лб2\Отчеты\{thisDate} {thisHour}-{thisMinute}-{thisSecond}.txt";
            File.WriteAllText(path, "");

            //Запросы

            //1. Найти на всех складах машину марки Alfa Romeo что имеются на складе (IsStock)

            using (Context db = new Context())
            {
                var bdcars = from c in db.Cars
                             join s in db.Stocks
                             on c.Stock.Id equals s.Id
                             where c.Name.Contains("Alfa Romeo")
                             group s by s.Stocks into g
                             select new
                             {
                                 Stocks = g.Key
                             };

                WriteLine("");
                WriteLine("Склады содержащие Alpha Romeo");
                WriteLine("");
                File.AppendAllLines(path, new[] { $"Склады содержащии Alpha Romeo", "" });
                foreach (var s in bdcars)
                {
                    WriteLine($" - {s.Stocks}");
                    File.AppendAllLines(path, new[] { $" - {s.Stocks}" });
                }
            }

            //2. Вывести все склады где есть машина BMW

            using (Context db = new Context())
            {
                var bdcars = db.Cars.Include(c => c.Stock)
                    .OrderBy(c => c.Stock)
                    .Where(c => c.Name.Contains("BMW"))
                    .GroupBy(c => c.Stock.Stocks)
                    .Select(g => new
                    {
                        Stocks = g.Key
                    });

                WriteLine("");
                WriteLine("Склады где есть BMW");
                WriteLine("");
                File.AppendAllLines(path, new[] { "", "Склады где есть BMW", "" });
                foreach (var c in bdcars)
                {
                    WriteLine($" - {c.Stocks}");
                    File.AppendAllLines(path, new[] { $" - {c.Stocks}" });
                }
            }

            //3. Найти все машины которые стоят меньше 10000 $

            using (Context db = new Context())
            {
                var bdcars = db.Cars
                    .OrderBy(c => c.Cost)
                    .Where(c => c.Cost < 10000);

                WriteLine("");
                WriteLine("Машины дешевле 10000$");
                WriteLine("");
                File.AppendAllLines(path, new[] { "", "Машины дешевле 10000$", "" });
                foreach (var c in bdcars)
                {
                    WriteLine($" - {c.Name} Цена: {c.Cost}");
                    File.AppendAllLines(path, new[] { $" - {c.Name} Цена: {c.Cost}" });
                }
            }

            //4. Найти все машины с пометками для продажи (remark) и отсортировать по имени

            using (Context db = new Context())
            {
                var bdcars = db.Cars
                    .OrderBy(c => c.Name)
                    .Where(c => c.Remark != "");

                WriteLine("");
                WriteLine("Машины с пометками");
                WriteLine("");
                File.AppendAllLines(path, new[] { "", "Машины с пометками", "" });
                foreach (var c in bdcars)
                {
                    WriteLine($" - {c.Name} Цена: {c.Cost} Пометка: {c.Remark}");
                    File.AppendAllLines(path, new[] { $" - {c.Name} Цена: {c.Cost} Пометка: {c.Remark}" });
                }
            }

            //5. Вывести все склады на которых есть машины с годом выпуска с 2000 до 2005
            //и отсортировать их по количеству машин на складе (сначала где их много)

            using (Context db = new Context())
            {
                var bdcars = db.Cars.Include(c => c.Stock)
                    .Where(c => c.DataRelease >= 2000 & c.DataRelease <= 2005)
                    .GroupBy(c => c.Stock.Stocks)
                    .Select(g => new
                    {
                        Stocks = g.Key,
                        Count = g.Count()
                    }).OrderByDescending(c => c.Count);

                WriteLine("");
                WriteLine("Все склады где есть машины с годом выпуска с 2000 до 2005");
                WriteLine("");
                File.AppendAllLines(path, new[] { "", "Все склады где есть машины с годом выпуска с 2000 до 2005", "" });
                foreach (var c in bdcars)
                {
                    WriteLine($" - {c.Stocks} Кол-во: {c.Count}");
                    File.AppendAllLines(path, new[] { $" - {c.Stocks} Кол-во: {c.Count}" });
                }
            }

            //6. Вывести все машины выпущенные до 2000 года и отсортировать по году выпуска

            using (Context db = new Context())
            {
                var bdcars = db.Cars.Include(c => c.Stock)
                    .OrderBy(c => c.DataRelease)
                    .Where(c => c.DataRelease < 2000);

                WriteLine("");
                WriteLine("Все машины выпущенные до 2000 года");
                WriteLine("");
                File.AppendAllLines(path, new[] { "", "Все машины выпущенные до 2000 года", "" });
                foreach (var c in bdcars)
                {
                    WriteLine($" - {c.Name} Дата выпуска: {c.DataRelease}");
                    File.AppendAllLines(path, new[] { $" - {c.Name} Дата выпуска: {c.DataRelease}" });
                }
            }
        }
    }
}
