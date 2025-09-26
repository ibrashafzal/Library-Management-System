using LMS.Controllers;
using LMS.DTO;
using LMS.Entities;
using NUnit.Framework;

namespace LMS.Test.TestFiles
{
    public class BookTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllBooks_Success()
        {
            //AAA Pattern
            //Arrange
            BookController bookApi = new BookController();

            //Act
            List<BookData> books = bookApi.Get();

            //Assert
            //Expect(books, Is.Not.Null);
            //Actual Value, Expected Value, Message on failure
            Assert.IsNotNull(books,"Get all book is null");
            Assert.IsTrue(books.Count > 0, "No book found in the list");
        }
        [Test]
        public void GetBookById()
        {
            //Arrange
            BookController bookApi = new BookController();  
            int bookId = 1;
            //Act
            List<BookData> books = bookApi.BookbyId(bookId);
            //Assert    
            Assert.IsNotNull(books, "Get book by id is null");
            Assert.IsTrue(books.Count > 0, "No book found in the list");
        }
        [Test]
        public void AddNewBook()
        {
            //Arrange
            BookController bookApi = new BookController();
            BookDTO book = new BookDTO()
            {
                Tittle = "Java in Depth",
                Author = "James Gosling",
                Category = "Computer",
                Quantity = 5

            };
           
            //Act
            var addedBook = bookApi.Add(book);
            //Assert    
            Assert.NotNull(addedBook);
        }
        [Test]
        public void UpdateBook()
        {
            //Arrange
            BookController bookApi = new BookController();
            int bookId = 3002;
            BookDTO book = new BookDTO()
            {
                Tittle = "Java in Depth",
                Author = "James Gosling",
                Category = "Programming",
                Quantity = 10
            };
            //Act
            var updatedBook = bookApi.Update(bookId, book);
            //Assert    
            Assert.NotNull(updatedBook);
        }
        [Test]
        public void DeleteBoook()
        {   //Arrange
            BookController bookApi = new BookController();
            int bookId = 2002;
            //Act
            var deletedBook = bookApi.Delete(bookId);
            //Assert    
            Assert.NotNull(deletedBook);
        }
        [Test]
        public void PDFReport()
        {
            //Arrange
            BookController bookApi = new BookController();
            //Act
            var report = bookApi.ExportBooksReport();
            //Assert    
            Assert.NotNull(report);
        }
        [Test]
        public void SearchBook()
        {
            //Arrange
            BookRepo bookRepo = new BookRepo();
            string tittle = "Mr Chips";
            //Act
            var searchedBooks = bookRepo.Search(tittle);
            //Assert    
            Assert.NotNull(searchedBooks);
            Assert.IsTrue(searchedBooks.Count > 0, "No book found in the list");
        }
        [Test]
        public void ReturnBook()
        {
            //Arrange 
            BookDAL bookDAL = new BookDAL();
            int bookId = 1;
            int returnQty = 1;
            //Act
           bookDAL.ReturnQuantity(bookId, returnQty);
            //Assert    
            Assert.Pass();
        }
        [Test]
        public void IncreaseAvailable()
        {
            //Arrange 
            BookDAL bookDAL = new BookDAL();
            int bookId = 1;
            int Qty = 1;
            //Act
           bookDAL.IncreaseAvailable(bookId, Qty);
            //Assert    
            Assert.Pass();
        }
    }
}