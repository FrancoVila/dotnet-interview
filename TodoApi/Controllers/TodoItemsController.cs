using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Dtos;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // Crear un nuevo ítem en una lista
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto dto)
        {
            // Asegúrate de que dto.TodoListId sea long en el DTO
            var list = await _context.TodoList.FindAsync(dto.TodoListId);
            if (list == null)
                return NotFound($"La lista con id {dto.TodoListId} no existe.");

            var item = new TodoItem
            {
                Description = dto.Description,
                IsCompleted = false,
                TodoListId = dto.TodoListId 
            };

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            var result = new TodoItemDto
            {
                Id = item.Id,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                TodoListId = item.TodoListId 
            };

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, result);
        }

        // Obtener un ítem por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetById(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();

            return new TodoItemDto
            {
                Id = item.Id,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                TodoListId = item.TodoListId
            };
        }

        // Actualizar la descripción de un ítem
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoItemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El id de la URL y el del cuerpo no coinciden.");

            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();

            item.Description = dto.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Marcar como completado
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkAsComplete(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();

            item.IsCompleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar ítem
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}