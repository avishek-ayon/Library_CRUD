using Library.Application.Features.Training.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Features.Training.Repositories
{
    public class BookRepository:Repository<Book,Guid>,IBookRepository
    {
        public BookRepository(IApplicationDbContext context):base((DbContext)context) { }

        public async Task<(IList<Book> records, int total, int totalDisplay)> GetTableDataAsync(
           Expression<Func<Book, bool>> expression,
            string orderBy, int pageIndex, int pageSize)
        {
            return await GetDynamicAsync(expression,orderBy,null,pageIndex,pageSize,true);
        }

        public bool IsDuplicateName(string name,Guid? id)
        {
            int? existingBookCount= null;
            if(id.HasValue)
            {
                existingBookCount=GetCount(x=>x.Name==name && x.Id !=id.Value);
            }
            else
            {
                existingBookCount = GetCount(x => x.Name == name);
            }
            return existingBookCount > 0;
        }
    }
}
