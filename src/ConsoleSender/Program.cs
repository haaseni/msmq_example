using System;
using System.Messaging;

namespace ConsoleSender
{
    class Program
    {
        private const string QueuePath = @".\Private$\Test";

        static void Main(string[] args)
        {
            Console.WriteLine("SENDER");
            SendMessage();
        }

        static void SendMessage()
        {
            MessageQueue messageQueue;

            if (MessageQueue.Exists(QueuePath))
            {
                messageQueue = new MessageQueue(QueuePath)
                {
                    Label = "Testing Queue"
                };
            }
            else
            {
                MessageQueue.Create(QueuePath);
                messageQueue = new MessageQueue(QueuePath)
                {
                    Label = "Testing Queue"
                };
            }

            Console.WriteLine("Enter message to send:");
            var message = Console.ReadLine();

            messageQueue.Send(message);

            Console.WriteLine($"-- Sent {message}");

            SendMessage();
        }
    }
}
