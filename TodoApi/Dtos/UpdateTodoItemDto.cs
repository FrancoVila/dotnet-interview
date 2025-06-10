namespace TodoApi.Dtos
{
    public class UpdateTodoItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}