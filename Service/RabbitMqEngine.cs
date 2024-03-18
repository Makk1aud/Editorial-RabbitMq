using Contracts;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class RabbitMqEngine : IRabbitMqEngine
    {
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMqEngine(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void SendToQueue<T>(string exchangeType, string exchangeName, string routingKey, T item)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            var basicProperties = channel.CreateBasicProperties();

            channel.BasicPublish(new PublicationAddress(exchangeType, exchangeName, routingKey),
                basicProperties,
                JsonSerializer.SerializeToUtf8Bytes(item));
        }
    }
}
