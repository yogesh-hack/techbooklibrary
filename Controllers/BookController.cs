using bookstoreapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookstoreapp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookstoreContext _context;

        public BookController(BookstoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            var viewModel = new BookDetailsViewModel
            {
                SelectedBook = book,
                AllBooks = _context.Books.ToList()
            };

            return View(viewModel);
        }


        public IActionResult Read(int id)
        {
            Console.WriteLine($"Read action called with id: {id}");
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found");
                return NotFound();
            }
            Console.WriteLine($"Found book: {book.Title}");
            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Book book, IFormFile CoverImage, IFormFile Pdf)
        {
            if (ModelState.IsValid)
            {
                // Generate book code
                var nextId = (_context.Books.Max(b => (int?)b.Id) ?? 0) + 1;
                book.BookCode = "BK" + nextId.ToString("D5");

                // Ensure the upload folder exists
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                // Handle Cover Image upload
                if (CoverImage != null && CoverImage.Length > 0)
                {
                    var imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(CoverImage.FileName);
                    var imagePath = Path.Combine(uploadsFolder, imageFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await CoverImage.CopyToAsync(stream);
                    }

                    // Save the relative URL
                    book.CoverImageUrl = "/uploads/" + imageFileName;
                }

                // Handle PDF upload
                if (Pdf != null && Pdf.Length > 0)
                {
                    var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(Pdf.FileName);
                    var pdfPath = Path.Combine(uploadsFolder, pdfFileName);

                    using (var stream = new FileStream(pdfPath, FileMode.Create))
                    {
                        await Pdf.CopyToAsync(stream);
                    }

                    // Save the relative URL
                    book.PdfUrl = "/uploads/" + pdfFileName;
                }

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }


        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Book book, IFormFile? CoverImageFile, IFormFile? PdfFile)
        {
            if (ModelState.IsValid)
            {
                var existingBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
                if (existingBook == null)
                    return NotFound();

                // Update text fields
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.Description = book.Description;

                // Ensure upload folder exists
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                // Handle new cover image upload
                if (CoverImageFile != null && CoverImageFile.Length > 0)
                {
                    var imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(CoverImageFile.FileName);
                    var imagePath = Path.Combine(uploadsFolder, imageFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await CoverImageFile.CopyToAsync(stream);
                    }

                    existingBook.CoverImageUrl = "/uploads/" + imageFileName;
                }

                // Handle new PDF upload
                if (PdfFile != null && PdfFile.Length > 0)
                {
                    var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(PdfFile.FileName);
                    var pdfPath = Path.Combine(uploadsFolder, pdfFileName);

                    using (var stream = new FileStream(pdfPath, FileMode.Create))
                    {
                        await PdfFile.CopyToAsync(stream);
                    }

                    existingBook.PdfUrl = "/uploads/" + pdfFileName;
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Book updated successfully!";
                return RedirectToAction("Index");
            }

            return View(book);
        }



        // POST: Book/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }


    }
}
