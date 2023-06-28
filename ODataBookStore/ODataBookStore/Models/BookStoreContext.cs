using Microsoft.EntityFrameworkCore;

namespace ODataBookStore.Models
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get;set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Book>()
                .HasOne<Address>(b=>b.Location)
                .WithMany(a=>a.Books)
                .HasForeignKey(b=>b.LocationName);

            mb.Entity<Press>()
                .HasData(
                new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book
                },
                new Press
                {
                    Id = 2,
                    Name = "Addison-Mercedes",
                    Category = Category.Magazine
                },
                new Press
                {
                    Id = 3,
                    Name = "John-Doe",
                    Category = Category.Ebook
                }
                );
            mb.Entity<Address>()
                .HasData(
                    new Address
                    {
                        City = "HCM City",
                        Street = "D2, Thu Duc District"
                    },
                    new Address
                    {
                        City = "Ha Noi City",
                        Street = "D3, Thu Duc District"
                    },
                    new Address
                    {
                        City = "Da Nang City",
                        Street = "D1, Thu Duc District"
                    },
                    new Address
                    {
                        City = "Quy Nhon City",
                        Street = "D6, Thu Duc District"
                    }
                    );

            mb.Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = 1,
                        ISBN = "978-0-321-87758-1",
                        Title = "Essential C#5.0",
                        Author = "Mark Michaelis",
                        Price = 59.99m,
                        LocationName = "HCM City",
                        PressId =  1
                    },
                    new Book
                    {
                        Id = 2,
                        ISBN = "123-0-321-87758-1",
                        Title = "Essential C#6.0",
                        Author = "Mark Wiens",
                        Price = 49.99m,
                        LocationName =  "Ha Noi City",
                        PressId =  2
                    },
                    new Book
                    {
                        Id = 3,
                        ISBN = "234-0-321-87758-1",
                        Title = "Food Blog",
                        Author = "Michelin",
                        Price = 33.99m,
                        LocationName = "Da Nang City",
                        PressId =  2
                    },
                    new Book
                    {
                        Id = 4,
                        ISBN = "345-0-321-87758-1",
                        Title = "Don't Make Me Think",
                        Author = "Steve Krug",
                        Price = 159.99m,
                        LocationName = "Quy Nhon City", 
                        PressId =  3
                    }
                    );
        }
    }
}
