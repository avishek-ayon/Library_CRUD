using Autofac;
using Library.Application.Features.Training;
using Library.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Areas.Admin.Models
{
    public class BookUpdateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Range(0, 50000, ErrorMessage = "Price should be between 0 & 50000")]
        public double Price { get; set; }

        private IBookService _bookService;

        public BookUpdateModel() { }

        public BookUpdateModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _bookService = scope.Resolve<IBookService>();
        }

        internal void Load(Guid id)
        {
            Book book=_bookService.GetBook(id);
            Id= book.Id;
            Name= book.Name;
            Price= book.Price;
        }
        internal void UpdateBook()
        {
            if (!string.IsNullOrWhiteSpace(Name) && Price >= 0)
            {
                _bookService.UpdateBook(Id,Name, Price);
            }
        }
    }
}
