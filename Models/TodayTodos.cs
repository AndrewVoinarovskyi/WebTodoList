using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoLists.Models
{
    public class TodayTodosDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}