namespace ProductService.Services {
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using ProductService.Configuration;
    using ProductService.Events.Contracts;
    using ProductService.Policies;
    using ProductService.Services.Abstract;

    public class BusService : IBusService {
        private readonly ILogger<BusService> _logger;
        public BusService (ILogger<BusService> logger) {
            _logger = logger ??
                throw new ArgumentNullException (nameof (logger));
        }

        public async Task PublishEvent<T> (T @event, string connectionString, string topicName) where T : IIntegrationEvent {
            ITopicClient topicClient = null;
            try {
                //Create Topic Client
                topicClient = new TopicClient (connectionString, topicName);

                //Create msg from @event
                var msg = new Message (Encoding.UTF8.GetBytes (JsonConvert.SerializeObject (@event)));

                //Publish msg to a Topic
                await topicClient.SendAsync (msg);
            } catch (Exception ex) {
                //Log - Error while publishing msg to Topic
                _logger.LogError ($"Error in publishing event# {nameof(@event)} with id# {@event.Id} to Topic {topicName}, exception - {ex}");
                throw;
            } finally {
                //Close Topic Client
                await topicClient.CloseAsync ();
            }
        }

        public Task SendMessage<T> (T @msg, string connectionString, string queueName) {
            throw new NotImplementedException ();
        }
    }
}