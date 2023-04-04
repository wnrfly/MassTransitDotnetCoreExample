# MassTransitDotnetCoreExample

#Install the MassTransit library for .NET Core using NuGet:


#dotnet add package MassTransit.RabbitMQ

#Write the code to configure MassTransit and RabbitMQ:



using MassTransit;
using MassTransit.RabbitMqTransport;

// Configure MassTransit and RabbitMQ
var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });
});

Write the code to publish an event to the bus:



// Publish an event to the bus
await busControl.Publish(new SomeEvent { Message = "Hello, world!" });
Console.WriteLine("Event published to the bus");

Write the code to consume events from the bus:



    // Define a consumer class
    public class SomeEventConsumer : IConsumer<SomeEvent>
    {
        public Task Consume(ConsumeContext<SomeEvent> context)
        {
            Console.WriteLine("Event received from the bus: {0}", context.Message.Message);
            return Task.CompletedTask;
        }
    }

    // Configure MassTransit to use the consumer class
    busControl.ConnectReceiveEndpoint("event_queue", endpointConfig =>
    {
        endpointConfig.Consumer<SomeEventConsumer>();
    });

Note that in the above example, the event is a simple class with a single string property. You can define more complex event classes as needed. Also, make sure to replace the RabbitMQ credentials with your own if they are different.
