using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRabbitMqEngine
    {
        void SendToQueue<T>(string exchangeType, string exchangeName, string routingKey, T item);
    }
}
