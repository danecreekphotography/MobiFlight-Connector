using MobiFlight.MQTT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels.Settings
{
    public partial class MqttServerSettingsPanel : UserControl
    {
        private MQTTServerSettings settings;

        public MqttServerSettingsPanel()
        {
            InitializeComponent();
        }

        public void LoadSettings()
        {
            settings = MQTTServerSettings.Load();

            addressTextBox.Text = settings.Address;
            portTextBox.Text = settings.Port.ToString();
            usernameTextBox.Text = settings.Username;
            passwordTextBox.Text = settings.Password;
        }

        public void SaveSettings()
        {
            settings.Address = addressTextBox.Text;
            settings.Port = Convert.ToInt32(settings.Port);
            settings.Username = usernameTextBox.Text;
            settings.Password = passwordTextBox.Text;

            settings.Save();
        }

        //Only allow numbers in the port text box.
        private void portTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
