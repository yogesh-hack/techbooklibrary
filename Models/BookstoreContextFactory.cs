using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace bookstoreapp.Models
{
    public class BookstoreContextFactory : IDesignTimeDbContextFactory<BookstoreContext>
    {
        public BookstoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookstoreContext>();

            // Use the same connection string as in appsettings.json
            optionsBuilder.UseSqlServer("yourconneciotn_string");

            return new BookstoreContext(optionsBuilder.Options);
        }
    }
}
