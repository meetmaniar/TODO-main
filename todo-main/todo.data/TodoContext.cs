using Microsoft.EntityFrameworkCore;
using todo.api.common;
using todo.data.Entities;

namespace todo.data
{
    public class TodoContext : DbContext
    {
        #region DbSets
        public DbSet<ClientApplication> ClientApplications { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<List> Lists { get; set; }

        public DbSet<TodoUser> Users { get; set; }
        #endregion

        public virtual void Seed()
        {
            Database.EnsureCreated();

            if (ClientApplications.Any()) return;

            AddRange(new object[]
            {
                new ClientApplication { Name = "todo-web", ApiKey = "test-api-key" },
                new TodoUser { Id = 1, UserName = "Amanda" },
                new TodoUser { Id = 2, UserName = "Billy" },
                new TodoUser { Id = 3, UserName = "Casey" },
                new List { Id = 1, UserId = 1, Name = "Groceries", Items = new[]
                {
                    new Item { Label = "Eggs", IsComplete = true },
                    new Item { Label = "Bacon" },
                    new Item { Label = "Bread" },
                }},
                new List { Id = 2, UserId = 1, Name = "Chores", Items = new[]
                {
                    new Item { Label = "Dishes" },
                    new Item { Label = "Laundry", IsComplete = true },
                    new Item { Label = "Weed the garden" },
                }},
                new List { Id = 3, UserId = 2, Name = "Banana bread recipe", Items = new[]
                {
                    new Item { Label = "Three overripe bananas" },
                    new Item { Label = "1.5 cups of all-purpose flour" },
                    new Item { Label = "1 tsp of salt" },
                    new Item { Label = "2 large eggs" },
                    new Item { Label = "3/4 cup of brown sugar" },
                    new Item { Label = "1 tbsp of baking soda" }
                }},
            });

            SaveChanges();

            DetachAll();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(AppSettings.Instance.SqliteConnection ?? "Data Source=:memory");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected void DetachAll()
        {
            foreach(var entry in ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
