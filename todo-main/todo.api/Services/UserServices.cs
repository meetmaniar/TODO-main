using Microsoft.EntityFrameworkCore;
using todo.data;
using todo.data.Entities;

namespace todo.api.Services
{
    public class UserServices
    {
        private readonly TodoContext _context;

        public UserServices(TodoContext context) => _context = context;

        public async Task<TodoUser> Get(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
            return user ?? await Create(username);
        }

        public async Task<TodoUser> Create(string username)
        {
            var user = new TodoUser { UserName = username };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
