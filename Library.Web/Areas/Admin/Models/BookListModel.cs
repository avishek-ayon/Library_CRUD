using Library.Application.Features.Training;
using Library.Domain.Entities;
using Library.Infrastructure;

namespace Library.Web.Areas.Admin.Models
{
    public class BookListModel
    {
        private readonly IBookService _bookService;
        public BookListModel()
        {

        }
        public BookListModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IList<Book> GetPopularBooks()
        {
            return _bookService.GetBooks();
        }

        public async Task<object> GetPagedBooksAsync(
            DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _bookService.GetPagedBooksAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] { "name", "price" }));


            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                          record.Name,
                          record.Price.ToString(),
                          record.Id.ToString()
                        }
                      ).ToArray()
            };

        }

        internal void DeleteBook(Guid id)
        {
            _bookService.DeleteBook(id);
        }
    }
}
