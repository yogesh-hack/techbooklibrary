using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookstoreapp.Models
{
    [Table("Books", Schema = "tridente_kamini")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string? Author { get; set; }
        public string? BookCode { get; set; }

        public string? Description { get; set; }

        public string? CoverImageUrl { get; set; }

        public string? PdfUrl { get; set; }
    }

    public class BookDetailsViewModel
    {
        public Book? SelectedBook { get; set; }
        public List<Book>? AllBooks { get; set; }
    }


}
