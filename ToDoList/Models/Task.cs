using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class Task
    {
        public int id { get; set; }
        public int priority { get; set; }
        public string task { get; set; }
    }
}