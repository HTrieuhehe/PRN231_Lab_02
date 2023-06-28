using System.ComponentModel.DataAnnotations;

namespace ODataBookStore.Models
{
    public class Address
    {
        [Key]
        public string City { get; set; }
        public string Street { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }

    }

}
