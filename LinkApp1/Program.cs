using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // LINQ to Objects
            // Основы LINQ
            // Выберем из массива строки, начинающиеся на определенную букву 
            // и отсортируем полученный список
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };

            var selectedTeams = new List<string>();
            foreach (string s in teams)
            {
                if (s.ToUpper().StartsWith("Б"))
                    selectedTeams.Add(s);
            }
            // Сортирует элементы в списке
            selectedTeams.Sort();

            foreach (string s in selectedTeams)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine();
            // Сделаем это с помощью LINQ

            var selectedTeamsLinq = from t in teams // определяем каждый объект из teams как t
                                    where t.ToUpper().StartsWith("Б") // фильтрация по критерию
                                    orderby t descending // упорядочивает по убыванию descending
                                    select t;
            foreach (string s in selectedTeamsLinq)
            {
                Console.WriteLine(s);
            }

            // from переменная in набор_объектов
            // select переменная;

            Console.WriteLine();
            // Методы расширения LINQ

            var selectTeamByExtendLinq = teams.Where(t => t.ToUpper().StartsWith("Б")).OrderBy(t => t);

            foreach (string s in selectTeamByExtendLinq)
            {
                Console.WriteLine(s);
            }

            // возвращает количество элементов в выборке
            int num = (from t in teams where t.ToUpper().StartsWith("Б") select t).Count();

            Console.WriteLine(num);

            Console.ReadKey();
        }
    }
}
