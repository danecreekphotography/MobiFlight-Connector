using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MobiFlight.InputConfig
{
    public class MSFS2020InputEventAction : InputAction, ICloneable
    {
        public new const String Label = "MSFS 2020 InputEvent";
        public new const String CacheType = "SimConnect";
        public const String TYPE = "MSFS2020InputEventAction";
        public String Hash;
        public double Value;

        public override object Clone()
        {
            return new MSFS2020InputEventAction
            {
                Hash = Hash,
                Value = Value
            };
        }

        public override void ReadXml(XmlReader reader)
        {
            Hash = reader["hash"];
            // Huge hack, should be safer than this using tryparse
            Value = Double.Parse(reader["value"]);
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("type", getType());
            writer.WriteAttributeString("hash", Hash);
            writer.WriteAttributeString("value", Value.ToString());
        }

        protected virtual String getType()
        {
            return TYPE;
        }

        public override void execute(CacheCollection cacheCollection, InputEventArgs e, List<ConfigRefValue> configRefs)
        {
            cacheCollection.simConnectCache.SetInputEvent(Hash, Value);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is MSFS2020InputEventAction &&
                Value == (obj as MSFS2020InputEventAction).Value &&
                Hash == (obj as MSFS2020InputEventAction).Hash;
        }
    }
}
