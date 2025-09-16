using Library_Management.Reports;
using LMS.DTO;
using LMS.Entities;
using Microsoft.AspNetCore.Mvc;
namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BookController : ControllerBase
    {
        private BookRepo bookRepo = new BookRepo();
        /// <summary>
        /// Get All Books.
        /// </summary>
        /// <response code="200">List of Books found </response>
        /// <response code="204">List of Books not found</response>
        /// /// <remarks>
        /// This endpoint retrieves all book with their information.
        /// Example: GET /api/Book
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(BookData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status204NoContent)]
        public List<BookData> Get()
        {
            List<Book> books = bookRepo.GetBooks();
            return books.Select(x => new BookData(x)).ToList();
        }
        /// <summary>
        /// Get a required book by Id.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <response code="200">Book found</response>
        /// <response code ="404">Book not exist of that Id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status404NotFound)]
        public List<BookData> BookbyId(int id)
        {
            List<Book> books = bookRepo.GetBooks().Where(b => b.Id == id).ToList();
            return books.Select(x => new BookData(x)).ToList();
        }
        /// <summary>
        /// Add a new book.
        /// </summary>
        ///<response code= "201">Book Added Successfully</response>
        ///<response code ="400">Bad Request</response>
        /// <response code="409">Book already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookData), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status409Conflict)]
        public BookData Add([FromBody] BookDTO Book)
        {
            Book x = Book.ToBook();
            bookRepo.AddNew(x);
            return new BookData(x);
        }
        /// <summary>
        /// Update a Book.
        /// </summary>
        /// <param name="id">The unique identifier of the book to update.</param>
        /// <response code = "200"> Book Updated Succesfully</response>
        /// <response code = "404">Invalid Input </response>
        [HttpPut]
        [ProducesResponseType(typeof(BookData),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult) , StatusCodes.Status404NotFound)]
        public ActionResult<BookData> Update(int id, [FromBody] BookDTO book)
        {
            var existingBook = bookRepo.GetBooks().FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Tittle = book.Tittle;
            existingBook.Author = book.Author;
            existingBook.Category = book.Category;
            existingBook.Quantity = book.Quantity;
            bookRepo.UpdateBook(existingBook);
            return Ok(new BookData(existingBook));
        }
        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        /// <response code = "204">Book deleted successfully</response>
        /// /// <response code="404">Book with the specified ID not found</response>
        [HttpDelete]
        [ProducesResponseType(typeof(BookData),StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status404NotFound)]
        
        public ActionResult Delete(int id)
        {
            var bookid = bookRepo.GetBooks().FirstOrDefault(b => b.Id == id);
            if (bookid == null)
            {
                return NotFound();
            }
            bookRepo.Delete(id);
            return NoContent();
        }
        /// <summary>
        /// Get all books in PDF with download links.
        /// </summary>

        [HttpGet("books/report")]
        public IActionResult ExportBooksReport()
        {
            var books = bookRepo.GetBooks();
            string filePath = "E:/Book Report";

            BookReport.ExportBooksToPDF(books, filePath, "Books Report",DateTime.Now.AddDays(-7), DateTime.Now);

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf", "BooksReport.pdf");
        }


    }
}


    








