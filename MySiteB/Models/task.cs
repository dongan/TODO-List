using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoListD.Models
{
    public class task
    {
        public int id { get; set; }
        public int priority { get; set; }
        public string TaskText { get; set; }
        public DateTime? deadline { get; set; }
        public bool isDone { get; set; }
    }

 
}