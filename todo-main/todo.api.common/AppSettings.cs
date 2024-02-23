namespace todo.api.common
{
    public class AppSettings
    {
        public string ApiKeyHeader { get; set; } = default!;

        public string TodoUserHeader { get; set; } = default!;

        public string? SqliteConnection { get; set; }

        public static AppSettings Instance { get; set; } = default!;
    }
}