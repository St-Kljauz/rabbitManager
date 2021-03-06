﻿using System;
using RabbitMqManager.Helpers;

namespace RabbitMqManager
{
    internal class Consumer<T> : IConsumer where T : class
    {
        public Action<T> Handling { get; }
        public string Queue { get; private set; }
        public string RoutingKey { get; private set; }

        internal Consumer(string queue, string routingKey, Action<T> handling)
        {
            Handling = handling;
        }

        //ToDo generic?
        public void Receive(object obj)
        {
            var message = DeserializeHelper<T>.Deserialize(obj.ToString());
            if (message != null)
            {
                Handling.Invoke(message);
            }
        }
    }
}
