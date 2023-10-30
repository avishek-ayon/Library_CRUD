using Autofac;
using Library.Application.Features.Training;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Areas.Admin.Models
{
    public class BookCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required, Range(0, 50000, ErrorMessage = "Price should be between 0 & 50000")]
        public double Price { get; set; }

        private IBookService _bookService;

        public BookCreateModel() { }

        public BookCreateModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _bookService = scope.Resolve<IBookService>();
        }
        internal void CreateBook()
        {
            if (!string.IsNullOrWhiteSpace(Name) && Price >= 0)
            {
                _bookService.CreateBook(Name, Price);
            }
        }

    }
}
