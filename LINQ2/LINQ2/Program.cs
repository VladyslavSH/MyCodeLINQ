using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2
{
    class User {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        public User()
        {
            Languages = new List<string>();
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Name: {Name}, Age: {Age}");
            foreach (var item in Languages)
            {
                builder.Append(" "+item);
            }
            return builder.ToString();
        }
    }
    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
       
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] teams = {"Бавария", "Барселона", "Реал", "Манчестер Сити", "ПСЖ", "Борусия" };
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 88, 66 };
            SimpleLinq(teams);
            FilterLinq(numbers);
            List<User> users = new List<User>
            {
                new User{ Name="Cristiano", Age = 32, Languages = new List<string>{ "Испанский", "Итальянский"} },
                new User{ Name="Leonel", Age = 31, Languages = new List<string>{ "Испанский", "Француский"} },
                new User{ Name="Andrea", Age = 40, Languages = new List<string>{ "Испанский", "Итальянский"} },
                new User{ Name="Mesut", Age = 29, Languages = new List<string>{ "Испанский", "Немецкий"} },
                new User{ Name="Isaac", Age = 26, Languages = new List<string>{ "Английский", "Итальянский"} },
            };
            List<Phone> phones = new List<Phone>
            {
                new Phone{ Name = "Lumia 630", Company = "Microsoft" },
                new Phone{ Name = "iPhomne", Company = "Apple"}
            };
            QueryObject(users);
            SelectQuery(users);
            UnionColoction(users, phones);
            SkipAndWhile(numbers, teams);
            Console.ReadKey();
        }

        private static void SkipAndWhile(int[] numbers, string [] teams)
        {
            Console.WriteLine();
            var result = numbers.Take(5);
            result = numbers.Skip(5);
            result = numbers.Skip(4).Take(2);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            foreach (var item in teams.TakeWhile(x=>x.StartsWith("Б")))
            {
                Console.WriteLine(item);
            }
        }

        private static void UnionColoction(List<User> users, List<Phone> phones)
        {
            Console.WriteLine();
            var people = from user in users
                         from phone in phones
                         select new { Name = user.Name, Phones = phone.Name };
            foreach (var item in people)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            var result = users.Select(u => u.Name).Union(phones.Select(p => p.Name));
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            
        }

        private static void SelectQuery(List<User> users)
        {
            var names = from u in users select u.Name;
            PrintArray(names);
            Console.WriteLine("========================");
            var items = from u in users select new { FirstName = u.Name, NewAge = u.Age };

            var items2 = users.Select(u => new { FirstName = u.Name, NewAge = u.Age });

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            PrintArray(names);

            foreach (var item in items2)
            {
                Console.WriteLine(item);
            }
            PrintArray(names);
        }

        private static void QueryObject(List<User> users)
        {
            var selectedUsers = from user in users where user.Age > 25 select user;
            PrintArray(selectedUsers);
            Console.WriteLine("========================");
            var selectedHardUsers = from user in users
                                    from lang in user.Languages
                                    where user.Age > 25
                                    where lang == "Английский"
                                    select user;
            PrintArray(selectedHardUsers);
            Console.WriteLine("========================");
            selectedHardUsers = users.SelectMany(u => u.Languages, (u, l) => new { user = u, lang = l }).Where(u => u.lang == "Английский" && u.user.Age > 25).Select(u => u.user);
            PrintArray(selectedHardUsers);
            Console.WriteLine("========================");
        }

        private static void FilterLinq(int[] numbers)
        {
            var evens = from i in numbers where i % 2 == 0 && i > 10 select i;
            PrintArray(evens);
            Console.WriteLine("========================");
        }

        private static void SimpleLinq(string [] teams)
        {
            var selectedTeams = from t in teams where t.ToUpper().StartsWith("Б") orderby t select t;
            PrintArray(selectedTeams);
            Console.WriteLine("========================");
        }

        private static void PrintArray(IEnumerable<int> selectedTeams)
        {
            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
        }
        private static void PrintArray(IOrderedEnumerable<string> selectedTeams)
        {
            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
        }

        private static void PrintArray(IEnumerable<User> selectedTeams)
        {
            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
        }
        private static void PrintArray(IEnumerable<string> selectedTeams)
        {
            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
        }
    }
}
