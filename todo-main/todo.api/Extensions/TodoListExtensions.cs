using todo.models;
using todo.data.Entities;

namespace todo.api.Extensions
{
    public static class TodoListExtensions
    {
        public static TodoListDetails AsDetails(this List entity) =>
            new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Items = entity.Items.Select(item => new TodoItem
                {
                    Id = item.Id,
                    IsComplete = item.IsComplete,
                    Label = item.Label
                }).ToArray()
            };

        public static TodoList AsModel(this List entity) =>
            new()
            {
                Id = entity.Id,
                Name = entity.Name
            };

        public static TodoItem AsModel(this Item entity) =>
            new()
            {
                Id = entity.Id,
                IsComplete = entity.IsComplete,
                Label = entity.Label
            };
    }
}
