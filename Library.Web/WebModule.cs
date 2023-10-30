using Autofac;
using Library.Application.Features.Training.Repositories;
using Library.Domain.Repositories;
using Library.Web.Areas.Admin.Models;

namespace Library.Web
{
    public class WebModule:Module
    {
        

        public WebModule()
        {
            
        }

   
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BookUpdateModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
