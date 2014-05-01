using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoListD.Models;
using System.Windows;


namespace ToDoListD.Controllers
{
    public class HomeController : Controller
    {
        private TasksDBContext _db = new TasksDBContext();

        private static int itemId;

        private static int taskPriority;

        private void reassignment()
        {
            var xz = (from task in _db.tasks
                      orderby task.priority ascending
                      select task).ToList();

            int prt = 1;

            foreach (task tsk in xz)
            {
                tsk.priority = prt;
                prt = prt + 2;
            }

            _db.SaveChanges();

            xz = (from task in _db.tasks
                      orderby task.priority descending
                      select task).ToList();

            int max;
            
            try
            {
                max = xz[0].priority;


                foreach (task tsk in xz)
                {
                    if (max < tsk.priority) max = tsk.priority;
                }
            }

            catch (ArgumentOutOfRangeException)
            {
                max = 1;
            }


            taskPriority = max;
        }

        public HomeController()
        {
            reassignment();
        }

        public ActionResult Index()
        {
            var tasks = (from task in _db.tasks
                         orderby task.priority descending
                         select task).ToList();

            ViewBag.tasks = tasks;
                      
        
            return View();
        }

     
        [HttpPost]
        public ActionResult editTasks(string addTask, string stateTextBox, string idTextBox)
        {
            reassignment();

            switch (stateTextBox)
            {
                case "addTask":
                    {
                        task t = new task();
                        t.TaskText = addTask;
                        t.priority = ++taskPriority;

                        try
                        {
                            _db.tasks.Add(t);
                            _db.SaveChanges();
                        }
                        catch
                        {
                            taskPriority--;
                            return PartialView("error");
                        }


                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("addTask",tasks);
                    }
                case "deleteTask":
                    {
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }


                        foreach (task tsk in _db.tasks)
                        {
                            if (tsk.id == id)
                                _db.tasks.Remove(tsk);
                        }
                        _db.SaveChanges();

                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();
                                                                        
                        return PartialView("deleteTask",tasks);
                    } 
                case "editTask":
                    {
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }

                        ViewBag.id = id;
                        itemId = id;
                       

                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("editTask",tasks);
                    }
                case "editTaskText":
                    {
                        foreach (task tsk in _db.tasks)
                        {
                            
                            if (tsk.id == itemId)
                            {
                                tsk.TaskText = addTask;
                            }
                        }

                        _db.SaveChanges();
                                               
                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("addTask",tasks);
                    }
                case "increasePriority":
                    {
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }

                        foreach (task tsk in _db.tasks)
                        {
                            if (tsk.id == id)
                            {
                                tsk.priority = tsk.priority + 3;
                            }
                        }

                        _db.SaveChanges();

                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("addTask",tasks);
                    }
                case "decreasePriority":
                    {
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }

                        foreach (task tsk in _db.tasks)
                        {
                            if (tsk.id == id)
                            {
                                tsk.priority = tsk.priority - 3;
                            }
                        }

                        _db.SaveChanges();

                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("addTask", tasks);
                   } 
                default: {
                    return PartialView();
                };
            }
                     

            
        }

        
    }
}
