using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using log4net;

namespace Receiver
{
    class Program
    {

        static string connectionString = "HostName=IoThubZhaoz1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=aIt3EVMl+i12JHZGsrSWzbnV8fYRo5PpEz0W54xWQ5I=";

        //static string connectionString = "HostName=BurnerPilot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=4J9D1Nd3dKsIC4rG5NkrePKIvUwV+ew4Z4JUmSzDSyA=";
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;
        static ILog log = null;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("Receiver");

            Console.WriteLine("Receive messages. Ctrl-C to exit.\n");
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            CancellationTokenSource cts = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, cts.Token));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private static async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
                log.Info(string.Format("Message received. Partition: {0} Data: '{1}'", partition, data));
            }
        }
    }
}
