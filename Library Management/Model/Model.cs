using System;
using System.Xml.Linq;
using Library_Management.User_Management;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Library_Management.Model
{
    public class BookModel
    {
        // simple make a model/DTO
        public string Tittle { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 


    }
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public string Contact { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;




    }
}
