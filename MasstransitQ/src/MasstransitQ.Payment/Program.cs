using MassTransit;
using MassTransit.Configuration;
using MasstransitQ.Payment;
using MasstransitQ.Payment.Consumers;
using MasstransitQ.Shared.Extensions;
using MasstransitQ.Shared.Models.Payment.Command;
using MasstransitQ.Shared.Settings;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();


builder.Services.Configure<MassTransitSettings>(builder.Configuration.GetSection("MassTransitSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<CreditCardPaymentCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<MassTransitSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });

        cfg.RegisterQueue<CreditCardPaymentCommandConsumer>(context, massTransitSettings.QueueName, typeof(CreditCardPaymentCommand));

    });
});

var host = builder.Build();
host.Run();
