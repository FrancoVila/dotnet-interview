namespace TodoApi.Dtos
{
    public class CreateTodoItemDto
    {
        public string Description { get; set; } = string.Empty;
        public long TodoListId { get; set; }
    }
}