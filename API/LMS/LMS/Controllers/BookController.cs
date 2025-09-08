using LMS.DTO;
using LMS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookRepo bookRepo = new BookRepo();
        [HttpGet]
        public List<BookData> Get()
        {
            List<Book> books = bookRepo.GetBooks();
            return books.Select(x => new BookData(x)).ToList();
        }
        [HttpPost]
        public BookData Add ([FromBody]BookDTO Book)
        {
            Book x = Book.ToBook();
            bookRepo.AddNew(x);
            return new BookData(x);
        }
        [HttpPut]
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
        [HttpDelete]
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

    }
}
