First configure MassTransit to use RabbitMQ as the message broker. 
You can do this by installing the MassTransit.RabbitMQ NuGet package and configuring the bus

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(new Uri("rabbitmq://localhost"), h =>
    {
        h.Username("guest");
        h.Password("guest");
    });
});



public class ScheduledMessage
{
    public string MyMessage { get; set; }
}

#Use the IBusScheduleSendEndpoint to schedule the message to be sent at a specific time:

var scheduledTime = DateTime.Now.AddMinutes(5);
var endpoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/my_queue_name"));
await endpoint.ScheduleSend(scheduledTime, new ScheduledMessage { Message = "Ramadan Kareem" });

#To consume the scheduled message, you can use the IScheduledMessageConsumer interface provided by MassTransit. For example:

public class ScheduledMessageConsumer : IConsumer<ScheduledMessage>
{
    public async Task Consume(ConsumeContext<ScheduledMessage> context)
    {
        var message = context.Message;
        // Do something with the message...
    }
}

# //You can then register the consumer with MassTransit

busControl.ConnectConsumeToQueue("your_queue_name", x => x.Consumer<ScheduledMessageConsumer>());

one last trick should take care about 

//the "rabbitmq_delayed_message_exchange" are not enabled by default we need to enable it by 

## rabbitmq-plugins enable rabbitmq_delayed_message_exchange
using the command provided to enable rabbitmq_delayed_message_exchange which are required for delayed messages 
