using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Server;
using static SimpleSolutions.Usb.ArcazeCommand;
using System.Runtime.CompilerServices;

namespace MobiFlight.MQTT
{
    internal static class MQTTManager
    {
        public static string Serial = "MQTTServer";
        public static MqttFactory mqttFactory;
        public static IMqttClient mqttClient;

        private static readonly Dictionary<string, string> outputCache = new Dictionary<string, string>();

        /// <summary>
        /// Compares the specified serial to the serial used to identify MQTT Server configurations.
        /// </summary>
        /// <param name="serial">The serial to verify.</param>
        /// <returns>True if the serial is an MQTT Server configuration.</returns>
        public static bool IsMQTTSerial(string serial)
        {
            return serial == Serial;
        }

        public static async Task Connect()
        {
            //var settings = new MQTTServerSettings()
            //{
            //    Address = "192.168.1.172",
            //    Port = 1883,
            //    Username = "mqtt"
            //};

            //settings.Save();

            var settings = MQTTServerSettings.Load();

            mqttFactory = new MqttFactory();

            mqttClient = mqttFactory.CreateMqttClient();

            // Use builder classes where possible in this project.
            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(settings.Address, settings.Port).WithCredentials(settings.Username, "password").Build();

            // This will throw an exception if the server is not available.
            // The result from this message returns additional data which was sent 
            // from the server. Please refer to the MQTT protocol specification for details.
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            Log.Instance.log($"MQTT: Connected to {settings.Address}:{settings.Port}.", LogSeverity.Debug);
        }

        public static async Task Publish(string topic, string payload)
        {
            if (!mqttClient.IsConnected)
                return;

            // Don't spam MQTT server if the payload is the same as last time for the topic.
            if (outputCache.ContainsKey(topic) && outputCache[topic] == payload)
                return;

            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            outputCache[topic] = payload;

            Log.Instance.log($"MQTT: Published {payload} to {topic}.", LogSeverity.Debug);
        }

        public static async Task Disconnect()
        {
            // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
            // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
            var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

            await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);

            Log.Instance.log($"MQTT: Disconnected from server.", LogSeverity.Debug);
        }
    }
}
