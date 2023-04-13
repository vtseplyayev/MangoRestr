using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private string connectionString = "Endpoint=sb://mangobus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cY59LRgk2Fi5cTxrbTm8sWgOHYmCInh0x+ASbAVe4jI=";

        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            ISenderClient client = new TopicClient(connectionString, topicName);

            var JsonMessage = JsonConvert.SerializeObject(message);
            var finalMessage = new Message(Encoding.UTF8.GetBytes(JsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await client.SendAsync(finalMessage);
            await client.CloseAsync();
        }
    }
}
