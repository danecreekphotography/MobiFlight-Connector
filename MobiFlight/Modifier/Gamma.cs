using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MobiFlight.Modifier
{
    public class Gamma : ModifierBase
    {
        private readonly System.Globalization.CultureInfo serializationCulture = new System.Globalization.CultureInfo("en");
        private int maxIn = 255;
        private int maxOut = 255;
        private double gamma = 2.8;

        public override void ReadXml(XmlReader reader)
        {
            if (reader["active"] != null)
                Active = bool.Parse(reader["active"]);
            if (reader["maxIn"] != null)
                maxIn = int.Parse(reader["maxIn"]);
            if (reader["maxOut"] != null)
                maxOut = int.Parse(reader["maxOut"]);
            if (reader["gamma"] != null)
                gamma = double.Parse(reader["gamma"]);
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("gamma");
            writer.WriteAttributeString("active", Active.ToString());
            writer.WriteAttributeString("maxIn", maxIn.ToString());
            writer.WriteAttributeString("maxOut", maxOut.ToString());
            writer.WriteAttributeString("gamma", gamma.ToString());
            writer.WriteEndElement();
        }

        public override object Clone()
        {
            var Clone = new Gamma
            {
                Active = Active,
                maxIn = maxIn,
                maxOut = maxOut,
                gamma = gamma
            };
            return Clone;
        }

        public override bool Equals(object obj)
        {
            return
                obj != null && obj is Gamma &&
                this.Active == (obj as Gamma).Active &&
                this.maxIn == (obj as Gamma).maxIn &&
                this.maxOut == (obj as Gamma).maxOut &&
                this.gamma== (obj as Gamma).gamma;
        }

        public override ConnectorValue Apply(ConnectorValue value, List<ConfigRefValue> configRefs)
        {
            if (!Active)
            {
                return value;
            }

            // Apply the gamma correction. Math comes from https://learn.adafruit.com/led-tricks-gamma-correction/the-longer-fix.
            var correctedValue = Math.Pow(value.Float64 / maxIn, gamma) * maxOut + 0.5;

            Log.Instance.log($"Gamma correcting {value.Float64} to {correctedValue}", LogSeverity.Debug);

            // Send out the corrected value.
            value.Float64 = correctedValue;
            return value;
        }
    }
}
