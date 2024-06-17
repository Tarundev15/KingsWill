
using System.ComponentModel.DataAnnotations;

namespace CRUD_Project.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsComplete { get; set; }

        public DateTime DueDate { get; set; }

        public int Priority { get; set; }
    }

    public class TodoItemResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }

    }
}
