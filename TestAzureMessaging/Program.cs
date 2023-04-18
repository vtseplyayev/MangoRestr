using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace TestAzureMessaging
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string connectionString = "Endpoint=sb://mangobus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cY59LRgk2Fi5cTxrbTm8sWgOHYmCInh0x+ASbAVe4jI=";
            string checkOutMessage = "checkoutmessagetopic";
            string subscription = "mangoOrderSubscription";

            ServiceBusClient client;
            ServiceBusProcessor processor;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            client = new ServiceBusClient(connectionString, clientOptions);

            processor = client.CreateProcessor(checkOutMessage, subscription);

            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;

                processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();
                Console.WriteLine("\nStopping the receiver...");
                processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                processor.DisposeAsync();
                client.DisposeAsync();
            }
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
