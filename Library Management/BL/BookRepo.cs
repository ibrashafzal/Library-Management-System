using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library_Management.Model;
using Library_Management.PL;
using Library_Management.User_Management;

namespace Library_Management.BL
{
    public class BookRepo
    {
        Persistent Persistent = new Persistent();
        public BookRepo()
        {
            // Load books from file into memory
            Persistent.LoadBooks();
        }
        public void AddBook(BookModel book)
        {

            Persistent.AddBooks(book);
        }

        public List<BookModel> GetAll()
        {
            return Persistent.Books;
        }
        // Search book
        public List<BookModel> Search(string searchText)
        {
            List<BookModel> books = Persistent.Books;
            List<BookModel> filteredBooks = new List<BookModel>();

            if (string.IsNullOrWhiteSpace(searchText))
                return books;

            searchText = searchText.ToLower();

            foreach (BookModel book in books)
            {
                if ((book.Tittle != null && book.Tittle.ToLower().Contains(searchText)) ||
                    (book.Author != null && book.Author.ToLower().Contains(searchText)) ||
                    (book.Category != null && book.Category.ToLower().Contains(searchText)) ||
                    book.Quantity.ToString().Contains(searchText))
                {
                    filteredBooks.Add(book);
                }
            }

            return filteredBooks;
        }


        //delelte book 
        public void DeleteBooks(int index)
        {
            Persistent.DeleteBook(index);
        }
        
        public void EditBooks(int index, BookModel updatedBook)
        {
            Persistent.EditBook(index, updatedBook);
        }
       

    }

}











