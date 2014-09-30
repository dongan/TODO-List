using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;


namespace ToDoList.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public TasksContext _db = new TasksContext();

        public ActionResult Index()
        {
            var xz = (from Task in _db.Tasks
                      select Task).ToList();

            return View(xz);
        }

    }
}
