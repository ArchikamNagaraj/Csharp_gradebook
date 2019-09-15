using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Diskbook("Nagaraj's Book module checks");
            book.GradeAdded += OnGradeAdded;//calling the delegate
            EnterGrades(book);
            var stats = book.GetStatistics();

            Console.WriteLine($"The module is {book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");

        }

        private static void EnterGrades(IBook book)//instead of Inmemorybook Book given to decide inmemory / files
        {
            while (true)
            {
                //adding comment to check
                // Console.WriteLine("Enter Bookname");
                // var bookname=Console.ReadLine();
                // book.name(bookname);
                Console.WriteLine("Enter a grade or q to quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
