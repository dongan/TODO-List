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
        //создаем контекст БД
        private TasksDBContext _db = new TasksDBContext();

        private static int itemId;
        private static int taskPriority;

        //функция необходимая для нормальной работы изменения приоритета
        private void reassignment()
        {
            // извлекаем все записи в порядке возростания
            var tasks = (from task in _db.tasks
                      orderby task.priority ascending
                      select task).ToList();

            int prt = 1;

            //переназначаем приоритеты
            foreach (task tsk in tasks)
            {
                tsk.priority = prt;
                prt = prt + 2; // +2 чтобы не было заданий с 1 и тем же приоритетом(при изменении приоритета он увеличивается на 3)
            }

            //сохраняем:)
            _db.SaveChanges();


            tasks = (from task in _db.tasks
                      orderby task.priority descending
                      select task).ToList();


            //ищем элемент с  максимальным приоритетом
            int max;
            
            try
            {
                max = tasks[0].priority;


                foreach (task tsk in tasks)
                {
                    if (max < tsk.priority) max = tsk.priority;
                }
            }

            catch (ArgumentOutOfRangeException)
            {
                max = 1;
            }

            //сохраняем в статической переменной
            taskPriority = max;
        }

        public HomeController()
        {
            reassignment();
        }

        public ActionResult Index()
        {
            //просто возвращаем список всех тасков через ViewBag
            var tasks = (from task in _db.tasks
                         orderby task.priority descending
                         select task).ToList();

            ViewBag.tasks = tasks;
                      
        
            return View();
        }

     //функция работающая с аякс запросами
        [HttpPost]
        public ActionResult editTasks(string addTask, string stateTextBox, string idTextBox)
        {
            reassignment();
            //В зависимости от значения stateTextBox выбираем что нам делать
            switch (stateTextBox)
            {
                //собственно добавляем запись и следим чтобы во время сохранения не было ошибки
                case "addTask":
                    {
                        task t = new task();
                        t.TaskText = addTask;
                        taskPriority = taskPriority + 2;
                        t.priority = taskPriority;

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

                        //возвращаем измененный список заданий
                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("addTask",tasks);
                    }

                //удаление записи
                case "deleteTask":
                    {
                        //парсим id записи
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }

                        //находим запись и удаляем
                        foreach (task tsk in _db.tasks)
                        {
                            if (tsk.id == id)
                                _db.tasks.Remove(tsk);
                        }
                        _db.SaveChanges();

                        //возвращаем измененный список заданий
                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();
                                                                        
                        return PartialView("deleteTask",tasks);
                    } 

                //изменяем задание
                case "editTask":
                    {
                        //парсим id
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }

                        //сохраняем id для последующего сохранения изменений при нажатии confirm
                        ViewBag.id = id;
                        itemId = id;
                       
                        //возвращаем весь список заданий, но при этом таска с нашим id отобразится по другому
                        var tasks = (from task in _db.tasks
                                     orderby task.priority descending
                                     select task).ToList();

                        return PartialView("editTask",tasks);
                    }

                //здесь сохраняем изменения после редактирования пользователем задания
                case "editTaskText":
                    {
                        //находим запись и изменяем
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
                //увеличения приоритета задания
                case "increasePriority":
                    {
                        //парсим id
                        int id;

                        if (!Int32.TryParse(idTextBox, out id))
                        {
                            return View("Error");
                        }

                        //находим запись и изменяем
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

                //аналогично, находим запись, изменяем, сохраняем
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
                    return View("error");
                };
            }
                     

            
        }

        
    }
}
