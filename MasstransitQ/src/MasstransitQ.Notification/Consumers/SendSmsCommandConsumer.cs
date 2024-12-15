using MassTransit;
using MasstransitQ.Shared.Models.Notification.Commands;

namespace MasstransitQ.Notification.Consumers
{
    public class SendSmsCommandConsumer : IConsumer<SendSmsCommand>
    {
        private readonly ILogger<SendSmsCommandConsumer> _logger;

        public SendSmsCommandConsumer(ILogger<SendSmsCommandConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<SendSmsCommand> context)
        {
            // Business flow

            _logger.LogInformation("Sent sms Success");

            return Task.CompletedTask;
        }
    }
}
