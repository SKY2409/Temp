using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.API.Models;
using ToDoApp.API.Repositories;


namespace ToDoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _repository;

        public ToDoController(IToDoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/todo
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetAll()
        {
            var items = _repository.GetAll();
            return Ok(items);
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetById(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/todo
        [HttpPost]
        public ActionResult<ToDoItem> Create(ToDoItem item)
        {
            var created = _repository.Add(item);
            return CreatedAtAction(nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItem item)
        {
            if (id != item.Id)
                return BadRequest("ID in URL does not match ID in model");

            var success = _repository.Update(item);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _repository.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
