using TodoApi.Models;
public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public long TodoListId { get; set; } 
    public TodoList? TodoList { get; set; }
}