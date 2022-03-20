using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");

//var factory = new ConnectionFactory() { HostName = "localhost" };
var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};



using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("my-first-queue",
durable: true,
exclusive: false,
autoDelete: false,
arguments: null);



var thismessage = Console.ReadLine() ?? "Hello from docker";
var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(thismessage));



channel.BasicPublish("", "my-first-queue", null, body);

Console.ReadLine();