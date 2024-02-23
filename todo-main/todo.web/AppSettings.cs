namespace todo.web
{
    public class AppSettings
    {
        public static AppSettings? Instance { get; set; }

        public string ApiKey { get; set; } = default!;

        public string ApiKeyHeader { get; set; } = default!;

        public string ApiUrl { get; set; } = default!;

        public string TodoUserHeader { get; set; } = default!;

        public string DefaultUser { get; set; } = default!;

        public int SessionIdleTimeoutMinutes { get; set; } = 20;
    }
}
