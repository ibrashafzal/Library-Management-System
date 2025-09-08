


using LMS.Entities;

namespace LMS
{
    public class BookRepo
    {
        public BookDAL bookDAL= new BookDAL(); 


        public List<Book> GetBooks()
        {
           return bookDAL.GetAllBooks();
        }


        public void AddNew(Book book)
        {
            bookDAL.AddNewBook(book);            
        }
        public void Delete(int id)
        {
            bookDAL.DeleteBook(id);
        }



        public void UpdateBook(Book book)
        {
            bookDAL.UpdateBook(book);
        }


        public void DeleteBook(int id)
        {
            bookDAL.DeleteBook(id);
        }
        public List<Book> Search(string searchText)
        {

            List<Book> books = bookDAL.GetAllBooks();
            List<Book> filtered = new List<Book>();
            if (string.IsNullOrWhiteSpace(searchText))
                return books;

            searchText = searchText.ToLower();
            foreach (Book book in books)
            {
                if ((book.Tittle != null && book.Tittle.ToLower().Contains(searchText)) ||
                    (book.Author != null && book.Author.ToLower().Contains(searchText)) ||
                    (book.Category != null && book.Category.ToLower().Contains(searchText)))


                {
                    filtered.Add(book);
                }
            }

            return filtered;
        }
        public void DecreaseAvailable(int bookId, int issuedQty)
        {
            bookDAL.ReturnQuantity(bookId, issuedQty);
        }
        public void Increase(int bookId, int quantity)
        {
            bookDAL.IncreaseAvailable(bookId, quantity);
        }





    }
}



