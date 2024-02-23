using todo.api.Services;
using todo.api.test.Util;
using todo.data;
using todo.data.Entities;

namespace todo.api.test
{
    public class BaseTest : IDisposable
    {
        protected TestTodoContext _context = default!;

        protected TodoServices _services = default!;

        protected TodoUser _todoUser = default!;

        public BaseTest()
        {
            _context = new TestTodoContext();
            _context.Seed();
            _services = new TodoServices(_context);
            _todoUser = _services.UserServices.Get("Amanda").Result;
        }

        public void Dispose() => _context.Dispose();
    }
}
