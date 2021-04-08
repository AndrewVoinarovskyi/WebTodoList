using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTodoList.Models;

namespace WebTodoList.Controllers
{
    [Route("api/WebTodoLists/{listId}/[controller]")]
    [ApiController]
    public class WebTodoListController : ControllerBase
    {
        private WebTodoListService service;

        public WebTodoListController(WebTodoListService service)
        {
            this.service = service;
        }

        [HttpGet]
        public List<TodoItem> GetTodoItems(int listId)
        {

            return service.Read(listId);
        }

        // [HttpGet("{id}")]
        // public ActionResult<TodoItem> GetTodoItemById(int listId, int id)
        // {
        //     return service.GetById(listId, id);
        // }

        [HttpPost("")]
        public void PostTodoItem(int listId, TodoItem model)
        {
            service.Create(listId, model);
        }

        [HttpPatch("{id}")]
        public void Update(int listId, int id, TodoItem model)
        {
            service.Update(listId, id, model);
        }

        [HttpDelete("{id}")]
        public void DeleteTodoItemById(int listId, int id)
        {
            service.Delete(id);
        }
    }
}


/////////////////////////////////////////////////////////////////////////////

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// //using TodoItem.Models;

// namespace TodoItem.Controllers
// {
//     [Route("api/TodoList/{listId}/[controller]")]
//     [ApiController]
//     public class TodoItemController : ControllerBase
//     {
//         private TodoItemService service;

//         public TodoItemController(TodoItemService service)
//         {
//             this.service = service;
//         }

//         [HttpGet]
//         public List<TodoItem> GetTodoItems(int listId)
//         {

//             return service.GetAll(listId);
//         }

//         [HttpGet("{id}")]
//         public ActionResult<TodoItem> GetTodoItemById(int listId, int id)
//         {
//             return service.GetById(listId, id);
//         }

//         [HttpPost("")]
//         public ActionResult<TodoItem> PostTodoItem(int listId, TodoItem model)
//         {
//             TodoItem todoItem = service.Create(listId, model);

//             return Created($"api/todoItem/{todoItem.Id}", todoItem);
//         }

//         [HttpPut("{id}")]
//         public  TodoItem PutTodoItem(int listId, int id, TodoItem model)
//         {

//             return service.Change(listId, id, model);
//         }

//         [HttpDelete("{id}")]
//         public List<TodoItem> DeleteTodoItemById(int listId, int id)
//         {


//             return service.Delete(listId, id);
//         }
//     }
// }