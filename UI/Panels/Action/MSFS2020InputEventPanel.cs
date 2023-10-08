using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MobiFlight.InputConfig;

namespace MobiFlight.UI.Panels.Action
{
    public partial class MSFS2020InputEventPanel : UserControl
    {
        public MSFS2020InputEventPanel()
        {
            InitializeComponent();
        }

        internal void syncFromConfig(InputConfig.MSFS2020InputEventAction inputAction)
        {
            if (inputAction == null)
            {
                HashTextBox.Text = string.Empty;
                ValueTextBox.Text = string.Empty;
                return;
            }

            HashTextBox.Text = inputAction.Hash;
            ValueTextBox.Text = inputAction.Value.ToString();
        }

        internal InputConfig.InputAction ToConfig()
        {
            return new MSFS2020InputEventAction()
            {
                Hash = HashTextBox.Text,
                // Giant hack, this should be TryParse().
                Value = Double.Parse(ValueTextBox.Text),
            };
        }
    }


}
