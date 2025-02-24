using System.ComponentModel.DataAnnotations;

namespace ToDoApp.API.Models
{
    public class ToDoItem
    {
        public int Id { get; set; } // Unique ID for each task

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = String.Empty;// Task description (must not exceed 255 characters)

        public bool IsDone { get; set; } // Status: true (done) or false (pending)

        public int SortOrder { get; set; } // Order for sorting
    }
}
