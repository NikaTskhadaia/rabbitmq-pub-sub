using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");

//var factory = new ConnectionFactory() { HostName = "localhost" };
var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") };

using (var connection = factory.CreateConnection()) 
{
    using var channel = connection.CreateModel();
    channel.QueueDeclare(queue: "my-first-queue",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" Message Received {0}", message);
    };
    channel.BasicConsume(queue: "my-first-queue",
                         autoAck: true,
                         consumer: consumer);

    Console.ReadLine();
}
