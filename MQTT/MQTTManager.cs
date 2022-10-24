using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Server;

namespace MobiFlight
{
    internal static class MQTTManager
    {
        public static MqttFactory mqttFactory;
        public static IMqttClient mqttClient;

        public static bool IsConnected
        {
            get
            {
                return mqttClient.IsConnected;
            }
        }


        public static async Task Connect()
        {
            mqttFactory = new MqttFactory();

            mqttClient = mqttFactory.CreateMqttClient();

            // Use builder classes where possible in this project.
            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("hostname").WithCredentials("username", "password").Build();

            // This will throw an exception if the server is not available.
            // The result from this message returns additional data which was sent 
            // from the server. Please refer to the MQTT protocol specification for details.
            var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            Console.WriteLine("The MQTT client is connected.");
        }

        public static async Task Publish(string topic, string payload)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            Console.WriteLine($"Published {payload} to {topic}");
        }

        public static async Task Disconnect()
        {
            // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
            // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
            var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

            await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);

            Console.WriteLine("The MQTT client is disconnected.");
        }
    }
}
