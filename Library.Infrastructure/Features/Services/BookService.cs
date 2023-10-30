using Library.Application;
using Library.Application.Features.Training;
using Library.Domain.Entities;
using Library.Domain.Repositories;
using Library.Infrastructure.Features.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Features.Services
{
    public class BookService:IBookService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public BookService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public void CreateBook(string name,double price)
        {
            if (_unitOfWork.Books.IsDuplicateName(name, null))
                throw new DuplicateNameException("Book name is duplicate");

            Book book = new Book()
            {
                Name = name,
                Price = price
            };
            _unitOfWork.Books.Add(book);
            _unitOfWork.Save();
            
        }

        public Book GetBook(Guid id)
        {
            return _unitOfWork.Books.GetById(id);
        }
        public IList<Book> GetBooks()
        {
            return _unitOfWork.Books.GetAll();
        }

        public async Task<(IList<Book> records, int total, int totalDisplay)> GetPagedBooksAsync(
            int pageIndex, int pageSize, string searchText, string orderBy)
        {
           
              var result =await _unitOfWork.Books.GetTableDataAsync(
                    x => x.Name.Contains(searchText), orderBy,
                    pageIndex, pageSize);

                return result;
           
        }

        public void UpdateBook(Guid id, string name, double price)
        {
            if (_unitOfWork.Books.IsDuplicateName(name, id))
                throw new DuplicateNameException("Book name is duplicate");

            Book book = _unitOfWork.Books.GetById(id);
            book.Name = name;
            book.Price = price;

            
            _unitOfWork.Save();

        }

        public void DeleteBook(Guid id)
        {
            _unitOfWork.Books.Remove(id);
            _unitOfWork.Save();
        }

    }
}
