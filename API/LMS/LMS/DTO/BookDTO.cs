using LMS.Entities;

namespace LMS.DTO
{
    public class BookDTO
    {
        public string Tittle { get; set; }  
        public string Author { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
      public Book ToBook()
        {
            return new Book
            {
                Tittle = this.Tittle,
                Author = this.Author,
                Quantity = this.Quantity,
                Category = this.Category
            };
        }
    }
}
