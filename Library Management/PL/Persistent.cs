using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library_Management.Model;
using Library_Management.User_Management;

namespace Library_Management.PL
{
    public class Persistent
    {
        const string USER_PATH = "E:\\Users.txt";
        const string BOOK_PATH = "E:\\books.txt";
       

        //Properties
        public List<UserModel> Users { get; private set; } = new List<UserModel>();
        public List<BookModel> Books { get; private set; } = new List<BookModel>();

        public Persistent()
        {
            LoadUsers();
            LoadBooks();
        }
        // Load all users from file into memory
        public void LoadUsers()
        {
            Users.Clear();
            if (!File.Exists(USER_PATH))
                return ;
            string[] lines = File.ReadAllLines(USER_PATH);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 6)
                {
                    Users.Add(new UserModel
                    {
                        Id = int.TryParse(parts[0], out int id) ? id : 0,                        Name = parts[1],
                        Email = parts[2],
                        Class = parts[3],
                        Contact = parts[4]
                    });
                }
            }

        }

        public void SaveAllUser()
        {
            
            List<string> lines = new List<string>();
            foreach (var user in Users)
            {
                lines.Add($"{user.Id} | {user.Name} | {user.Email} | {user.Class} | {user.Contact} | {user.CreatedAt}");
                
            }
            File.WriteAllLines(USER_PATH, lines);

        }
        public void AddUser(UserModel user)
        {
            user.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
            Users.Add(user);
            SaveAllUser();
        }

        public void Delete(int Id)
        {
            var user = Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                Users.Remove(user);
                SaveAllUser();

            }

        }

        public void Edit(int Id, UserModel updatedUser)
        {

            var user = Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Class = updatedUser.Class;
                user.Contact = updatedUser.Contact;
                SaveAllUser();
            }
        }



        //For Books

        // Load all users from file into memory.
        public void LoadBooks()
        {
            if (!File.Exists(BOOK_PATH))
                return; 

            Books.Clear(); 
            string[] lines = File.ReadAllLines(BOOK_PATH);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 4)
                {
                    Books.Add(new BookModel
                    {
                        Tittle = parts[0],
                        Author = parts[1],
                        Quantity = int.TryParse(parts[2], out int qty) ? qty : 0,
                        Category = parts[3]
                    });
                }
            }
        }

        private void SaveAllBooks()
        {
            List<string> lines = new List<string>();
            foreach (var book in Books)
            {
                lines.Add($"{book.Tittle} | {book.Author} | {book.Quantity} | {book.Category} | {book.CreatedAt}");
            }
            File.WriteAllLines(BOOK_PATH, lines);
        }
        public void AddBooks(BookModel book)
        {
            Books.Add(book);
            SaveAllBooks();
            
        }
        public void DeleteBook(int index)
        {

            if (index >= 0 && index < Books.Count)
            {
                Books.RemoveAt(index);
                SaveAllBooks();
               
            }
        }
        public void EditBook(int index, BookModel updatedBook)
        {

            if (index >= 0 && index < Books.Count)
            {
                Books[index] = updatedBook;
                SaveAllBooks();

                
            }

        }

    }
}







