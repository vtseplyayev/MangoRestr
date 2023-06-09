﻿using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Mango.Services.OrderAPI.Repository;
using Mango.Services.OrderAPI.Models;

namespace Mango.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer: IAzureServiceBusConsumer
    {
        private readonly OrderRepository orderRepository;

        private ServiceBusProcessor busProcessor;

        private readonly IMessageBus messageBus;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IMessageBus messageBus)
        {
            this.orderRepository = orderRepository;
            this.messageBus = messageBus;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var client = new ServiceBusClient(Config.ServiceBusURL, clientOptions);

            busProcessor = client.CreateProcessor(Config.CheckOutMessageTopic, Config.Subscription);
        }

        public async Task Start()
        {
            busProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            busProcessor.ProcessErrorAsync += ErrorHandler;

            await busProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await busProcessor.StopProcessingAsync();
            await busProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDTO checkoutHeaderDTO = JsonConvert.DeserializeObject<CheckoutHeaderDTO>(body);

            OrderHeader orderHeader = new OrderHeader()
            {
                UserId = checkoutHeaderDTO.UserId,
                FirstName = checkoutHeaderDTO.FirstName,
                LastName = checkoutHeaderDTO.LastName,
                OrderDetails = new List<OrderDetails>(),
                CouponCode = checkoutHeaderDTO.CouponCode,
                DiscountTotal = checkoutHeaderDTO.DiscountTotal,
                Email = checkoutHeaderDTO.Email,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDTO.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDTO.Phone,
                PickupDateTime = checkoutHeaderDTO.PickupDateTime
            };

            foreach (var item in checkoutHeaderDTO.CartDetails)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId = item.ProductId,
                    ProductName = item.CartProduct.Name,
                    Price = item.CartProduct.Price,
                    Count = item.Count
                };

                orderHeader.CartTotalItems += item.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await orderRepository.AddOrder(orderHeader);

            PaymentRequestMessage paymentRequestMessage = new()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
            };

            try
            {
                await messageBus.PublishMessage(paymentRequestMessage, Config.OrderPaymentProcessTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
