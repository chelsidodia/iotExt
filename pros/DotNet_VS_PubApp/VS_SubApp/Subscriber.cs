using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace VS_SubApp
{
    class Subscriber
    {
        static async Task Main(string[] args)
        {
            //Use MQTTnet 3.1.2    Version Specific bcz framework 3.1

            var mqttFactory = new MqttFactory();
            var client = mqttFactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                            .WithClientId(Guid.NewGuid().ToString())
                            .WithTcpServer("test.mosquitto.org", 1883)
                            .WithCleanSession()
                            .Build();

            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("Connected to the broker successfully");
                var topicFilter = new TopicFilterBuilder()
                                        .WithTopic("kruti1010")
                                        .Build();
                await client.SubscribeAsync(topicFilter);
            });

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("Disconnected from the broker successfully");
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine($"Received Message -{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            });

            await client.ConnectAsync(options);

            Console.ReadLine();

            await client.DisconnectAsync();
        }
    }
}
