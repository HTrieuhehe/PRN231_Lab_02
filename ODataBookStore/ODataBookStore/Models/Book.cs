using System.ComponentModel.DataAnnotations;

namespace ODataBookStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string LocationName { get; set; }
        public virtual Address Location { get; set; }
        public int PressId { get; set; }
        public virtual Press Press { get; set; }
    }
}
