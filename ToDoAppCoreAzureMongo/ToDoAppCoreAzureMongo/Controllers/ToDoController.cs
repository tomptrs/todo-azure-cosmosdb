using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace ToDoAppCoreAzureMongo.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        IDataRepository repo;
        public ToDoController(IDataRepository _repo)
        {
            repo = _repo;
        }

        // GET: api/Todo
        [HttpGet("[action]")]
        public int GetTodoItems()
        {
            return 10;
        }
        [HttpGet("[action]")]
        public IEnumerable<ToDo> GetAll()
        {
            return repo.GetItemsAsync();
        }

        /*
         * 
         * Furthermore, we need to get the cat, we need to insert from the 
         * body of the request. For that we add a parameter called cat to 
         * the method and mark it with the “FromBody”-attribute. The body 
         * of the request will then be automatically extracted, parsed and 
         * passed as a parameter to our method.
         * 
         */
        // POST: api/Todo
        [HttpPost("[action]")]
        public void PostToDoItem([FromBody]ToDo item)
        {
          
            repo.CreateItemAsync(item);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}