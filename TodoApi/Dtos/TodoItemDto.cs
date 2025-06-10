namespace TodoApi.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public long TodoListId { get; set; }
    }
}