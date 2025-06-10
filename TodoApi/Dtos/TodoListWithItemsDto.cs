namespace TodoApi.Dtos
{
    public class TodoListWithItemsDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? EndDate { get; set; }
        public List<TodoItemDto> Items { get; set; } = new();
    }
}
