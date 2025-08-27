using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.UI;

namespace Library_Management.DAL
{
   public class BookDAL
    {
        public List<Book> GetAllBooks()
        {
            using(var db = new LMSEntities2())
            {
                return db.Books.ToList();
            }
        }
        public void AddNewBook(Book book)
        {
            using(var db = new LMSEntities2())
            {
                book.AvailableQuantity = book.Quantity ?? 0;    // NEW LINE FOR ISSUE
                db.Books.Add(book);
                db.SaveChanges();
            }
        }
        public void DeleteBook(int id)
        {
            using( var db = new LMSEntities2())
            {
                Book book  = db.Books.FirstOrDefault(b => b.Id == id);
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }
        public void UpdateBook(Book book)
        {
            using(var db =new LMSEntities2())
            {
                var existingBook = db.Books.Find(book.Id);
                if (existingBook != null)
                {
                    existingBook.Tittle = book.Tittle;
                    existingBook.Author = book.Author;
                    existingBook.Quantity = book.Quantity;
                    existingBook.Category = book.Category;


                    if (existingBook.AvailableQuantity > book.Quantity)  //NEW
                        existingBook.AvailableQuantity = book.Quantity; //NEW
                    db.SaveChanges();
                }
            }
        }
        public void DecreaseAvailableQuantity(int bookId, int issuedQty) //NEW
        {
            using (var db = new LMSEntities2())
            {
                var book = db.Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    book.AvailableQuantity -= issuedQty;
                    if (book.AvailableQuantity < 0) book.AvailableQuantity = 0; // safety
                    db.SaveChanges();
                }
            }
        }
        public void IncreaseAvailableQuantity(int bookId, int quantity)
        {
            using (var db = new LMSEntities2())
            {
                var book = db.Books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    int available = book.AvailableQuantity ?? 0;
                    int totalQuantity = book.Quantity ?? 0;

                    int newAvailable = available + quantity;

                    // Ensure it does not exceed total quantity
                    book.AvailableQuantity = Math.Min(newAvailable, totalQuantity);

                    db.SaveChanges();
                }
            }
        }





    }
}
