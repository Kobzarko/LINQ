using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Фильтрация выборки и проекция

            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

            // Здесь используется конструкция from: from i in numbers
            IEnumerable<int> evens = from i in numbers
                                     where i % 2 == 0 && i > 10
                                     select i;

            // запрос с помощью метода расширения

            IEnumerable<int> evensExtend = numbers.Where(i => i % 2 == 0 && i > 10);

            // Выборка сложных объектов

            
            Console.WriteLine("--------------------------------------------Выборка сложных объектов");

            List<User> users = new List<User>
            {
                new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
                new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
                new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
                new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            };

            // Создадим набор пользователей и выберем из них тех, которым больше 25 лет

            var selectedUsers = from user in users
                                where user.Age > 25
                                select user;

            foreach (User user in selectedUsers)
            {
                Console.WriteLine($"{user.Name} - {user.Age} лет");
            }

            //  аналогичный запрос с помощью метода where

            var selectedUsers2 = users.Where(u => u.Age > 25);

            Console.WriteLine("--------------------------------------------Сложные фильтры");

            // Сложные фильтры------------------------------------------------

            // Отфильтровать пользователей по языку

            var selectedLangUsers = from user in users
                                    from lang in user.Languages
                                    where user.Age < 28
                                    where lang == "английский"
                                    select user;

           

            foreach (var item in selectedLangUsers)
            {
                Console.WriteLine($" user - {item.Name} - language {item.Languages[0]}");
                
            }
            // вариант сложной выборки
            var selLanUsers = users.Where(u => u.Age < 28 && u.Languages.Exists(l => l == "английский"));

            foreach (var item in selLanUsers)
            {
                Console.WriteLine($" user - {item.Name} - language {item.Languages[0]}");

            }

            // применянем метод расширения SelectMany

            var selectLangUsers = users.SelectMany(u => u.Languages,
                                   (u, l) => new { User = u, Lang = l })
                                   .Where(u => u.Lang == "английский" && u.User.Age < 28)
                                   .Select(u => u.User);

            foreach (var item in selectLangUsers)
            {
                Console.WriteLine($" user - {item.Name} - language {item.Languages[0]}");
            }

            Console.WriteLine("--------------------------------------------ПРОЕКЦИЯ");
            // ПРОЕКЦИЯ ---------------------------------------

            //Проекция позволяет спроектировать из текущего типа выборки какой-то другой тип

            // создаем список из класса Person (внизу)
            List<Person> people = new List<Person>
            {
                new Person{Name="Vasya", Age = 26},
                new Person{ Name= "Kolyan", Age = 22},
                new Person{ Name = "Billy", Age = 29}
            };
            // Выберем только имена

            var names = from p in people select p.Name;

            foreach(string n in names)
            {
                Console.WriteLine($" name - {n}");
            }

            Console.WriteLine();
            // аналогично создать объекты другого типа, в том числе анонимного

            var anonymType = from p in people
                             select new
                             {
                                 FirstName = p.Name,
                                 DayOfBurthday = DateTime.Now.Year - p.Age
                             };

            // результат будет содержать набор объектов данного анонимного типа
            foreach (var ano in anonymType)
            {
                Console.WriteLine($" name - {ano.FirstName} , BirthDay - {ano.DayOfBurthday}");
            }

            // альтернативный вариант 
            // выборка имен

            var names2 = people.Select(p => p.Name);

            // выборка объектов анонимного типа

            var anonymType2 = people.Select(p => new { 
                              FirstName = p.Name,
                              DateOfBirthday = DateTime.Now.Year - p.Age});

            //Переменые в запросах и оператор let

            Console.WriteLine("-----------------------------------------------Переменые в запросах и оператор let");

            // Иногда возникает необходимость произвести в запросах LINQ 
            // какие-то дополнительные промежуточные вычисления

            var persons = from p in people
                          let name = "Mr. " + p.Name // промежуточная переменная
                          select new
                          {
                              Name = name,
                              Age = p.Age
                             
                          };
            foreach (var item in persons)
            {
                Console.WriteLine($"Hello {item.Name}");
            }

            // Выборка из нескольких источников

            Console.WriteLine("-------------------------------------Выборка из нескольких источников");

            //В LINQ можно выбирать объекты не только из одного, но и из большего количества источников:

            // Создадим два разных источника данных и произведем выборку , список из класса Person уже есть

            List<Phone> phones = new List<Phone>()
            {
                new Phone { Title = "Redmi Note 8 pro" , Company="Xiaomi"},
                new Phone { Title = "IPhone X", Company ="Apple"},
                new Phone { Title="Galaxy A51", Company = "Samsung"}
            };

            var personPhones = from p in people
                               from ph in phones
                               select new
                               {
                                   Name = p.Name,
                                   Company = ph.Company,
                                   Title = ph.Title
                               };
            Console.WriteLine(" Name  -  Phone");
            foreach (var item in personPhones)
            {
                Console.WriteLine($"{item.Name}   {item.Company} {item.Title} ");
            }



            Console.ReadKey();
    }
}
}



public class Phone
{
    public string Title { get; set; }
    public string Company { get; set; }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}