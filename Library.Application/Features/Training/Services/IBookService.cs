using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Training
{
    public interface IBookService
    {
        void CreateBook(string name, double price);
        void DeleteBook(Guid id);

        Book GetBook(Guid id);
        public IList<Book> GetBooks();
        Task<(IList<Book> records, int total, int totalDisplay)>GetPagedBooksAsync(
            int pageIndex, int pageSize,string searchText,string orderBy);

        void UpdateBook(Guid id,string name, double price);
    }
}
