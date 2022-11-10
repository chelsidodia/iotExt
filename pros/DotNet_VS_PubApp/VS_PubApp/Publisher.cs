using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Threading.Tasks;

namespace VS_PubApp
{
    class Publisher
    {
        static async Task Main(string[] args)
        {
            //Use MQTTnet 3.1.2    Version Specific bcz framework 3.1
            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                            .WithClientId(Guid.NewGuid().ToString())
                            .WithTcpServer("test.mosquitto.org",1883)
                            .WithCleanSession()
                            .Build();

            client.UseConnectedHandler(e =>
            {
                Console.WriteLine("Connected to the broker successfully");
            });

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("Disconnected from the broker successfully");
            });

            await client.ConnectAsync(options);

            Console.WriteLine("Press a key for publishing a message");

            Console.ReadLine();

            await publishMessageAsync(client);

            await client.DisconnectAsync();

        }

        private static async Task publishMessageAsync(IMqttClient client)
        {
            var messagePayload = "Hii, Welcome!!";
            var message = new MqttApplicationMessageBuilder()
                            .WithTopic("kruti1010")
                            .WithPayload(messagePayload)
                            .WithAtLeastOnceQoS()
                            .Build();

            if (client.IsConnected)
            {
                await client.PublishAsync(message);
            }
        }
    }
}
