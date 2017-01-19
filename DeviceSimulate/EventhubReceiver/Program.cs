using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;

namespace EventhubReceiver
{
    class Program
    {
        #region msdn
        static string eventHubName = "burnereventhub";
        static string connectionString = "Endpoint=sb://burnerpilot.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1g+AHHwLhiGIa6BfWhzpw7kPaK5oFTsjO//27vLuUIs=";
        #endregion

        #region dev
        //static string eventHubName = "messagedatahub";
        //static string connectionString = "Endpoint=sb://ap-evnt-apgateway-use-dev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2O8PdyjQ9qTakTJVFRZGwNkaR2h9fLik6gX2yRxHGGc=";
        #endregion
        static void Main(string[] args)
        {
            string storageAccountName = "burnerpilot";
            string storageAccountKey = "fS7QfGg1uVJ7m7F6glUBSWcHJh26TiU8w8/tez7LnhJbo4Nuk9J87coIs+8DasKSAVGSRZ/0Vuh27YkX1t5mHA==";
            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            #region dev
            //string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=apstapgatewayusedev;AccountKey=62Eo+z9yLvRtP1nVDoVjweQH/o8IljGv+QbHBhG6KuFBh5cEQWLdxUcZQrDjZRloUEGYJ7QFygaTUuJOFGWPBg==";

            #endregion
            string eventProcessorHostName = Guid.NewGuid().ToString();
            string leaseName = eventProcessorHostName;
            EventProcessorHost eventProcessorHost 
                = new EventProcessorHost(eventProcessorHostName, eventHubName, 
                            EventHubConsumerGroup.DefaultGroupName, connectionString, storageConnectionString,
                            leaseName);
            Console.WriteLine("Registering EventProcessor...");
            var options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
