using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://yejguzib:NuvHgMGxrkfxK8SHh3VRyAYLi3B3ENf1@shark.rmq.cloudamqp.com/yejguzib");


using IConnection connection =  factory.CreateConnection();
using IModel channel =  connection.CreateModel();

channel.ExchangeDeclare("topic-exchange-example", type: ExchangeType.Topic);

while(true)
{
    Console.Write("Message: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    Console.Write("Topic(Sender): ");
    string topic = Console.ReadLine();

    channel.BasicPublish(
        exchange: "topic-exchange-example",
        routingKey: topic,
        body: byteMessage
        );

}

Console.Read();