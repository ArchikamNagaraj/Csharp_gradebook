using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);// define how the method should be strictly data type
    public class TypeTest
    {

        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            //log=new WriteLogDelegate(ReturnMessage);//Long way to intialize deligate short way is down
            //this needs to be done because we should invoke the method 
            log += ReturnMessage;
            log += IncrimentcountMessage;
            var result = log("Hello");
            Assert.Equal(1, count);
        }

        string IncrimentcountMessage(string Message)
        {
            count++;
            return Message.ToLower();
        }


        string ReturnMessage(string Message)
        {
            return Message;
        }

        [Fact]
        public void GetBookreturnsDifferentObjects()
        {
            var book1 = Getbook("Book 1");
            var book2 = Getbook("Book 2");

            //Assert
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
        }

        [Fact]
        public void CanSetNameForReference()
        {
            var book1 = Getbook("Book 1");
            SetName(book1, "My Book");

            //Assert
            Assert.Equal("My Book", book1.Name);
        }

        private void SetName(InMemoryBook book, String name)
        {
            book.Name = name;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = Getbook("Book 1");
            GetBookSetName(book1, "My Book");

            //Assert
            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, String name)
        {
            book = new InMemoryBook(name);
            book.Name = name;
        }

        [Fact]
        public void CSharpIsPassByRefrence()
        {
            var book1 = Getbook("Book 1");
            GetBookSetName(out book1, "My Book");

            //Assert
            Assert.Equal("My Book", book1.Name);
        }

        private void GetBookSetName(out InMemoryBook book, String name)
        {
            book = new InMemoryBook(name);
            book.Name = name;

            //ref keyword is not used so often the same can be done by using out param
            //out param is used for instead of ref. If out is used then it is understood that object should be 
            //instailized explicitly.
        }

        [Fact]
        public void TwoVarscanReferSameObject()
        {
            var book1 = Getbook("Book 1");
            var book2 = book1;

            //Assert
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 1", book2.Name);
            Assert.Same(book1, book2); //check if both are referring the same values
            Assert.True(Object.ReferenceEquals(book1, book2)); //ReferenceEquals-static method which is available
                                                               // in class name called object in .Net framework and tells u both are referring to same object
        }

        InMemoryBook Getbook(string name)
        {
            return new InMemoryBook(name);
        }

        [Fact]
        public void test1()
        {
            var x = GetInt();
            SetInt(ref x);

            //Assert
            Assert.Equal(42, x);
        }

        private int GetInt()
        {
            return 3;
        }
        private void SetInt(ref int num)
        {
            num = 42;
        }

        [Fact]
        public void StringBehavelikeValueTypes()
        {
            string name = "Mani";
            var upper = Makeuppercase(name);

            //Assert
            Assert.Equal("MANI", upper);
        }

        private string Makeuppercase(string parameter)
        {
            return parameter.ToUpper();
        }
    }
}