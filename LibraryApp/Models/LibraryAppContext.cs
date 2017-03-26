using System.Data.Entity;

namespace LibraryApp.Models
{
    public class LibraryAppContext : DbContext
    {
        public LibraryAppContext() : base("name=LibraryAppContext")
        {
        }

        public DbSet<Writer> Writers { get; set; }

        public System.Data.Entity.DbSet<LibraryApp.Models.BookType> BookTypes { get; set; }

        public System.Data.Entity.DbSet<LibraryApp.Models.Editorial> Editorials { get; set; }

        public System.Data.Entity.DbSet<LibraryApp.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<LibraryApp.Models.Book> Books { get; set; }
    }
}
