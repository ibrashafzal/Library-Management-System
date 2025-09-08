using LMS.Entities;

namespace LMS
{
   public class BookDAL
    {
        public List<Book> GetAllBooks()
        {
            using(var db = new LmsContext())
            {
                return db.Books.ToList();
            }
        }
        public void AddNewBook(Book book)
        {
            using(var db = new LmsContext())
            {
                book.AvailableQuantity = book.Quantity ?? 0;   
                db.Books.Add(book);
                db.SaveChanges();
            }
        }
        public void DeleteBook(int id)
        {
            using( var db = new LmsContext())
            {
                Book book  = db.Books.FirstOrDefault(b => b.Id == id);
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }
        public void UpdateBook(Book book)
        {
            using(var db =new LmsContext())
            {
                var existingBook = db.Books.Find(book.Id);
                if (existingBook != null)
                {
                    existingBook.Tittle = book.Tittle;
                    existingBook.Author = book.Author;
                    existingBook.Quantity = book.Quantity;
                    existingBook.Category = book.Category;


                    if (existingBook.AvailableQuantity > book.Quantity)  
                        existingBook.AvailableQuantity = book.Quantity; 
                    db.SaveChanges();
                }
            }
        }
        public void ReturnQuantity(int bookId, int issuedQty) 
        {
            using (var db = new LmsContext())
            {
                var book = db.Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    book.AvailableQuantity -= issuedQty;
                    if (book.AvailableQuantity < 0) book.AvailableQuantity = 0;
                    db.SaveChanges();
                }
            }
        }
        public void IncreaseAvailable(int bookId, int quantity)
        {
            using (var db = new LmsContext())
            {
                var book = db.Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    int available = book.AvailableQuantity ?? 0;
                    int totalQuantity = book.Quantity ?? 0;

                    int newAvailable = available + quantity;

                    
                    book.AvailableQuantity = Math.Min(newAvailable, totalQuantity);

                    db.SaveChanges();
                }
            }
        }





    }
}
