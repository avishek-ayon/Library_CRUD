using Library.Application.Features.Training.Repositories;
using Library.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public interface IApplicationUnitOfWork:IUnitOfWork
    {
        IBookRepository Books { get; }
    }
}
