using MobiFlight.Properties;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MobiFlight.MQTT
{
    public class MQTTServerSettings
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        
        [XmlIgnore]
        public string Password { get; set; }

        /// <summary>
        /// Loads the settings from the app Settings object.
        /// </summary>
        /// <returns>The MQTT server settings</returns>
        public static MQTTServerSettings Load()
        {
            var config = Settings.Default.MqttServerConfig;

            if (String.IsNullOrEmpty(config))
            {
                return new MQTTServerSettings()
                {
                    Port = 1883
                };
            }

            var serializer = new XmlSerializer(typeof(MQTTServerSettings));
            var reader = new StringReader(config);
            return (MQTTServerSettings)serializer.Deserialize(reader);
        }

        /// <summary>
        /// Saves the MQTT settings to the app Settings object.
        /// </summary>
        public void Save()
        {
            var serializer = new XmlSerializer(typeof(MQTTServerSettings));
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            serializer.Serialize(writer, this);
            var s = writer.ToString();
            Settings.Default.MqttServerConfig = s;
        }
    }
}
