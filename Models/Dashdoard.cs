using System;
using System.Collections.Generic;
using System.Linq;
using TodoLists.Models;

namespace TodoLists.Models
{
    public class DashboardDto
    {
        public int Count{ get; set; }
        public List<TodayTodosDto> Dashboards { get; set; }
    }
}

