namespace MasstransitQ.Shared.Settings
{
    public class MassTransitSettings
    {
        public required string Host { get; set; }
        public required string? VirtualHost { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string QueueName { get; set; }
    }
}
