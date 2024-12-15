using MassTransit;
using MassTransit.Configuration;
using MasstransitQ.Notification.Consumers;
using MasstransitQ.Shared.Extensions;
using MasstransitQ.Shared.Settings;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();

builder.Services.Configure<MassTransitSettings>(builder.Configuration.GetSection("MassTransitSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<OrderPaymentReceivedEventConsumers>();
    x.RegisterConsumer<SendEmailCommandConsumer>();
    x.RegisterConsumer<SendSmsCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<MassTransitSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });

        cfg.RegisterQueue<OrderPaymentReceivedEventConsumers>(context, massTransitSettings.QueueName, typeof(OrderPaymentReceivedEventConsumers));
        cfg.RegisterQueue<SendEmailCommandConsumer>(context, massTransitSettings.QueueName, typeof(SendEmailCommandConsumer));
        cfg.RegisterQueue<SendSmsCommandConsumer>(context, massTransitSettings.QueueName, typeof(SendSmsCommandConsumer));

    });
});


var host = builder.Build();
host.Run();
