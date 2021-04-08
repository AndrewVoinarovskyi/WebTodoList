using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using TodoItem.Models;

namespace WebTodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebTodoListController : ControllerBase
    {
        private WebTodoListService service;

        public WebTodoListController(WebTodoListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<TodoItem> GetTodoItems()
        {

            return service.Read();
        }

        // [HttpGet("{id}")]
        // public ActionResult<TodoItem> GetTodoItemById(int listId, int id)
        // {
        //     return service.GetById(listId, id);
        // }

        [HttpPost("")]
        public void PostTodoItem(TodoItem model)
        {
            service.Create(model);
        }

        [HttpPut("{id}")]
        public void Update(TodoItem model)
        {
            service.Update(model);
        }

        [HttpDelete("{id}")]
        public void DeleteTodoItemById(int id)
        {
            service.Delete(id);
        }
    }
}