using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Models;

namespace ToDoList.Models
{
    public class DashboardDto
    {
        public int Count{ get; set; }
        public List<TodayTodosDto> Dashboards { get; set; }
    }
}

