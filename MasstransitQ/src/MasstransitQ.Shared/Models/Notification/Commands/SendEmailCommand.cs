using MassTransit;

namespace MasstransitQ.Shared.Models.Notification.Commands
{
    [EntityName("notification.sendemailcommand")]
    public record SendEmailCommand(List<string> ToEmailAddresses, string Subject, string Body)
    {
        public List<string> ToEmailAddresses { get; protected set; } = ToEmailAddresses;
        public string Subject { get; protected set; } = Subject;
        public string Body { get; protected set; } = Body;
    }
}
