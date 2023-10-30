using Autofac;
using Library.Domain.Entities;
using Library.Infrastructure;
using Library.Infrastructure.Features.Exceptions;
using Library.Web.Areas.Admin.Models;
using Library.Web.Models;
using Library.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        ILifetimeScope _scope;
        ILogger<BookController> _logger;
        public BookController(ILifetimeScope scope, ILogger<BookController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<BookListModel>();
            return View(model);
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<BookCreateModel>();
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Create(BookCreateModel model)
        {
            model.ResolveDependency(_scope);

            if(ModelState.IsValid)
            {
                try
                {
                    model.CreateBook();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new book",

                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction("Index");
                }
                catch(DuplicateNameException ex)
                {
                    _logger.LogError(ex,ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,

                        Type = ResponseTypes.Danger
                    });
                   
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating book",

                        Type = ResponseTypes.Danger
                    });
                }
            }

           
            return View(model);
        }
        public IActionResult Update(Guid id)
        {
            var model = _scope.Resolve<BookUpdateModel>();
            model.Load(id);
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Update(BookUpdateModel model)
        {
            model.ResolveDependency(_scope);

            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdateBook();


                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException ex)
                {
                    _logger.LogError(ex, ex.Message);
               
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");
                }
            }
            return View(model);
        }


        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<BookListModel>();


            if (ModelState.IsValid)
            {
                try
                {
                    model.DeleteBook(id);
                    
                }
               
                catch (Exception e)
                {
                    _logger.LogError(e, "Server Error");

                }
            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetBooks()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<BookListModel>();

            var data = await model.GetPagedBooksAsync(dataTablesModel);
            return Json(data);
        }
    }
}
