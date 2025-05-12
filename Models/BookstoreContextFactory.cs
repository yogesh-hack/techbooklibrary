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
            optionsBuilder.UseSqlServer("Data Source=103.120.176.21;Integrated Security=False;Database=tridente_kamini;User ID=tridente_kamini;Password=kamini@123;Connect Timeout=15;Encrypt=False;Packet Size=4096;");

            return new BookstoreContext(optionsBuilder.Options);
        }
    }
}
