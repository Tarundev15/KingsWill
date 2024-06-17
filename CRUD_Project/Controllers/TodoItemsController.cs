// Controllers/TodoItemsController.cs
using CRUD_Project.Data;
using CRUD_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {

            var todoItems = await _context.TodoItems.ToListAsync();
            var todoItemsDto = todoItems.Select(item => new TodoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                IsComplete = item.IsComplete,
                AssociationData = !item.IsComplete ? GetAssociationData() : null
            }).ToList();
            //return await _context.TodoItems.ToListAsync();
            return Ok(todoItemsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            var todoItemDtos = await _context.TodoItems
                .Select(item => new TodoItemResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                })
                .ToListAsync();

            if (todoItem == null)
            {
                return NotFound();
            }
            //var todoItemDto = new TodoItemDto
            //{
            //    Id = todoItem.Id,
            //    Title = todoItem.Title,
            //    IsComplete = todoItem.IsComplete,
            //    AssociationData = !todoItem.IsComplete ? GetAssociationData() : null
            //};

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        // Method to get association data
        private List<AssociationDataDto> GetAssociationData()
        {
            return new List<AssociationDataDto>
            {
                new AssociationDataDto { UserType = "Customer", UserSubType = "Weekly", UserTypeId = "10" },
                new AssociationDataDto { UserType = "Employee", UserSubType = "Monthly", UserTypeId = "11" },
                new AssociationDataDto { UserType = "Vender", UserSubType = "Partner", UserTypeId = "22" }
            };
        }

    }
}
