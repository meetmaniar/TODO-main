using Microsoft.EntityFrameworkCore;
using todo.data;
using todo.data.Entities;
using todo.models;
using todo.api.Extensions;

namespace todo.api.Services
{
    public class ListServices
    {
        private readonly TodoContext _context;

        public ListServices(TodoContext context) => _context = context;

        public async Task<Item?> CreateItem(int listId, TodoItemUpdate model, TodoUser user)
        {
            var list = await GetList(listId, user);
            if (list is null) return null;

            var item = new Item { Label = model.Label };
            list.Items.Add(item);
            return item;
        }

        public async Task<List> CreateList(TodoListUpdate model, TodoUser user)
        {
            var entity = new List { UserId = user.Id, Name = model.Name };
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task DeleteItem(int id, int listId, TodoUser user)
        {
            var entity = await GetItem(id, listId, user);
            if (entity is null) return;

            _context.Remove(entity);
        }

        public async Task DeleteList(int id, TodoUser user)
        {
            var entity = await GetList(id, user);
            if (entity is null) return;

            _context.Remove(entity);
        }

        public async Task<Item?> GetItem(int id, int listId, TodoUser user) => 
            await _context.Items.FirstOrDefaultAsync(x => x.Id == id && x.ListId == listId && x.List.UserId == user.Id);

        public async Task<List?> GetList(int listId, TodoUser user) =>
            await _context.Lists.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == listId && x.UserId == user.Id);

        public async Task<IEnumerable<List>> GetLists(TodoUser user) =>
            await _context.Lists.Where(x => x.UserId == user.Id).ToListAsync();

        public async Task<Item?> UpdateItem(int id, int listId, TodoItemUpdate model, TodoUser user)
        {
            var entity = await GetItem(id, listId, user);
            if (entity is null) return null;

            entity.Label = model.Label;
            entity.IsComplete = model.IsComplete;
            return entity;
        }

        public async Task<List?> UpdateList(int id, TodoListUpdate model, TodoUser user)
        {
            var entity = await GetList(id, user);
            if (entity is null) return null;

            entity.Name = model.Name;
            return entity;
        }
    }
}
