using System.Data.Entity;

namespace LibraryApp.Models
{
    public class LibraryAppContext : DbContext
    {
        public LibraryAppContext() : base("name=LibraryAppContext")
        {
        }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<BookType> BookTypes { get; set; }

        public DbSet<Editorial> Editorials { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<BookSeller> BookSellers { get; set; }

        public System.Data.Entity.DbSet<LibraryApp.Models.OrderStatus> OrderStatus { get; set; }

        public System.Data.Entity.DbSet<LibraryApp.Models.Order> Orders { get; set; }
    }
}
