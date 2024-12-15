using MassTransit;
using MasstransitQ.Shared.Models.Notification.Commands;

namespace MasstransitQ.Notification.Consumers
{
    public class SendEmailCommandConsumer : IConsumer<SendEmailCommand>
    {
        private readonly ILogger<SendEmailCommandConsumer> _logger;

        public SendEmailCommandConsumer(ILogger<SendEmailCommandConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<SendEmailCommand> context)
        {
            // Business flow

            //throw new Exception("Provider doesn't work");

            _logger.LogInformation("Sent email Success");

            return Task.CompletedTask;
        }
    }
}
