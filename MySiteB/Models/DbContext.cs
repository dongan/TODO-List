using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace ToDoListD.Models
{
    public class TasksDBContext : DbContext
    {
        public TasksDBContext() : base("TaskDB") { }
        public DbSet<task> tasks{get; set;}
       
    }
}