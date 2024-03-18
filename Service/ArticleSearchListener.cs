using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class ArticleSearchListener : BackgroundService, IServiceScopeFactory
    {
        private IArticleRepository _articleRepository;
        private IConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ArticleSearchListener(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            CreateScope();
        }
        
        public IServiceScope CreateScope()
        {
            var scope = _serviceScopeFactory.CreateScope();
            _articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();
            _connectionFactory = scope.ServiceProvider.GetRequiredService<IConnectionFactory>();
            return scope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (_, args) =>
            {
                var substring = JsonSerializer.Deserialize<string>(args.Body.ToArray()); 
                var articles = await _articleRepository.SearchEngineArticleAsync(substring);
                channel.BasicAck(args.DeliveryTag, false);
            };

            channel.BasicConsume(consumer, "editorial-search", consumerTag: "article-search-tag");
        }
    }
}
