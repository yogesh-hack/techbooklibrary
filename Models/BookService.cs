using bookstoreapp.Models;
using Microsoft.EntityFrameworkCore;

public class BookService
{
    private readonly BookstoreContext _context;

    public BookService(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            throw new KeyNotFoundException("Book not found");
        return book;
    }
}
