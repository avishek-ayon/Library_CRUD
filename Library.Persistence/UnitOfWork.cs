using Library.Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public abstract class UnitOfWork:IUnitOfWork
    {
        protected readonly DbContext _dbContext;
        protected UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public virtual void Dispose()=>_dbContext?.Dispose();
        public virtual void Save() => _dbContext?.SaveChanges();
    }
}
