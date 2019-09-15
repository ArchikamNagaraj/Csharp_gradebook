using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTest
    {

        [Fact]
        public void BookCalculatesAnAverageGrade()
        {
            //arrange
            var book = new InMemoryBook("Nagaraj");
            book.AddGrade(89.1);
            book.AddGrade(97.2);
            book.AddGrade(77.3);

            //act
            var result = book.GetStatistics();

            //assert            
            Assert.Equal(87.9, result.Average, 1);
            Assert.Equal(97.2, result.High, 1);
            Assert.Equal(77.3, result.Low, 1);
            Assert.Equal('B', result.Letter);
        }

        // [Fact]
        // public void CheckGradeInputs () {

        //     var book = new Book ("Grade input test");
        //     book.AddGrade (105);
        //     book.AddGrade (-1);

        //     //act
        //     var result = book.GetStatistics ();

        //     //assert            
        //     Assert.Equal (double.NaN, result.Average, 1);

        // }
    }
}