using System;
using System.Messaging;

namespace ConsoleReceiver
{
    class Program
    {
        private const string QueuePath = @".\Private$\Test";

        static void Main(string[] args)
        {
            Console.WriteLine("RECEIVER");

            if (MessageQueue.Exists(QueuePath))
            {
                ReadMessages();
            }

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        static void ReadMessages()
        {
            using (var messageQueue = new MessageQueue(QueuePath))
            {
                var messages = messageQueue.GetAllMessages();

                foreach (var message in messages)
                {
                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    Console.WriteLine($"\n -- Received Message: {message.Body}");
                }

                messageQueue.Purge();

                Console.WriteLine("Press R to Refresh");
                var response = Console.ReadKey();

                if (response.KeyChar.ToString().ToUpper() == "R")
                {
                    ReadMessages();
                }
            }
        }
    }
}
