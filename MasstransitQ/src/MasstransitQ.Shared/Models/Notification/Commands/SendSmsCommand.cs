using MassTransit;

namespace MasstransitQ.Shared.Models.Notification.Commands
{
    [EntityName("notification.sendsmscommand")]
    public record SendSmsCommand(string phoneNumber, string Content);
}
