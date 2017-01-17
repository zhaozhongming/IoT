using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using log4net;

namespace SendCloudToDevice
{
    class Program
    {
        static ServiceClient serviceClient;
        //static string connectionString = "HostName=BurnerPilot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=4J9D1Nd3dKsIC4rG5NkrePKIvUwV+ew4Z4JUmSzDSyA=";
        static string connectionString = "HostName=IoThubZhaoz1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=aIt3EVMl+i12JHZGsrSWzbnV8fYRo5PpEz0W54xWQ5I=";

        static ILog log = null;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("SendCloudToDevice");

            Console.WriteLine("Send Cloud-to-Device message\n");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            Console.WriteLine("Press any key to send a C2D message.");
            Console.ReadLine();
            SendCloudToDeviceMessageAsync().Wait();
            Console.ReadLine();


            //tst.SendCloudToDeviceMessageAsync(0).Wait();
        }

        private async static Task SendCloudToDeviceMessageAsync()
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            //await serviceClient.SendAsync("rogue1", commandMessage).ConfigureAwait(false);
            await serviceClient.SendAsync("Raspberry1", commandMessage).ConfigureAwait(false);
        }
    }
}
