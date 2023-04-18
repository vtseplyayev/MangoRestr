using System;
using System.Threading.Tasks;

namespace Mango.Services.OrderAPI.Messaging
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}
