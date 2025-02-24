namespace ToDoApp.Client.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public int SortOrder { get; set; }
    }
}
