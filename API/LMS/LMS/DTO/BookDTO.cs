using LMS.Entities;

namespace LMS.DTO
{
    public class BookDTO
    {
        /// <summary>Book title</summary>
        /// <example>C# in Depth</example>
        public string Tittle { get; set; }

        /// <summary>Author of the book</summary>
        /// <example>Jon Skeet</example>
        public string Author { get; set; }

        /// <summary>Number of copies available</summary>
        /// <example>10</example>
        public int Quantity { get; set; }

        /// <summary>Category of the book</summary>
        /// <example>Programming</example>
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
