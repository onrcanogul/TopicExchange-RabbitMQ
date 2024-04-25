using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://yejguzib:NuvHgMGxrkfxK8SHh3VRyAYLi3B3ENf1@shark.rmq.cloudamqp.com/yejguzib");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.Write("Topic(Listener): ");
string topic = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
};

Console.Read();



