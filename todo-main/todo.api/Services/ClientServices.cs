using Microsoft.EntityFrameworkCore;
using todo.data;
using todo.data.Entities;

namespace todo.api.Services
{
    public class ClientServices
    {
        private readonly TodoContext _context;

        public ClientServices(TodoContext context) => _context = context;

        public async Task<ClientApplication?> Get(string apiKey) =>
            await _context.ClientApplications.FirstOrDefaultAsync(x => x.ApiKey == apiKey);
    }
}
