using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library_Managment.Models;
using Microsoft.Ajax.Utilities;
using PagedList;
using PagedList.Mvc;

namespace Library_Managment.Controllers
{
    public class HomeController : Controller
    {

        private librarymanagementEntities1 _db;

        public HomeController()
        {
            _db = new librarymanagementEntities1();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(user_tbl obj)
        {

            if (obj != null)
            {
                var checker = _db.user_tbl.Select(x => x.user_name == obj.user_name && x.password == obj.password).SingleOrDefault();
                if (checker)
                {
                    var getId = _db.user_tbl.Select(x => x.user_name == obj.user_name && x.password == obj.password).SingleOrDefault();
                    Session["User_id"] = getId;

                    return View("CreateBooks");
                }
            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult CreateBooks()
        {
            if (Session["User_id"] != null)
            {
                return View();
            }
            return View("Index");
        }

        [HttpPost]
        public JsonResult CreateBooks(create_books obj)
        {
            obj.book_user_id = Convert.ToInt32(Session["user_id"]);

            var data = new
            {
                message = "Data is created",
                link = Url.Action("Contact")
            };

            _db.create_books.Add(obj);
            _db.SaveChangesAsync();

            return Json(data);
        }


        public ActionResult Viewlist(int? page)
        {
            int pageSize = 3;
            int pageIndex = 1;

            pageIndex = page.HasValue ? Convert.ToInt32(page.Value) : 1;
            IPagedList<create_books> list = null;

            List<create_books> data = (from b in _db.create_books select b).ToList();
            //List<create_books> data = (from b in _db.create_books select b).ToList();

            list = data.ToPagedList(pageIndex, pageSize); // pageSize => how many components should be there on View Page.

            return View(list);
        }


        [HttpGet]
        public ActionResult Viewmore(int id, string name)
        {
            var res = new crud_book
            {
                book_name = name,
                cr_id = id
            };

            return View(res);
        }

        [HttpPost]
        public JsonResult Viewmore(crud_book obj)
        {
            try
            {
                using (var _db = new librarymanagementEntities1()) // Use your actual DbContext class
                {
                    // Add the new record to the crud_book table
                    _db.crud_book.Add(obj);

                    // Save the changes to the database
                    _db.SaveChanges();
                }

                // Return a success message
                return Json(new { success = true, message = "Data created successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception and return an error message
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        public ActionResult UpdateBookDetail(int id, string name)
        {
            var update = _db.create_books.SingleOrDefault(b => b.book_id == id && b.book_name == name);

            return View(update);
        }

        [HttpPost]
        public ActionResult UpdateBookDetail(int book_id, string book_name, string book_description)
        {
            var bookToUpdate = _db.create_books.SingleOrDefault(b => b.book_id == book_id);
            if (bookToUpdate != null)
            {
                // Update only the fields that you want to change
                bookToUpdate.book_name = book_name;
                bookToUpdate.book_description = book_description;

                // Save the changes back to the database
                _db.SaveChanges();
                return Json(new { success = true, message = "Book details updated successfully!" });
            }
            return Json(new { success = false, message = "Book not found!" });
        }
    }
}