using System;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartAPI.Messaging
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}
