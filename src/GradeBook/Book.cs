using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get; set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {

        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();

    }

    public class Diskbook : Book
    {
        public Diskbook(string name) : base(name)
        {

        }
        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var Writer = File.AppendText($"{Name}.txt"))
            {
                Writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }
            return result;

        }
    }
    public class InMemoryBook : Book
    {
        //create a delegate event to call whenever a new grade is added
        private List<double> grades;

        // public string Name { get; set; } //not needed since we are inherting 
        public const string CATEGORY = "Education";
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            this.Name = name;
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(75);
                    break;
                case 'C':
                    AddGrade(60);
                    break;
                case 'D':
                    AddGrade(50);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        //Create an event for grade addeddelegate 
        public override event GradeAddedDelegate GradeAdded;
        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                //.. we need to create event where it keeps track of how many grades has been added
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);
            }

            return result;
        }

    }
}