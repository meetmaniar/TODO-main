using todo.data;

namespace todo.api.Services
{
    public class TodoServices
    {
        private readonly TodoContext _context;

        private ClientServices? _clientServices;

        private ListServices? _listServices;

        private UserServices? _userServices;

        public ClientServices ClientServices => _clientServices ??= new ClientServices(_context);

        public ListServices ListServices => _listServices ??= new ListServices(_context);

        public UserServices UserServices => _userServices ??= new UserServices(_context);

        public TodoServices(TodoContext context) => _context = context;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
