using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendCloudToDevice
{
    class tst
    {
        private static ServiceClient serviceClient;
        private static string connectionString = "HostName=IoThubZhaoz1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=aIt3EVMl+i12JHZGsrSWzbnV8fYRo5PpEz0W54xWQ5I=";

        public async static Task<string> SendCloudToDeviceMessageAsync(int onf)
        {
            string ret = "success";
            try
            {
                serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
                serviceClient.GetFeedbackReceiver();

                var commandMessage = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes("the action is: " + onf.ToString()));
                commandMessage.Ack = DeliveryAcknowledgement.Full;
                commandMessage.MessageId = "WeChat#1";
                await serviceClient.SendAsync("Raspberry1", commandMessage);
            }
            catch (Exception e)
            {
                ret = e.Message;
            }

            return ret;
        }
    }
}
