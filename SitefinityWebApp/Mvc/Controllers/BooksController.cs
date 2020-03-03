using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Books", SectionName = "Feather samples", Title = "Books")]
    public class BooksController : Controller
    {

        [RelativeRoute("{page:int:min(1)?}")]
        public ActionResult Index(int? page)
        {
            IEnumerable<Book> items = BooksController._library;
            if (page.HasValue)
                items = items.Skip((page.Value - 1) * BooksController.PageSize);
            items = items.Take(BooksController.PageSize);

            var viewModel = new BooksViewModel(items, (int)Math.Ceiling(BooksController._library.Count / (double)BooksController.PageSize), page ?? 1);

            return this.View(viewModel);
        }

        public JsonResult Points(int? page)
        {
            IEnumerable<int> points = BooksController._library.Select(Book => Book.Points);

            if (page.HasValue)
                points = points.Skip((page.Value - 1) * BooksController.PageSize);
            points = points.Take(BooksController.PageSize);
            return this.Json(points, JsonRequestBehavior.AllowGet);
        }
        private const int PageSize = 5;

        private static readonly List<Book> _library = new List<Book>(10)
        {
            new Book("Beatrix Potter", "The Tale Of Peter Rabbit"),
            new Book("Julia Donaldson", "The Gruffalo"),
            new Book("Michael Rosen", "We're Going on a Bear Hunt"),
            new Book("Judith Kerr", "The Tiger Who Came to Tea"),
            new Book("AA Milne", "Winnie the Pooh"),
            new Book("Enid Blyton", "The Enchanter Wood"),
            new Book("Jill Murphy", "The Worst Witch"),
            new Book("Roald Dahl", "Charlie and the Chocolate Factory"),
            new Book("Jacqueline Wilson", "The Story of Tracy Beaker"),
            new Book("Michelle Magorian", "Goodnight Mister Tom"),
        };
    }
}