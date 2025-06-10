using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/todolists")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/todolists
        [HttpGet]
        public async Task<ActionResult<IList<TodoListWithItemsDto>>> GetTodoLists()
        {
            var lists = await _context.TodoList
                .Include(l => l.Items)
                .ToListAsync();

            var dtos = lists.Select(todoList => new TodoListWithItemsDto
            {
                Id = todoList.Id,
                Name = todoList.Name,
                EndDate = todoList.EndDate,
                Items = todoList.Items.Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted,
                    TodoListId = item.TodoListId
                }).ToList()
            }).ToList();

            return Ok(dtos);
        }

        // GET: api/todolists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListWithItemsDto>> GetTodoList(long id)
        {
            var todoList = await _context.TodoList
                .Include(l => l.Items)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (todoList == null)
                return NotFound();

            var dto = new TodoListWithItemsDto
            {
                Id = todoList.Id,
                Name = todoList.Name,
                EndDate = todoList.EndDate,
                Items = todoList.Items.Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted,
                    TodoListId = item.TodoListId
                }).ToList()
            };

            return Ok(dto);
        }

        // GET: api/todolists/{todoListId}/items
        [HttpGet("{todoListId}/items")]
        public async Task<ActionResult<IList<TodoItemDto>>> GetTodoListItems(long todoListId)
        {
            var todoList = await _context.TodoList
                .Include(l => l.Items)
                .FirstOrDefaultAsync(l => l.Id == todoListId);

            if (todoList == null)
                return NotFound($"La lista con id {todoListId} no existe.");

            var items = todoList.Items.Select(item => new TodoItemDto
            {
                Id = item.Id,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                TodoListId = item.TodoListId
            }).ToList();

            return Ok(items);
        }

        // PUT: api/todolists/5
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodoList(long id, UpdateTodoList payload)
        {
            var todoList = await _context.TodoList.FindAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            todoList.Name = payload.Name;
            await _context.SaveChangesAsync();

            return Ok(todoList);
        }


        [HttpPut("enddate/{id}")]
        public async Task<ActionResult> PutEndDate(long id)
        {
            var todoList = await _context.TodoList.FindAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            todoList.EndDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(todoList);
        }

        // POST: api/todolists
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(TodoList), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoList>> PostTodoList(CreateTodoList payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Name))
                return BadRequest("El nombre es obligatorio.");

            var todoList = new TodoList { Name = payload.Name };

            _context.TodoList.Add(todoList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoList", new { id = todoList.Id }, todoList);
        }

        // DELETE: api/todolists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoList(long id)
        {
            var todoList = await _context.TodoList.FindAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }

            _context.TodoList.Remove(todoList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoListExists(long id)
        {
            return (_context.TodoList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
