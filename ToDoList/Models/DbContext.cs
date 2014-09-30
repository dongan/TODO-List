using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ToDoList.Models;

namespace ToDoList.Models
{
    public class TasksContext : DbContext
    {
        public TasksContext() : base("ToDoListDB") { }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}