using LMS.Entities;
using System.Security.Claims;

namespace LMS.DTO
{
    public class BookData: BookDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public BookData(Book x)
        {
            Id = x.Id;
            Tittle = x.Tittle;
            Author = x.Author;
            Quantity = Convert.ToInt32(x.Quantity);
            Category = x. Category ;
            CreatedAt = x.CreatedAt ?? DateTime.Now;

        }

    }
}
