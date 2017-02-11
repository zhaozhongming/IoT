using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using log4net;
using Common;

namespace DeciveSimulate
{
    class Program
    {
        static DeviceClient deviceClient;
        #region personal
        //static string iotHubUri = "IoThubZhaoz1.azure-devices.net";
        //static string deviceKey = "aGSneKO7wnGmA04KqvzSImRMSfJnLFtQ0uoJA7IWH7U=";
        //static string deviceIdDefined = "Raspberry1";
        #endregion

        #region msdn
        //static string iotHubUri = "BurnerPilot.azure-devices.net";
        ////static string deviceKey = "I08yZQxiPPAe30L2mXFGxsspYxR6obMsBjmvS/uSldw=";
        ////static string deviceIdDefined = "rogue1";

        //static string deviceKey = "XMnkymHy6LWf9PZH1yRQf8SmkEEK04tkGxNGPvVQAoQ=";
        //static string deviceIdDefined = "zhaoz1";
        #endregion

        #region dev
        static string iotHubUri = "ap-Iot-APGateway-USE-Dev.azure-devices.net";
        //static string deviceKey = "X9XI62RaRjSoVghhQkRalHIqkzwoNTwLpShtWX6XFR4=";
        //static string deviceIdDefined = "zhaoz1";

        static string deviceKey = "uZv0UuPO3N4capCOUOTfBEmULnWMK/n97I1yGXYgKeA=";
        static string deviceIdDefined = "marvin1";
        #endregion

        private static InputDTO[] testDataDic
         = new InputDTO[] { new InputDTO { BpcUid = 50, WinUid = 12 }
                            , new InputDTO { BpcUid = 50, WinUid = 55}
                            , new InputDTO { BpcUid = 81, WinUid = 10}
                            , new InputDTO { BpcUid = 78, WinUid = 1}
                            , new InputDTO { BpcUid = 78, WinUid = 2}
                            , new InputDTO { BpcUid = 63, WinUid = 2}
                            , new InputDTO { BpcUid = 179, WinUid = 1}
                            , new InputDTO { BpcUid = 212, WinUid = 2}
                            , new InputDTO { BpcUid = 50, WinUid = 6}
                            , new InputDTO { BpcUid = 50, WinUid = 9} };


        static ILog log = null;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("Device");

            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceIdDefined, deviceKey));

            ReceiveC2dAsync();

            while (true)
            {
                SendDeviceToCloudMessagesAsync(GenerateRandomData());
                Task.Delay(1000).Wait();

                Console.ReadLine();
            }
        }

        private static InputDTO GenerateRandomData()
        {
            Random rand = new Random();

            InputDTO inputDTO = new InputDTO
            {
                BpcUid = testDataDic[rand.Next(0,9)].BpcUid,
                WinUid = testDataDic[rand.Next(0, 9)].WinUid,
                TimeStamp = DateTime.Now.ToUniversalTime(),
                Ch01 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch02 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch03 = Convert.ToSingle(rand.NextDouble() * 2),
                Ch04 = Convert.ToSingle(rand.NextDouble() * 0.1),
                Ch05 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch06 = Convert.ToSingle(rand.NextDouble() * 5),
                Ch07 = Convert.ToSingle(rand.NextDouble() * 20),
                Ch08 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch09 = Convert.ToSingle(rand.NextDouble() * 0.4),
                Ch10 = Convert.ToSingle(rand.NextDouble() * 40),
                Ch11 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch12 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch13 = Convert.ToSingle(rand.NextDouble()),
                Ch14 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch15 = Convert.ToSingle(rand.NextDouble() * 4),
                Ch16 = Convert.ToSingle(rand.NextDouble())
            };

            return inputDTO;
        }

     
    private static async void SendDeviceToCloudMessagesAsync(InputDTO inputDTO)
        {
            //var message = new Common.Message();
            //message.LocationID = bpc;
            //message.NodeNumber = win;
            //message.Ch0 = currentWindSpeed;
            //message.Ch1 = 1;
            //message.Ch2 = 2;
            //message.Ch3 = 43;
            //message.Ch4 = 3214;
            //message.Ch5 = 3;
            //message.Ch6 = 4;
            //message.Ch7 = 6;
            //message.Ch8 = 5;
            //message.Ch9 = 2435;
            //message.Ch10 = 544;
            //message.Ch11 = 45;
            //message.Ch12 = 44;
            //message.Ch13 = 44;
            //message.Ch14 = 33;
            //message.Ch15 = 2;
            //message.UTCTimestamp = DateTime.UtcNow;

            //var messageString = @"{'Location ID': " + bpc + ",'Node number': " + win + ",'Ch0':" + currentWindSpeed  + ",'Ch1': 0.017,'Ch2': 16.601,'Ch3': 16.961"
            //    + ",'Ch4': -999.000,'Ch5': -999.000,'Ch6': 3.694229,'Ch7': 1.000,'Ch7': 6.975,'Ch8': -26.811"
            //    + ",'Ch9': 17.875,'Ch10': 3.677489,'Ch10': -41,'Ch11': 255,'Ch12': 255,'Ch13': 255,'Ch14': 255,'Ch15': 255,'UTC Timestamp':" + DateTime.UtcNow + "}";

            //var messageString = "{'bpc':" + bpc + ",'win':" + win + ",'time':" + DateTime.Now.ToUniversalTime() + ",'data':[6435,3310,56.06875,60.325,3300,6288,-12161,6204,3293,55.01875,59.2125,3300,6257,1805,106,1901]}";

            var messageString = JsonConvert.SerializeObject(inputDTO);
            var messageSend = new Message(Encoding.ASCII.GetBytes(messageString));

            await deviceClient.SendEventAsync(messageSend);
            Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
            
               
        }

        private static async void ReceiveC2dAsync()
        {
            while (true)
            {
                Console.WriteLine("\nReceiving cloud to device messages from service");

                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Received message: {0}", Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                Console.ResetColor();

                await deviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
