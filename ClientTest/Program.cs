using System;

namespace ClientTest
{
    class Person
    {
        public string Name { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Person { Name = "Pierre" };
            var p2 = new { Name = "Pierre" };

            var areSame = Likeness.Comparer.AreAlike(p1, p2);
            Console.WriteLine(areSame);
        }
    }
}
