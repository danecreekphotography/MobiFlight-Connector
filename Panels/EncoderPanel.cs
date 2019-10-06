﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MobiFlight;
using MobiFlight.InputConfig;
using MobiFlight.Panels.Group;

namespace MobiFlight.Panels
{
    public partial class EncoderPanel : UserControl
    {
        InputConfig.EncoderInputConfig _config;

        public EncoderPanel()
        {
            InitializeComponent();
            onLeftActionTypePanel.ActionTypeChanged += new ActionTypePanel.ActionTypePanelSelectHandler(onPressActionTypePanel_ActionTypeChanged);
            onLeftFastActionTypePanel.ActionTypeChanged += new ActionTypePanel.ActionTypePanelSelectHandler(onPressActionTypePanel_ActionTypeChanged);
            onRightActionTypePanel.ActionTypeChanged += new ActionTypePanel.ActionTypePanelSelectHandler(onPressActionTypePanel_ActionTypeChanged);
            onRightFastActionTypePanel.ActionTypeChanged += new ActionTypePanel.ActionTypePanelSelectHandler(onPressActionTypePanel_ActionTypeChanged);
        }

        // On Press Action
        private void onPressActionTypePanel_ActionTypeChanged(object sender, String value)
        {
            Control panel = null;
            Panel owner = onLeftActionConfigPanel;
            bool isLeft = ((sender as ActionTypePanel) == onLeftActionTypePanel) || ((sender as ActionTypePanel) == onLeftFastActionTypePanel);
            bool isFast = ((sender as ActionTypePanel) == onLeftFastActionTypePanel) || ((sender as ActionTypePanel) == onRightFastActionTypePanel);

            if (isLeft && isFast) owner = onLeftFastActionConfigPanel;
            else if (!isLeft)
            {
                owner = onRightActionConfigPanel;
                if (isFast) owner = onRightFastActionConfigPanel;
            }

            owner.Controls.Clear();
            switch (value)
            {
                case "FSUIPC Offset":
                    panel = new Panels.Group.FsuipcConfigPanel();
                    (panel as Panels.Group.FsuipcConfigPanel).setMode(false);

                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.Group.FsuipcConfigPanel).syncFromConfig(_config.onLeft as FsuipcOffsetInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.Group.FsuipcConfigPanel).syncFromConfig(_config.onLeftFast as FsuipcOffsetInputAction);
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.Group.FsuipcConfigPanel).syncFromConfig(_config.onRight as FsuipcOffsetInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.Group.FsuipcConfigPanel).syncFromConfig(_config.onRightFast as FsuipcOffsetInputAction);
                    break;

                case "Key":
                    panel = new KeyboardInputPanel();
                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.KeyboardInputPanel).syncFromConfig(_config.onLeft as KeyInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.KeyboardInputPanel).syncFromConfig(_config.onLeftFast as KeyInputAction);                        
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.KeyboardInputPanel).syncFromConfig(_config.onRight as KeyInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.KeyboardInputPanel).syncFromConfig(_config.onRightFast as KeyInputAction);
                    break;

                case "Event ID":
                    panel = new EventIdInputPanel();
                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.EventIdInputPanel).syncFromConfig(_config.onLeft as EventIdInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.EventIdInputPanel).syncFromConfig(_config.onLeftFast as EventIdInputAction);
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.EventIdInputPanel).syncFromConfig(_config.onRight as EventIdInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.EventIdInputPanel).syncFromConfig(_config.onRightFast as EventIdInputAction);
                    break;

                case PmdgEventIdInputAction.Label:
                    panel = new PmdgEventIdInputPanel();
                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.PmdgEventIdInputPanel).syncFromConfig(_config.onLeft as PmdgEventIdInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.PmdgEventIdInputPanel).syncFromConfig(_config.onLeftFast as PmdgEventIdInputAction);
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.PmdgEventIdInputPanel).syncFromConfig(_config.onRight as PmdgEventIdInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.PmdgEventIdInputPanel).syncFromConfig(_config.onRightFast as PmdgEventIdInputAction);
                    break;

                case "Jeehell DataPipe":
                    panel = new JeehellInputPanel();
                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.JeehellInputPanel).syncFromConfig(_config.onLeft as JeehellInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.JeehellInputPanel).syncFromConfig(_config.onLeftFast as JeehellInputAction);
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.JeehellInputPanel).syncFromConfig(_config.onRight as JeehellInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.JeehellInputPanel).syncFromConfig(_config.onRightFast as JeehellInputAction);
                    break;

                case LuaMacroInputAction.Label:
                    panel = new LuaMacroInputPanel();
                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.LuaMacroInputPanel).syncFromConfig(_config.onLeft as LuaMacroInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.LuaMacroInputPanel).syncFromConfig(_config.onLeftFast as LuaMacroInputAction);
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.LuaMacroInputPanel).syncFromConfig(_config.onRight as LuaMacroInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.LuaMacroInputPanel).syncFromConfig(_config.onRightFast as LuaMacroInputAction);
                    break;

                case RetriggerInputAction.Label:
                    panel = new LuaMacroInputPanel();
                    if (isLeft && !isFast && _config != null && _config.onLeft != null)
                        (panel as Panels.RetriggerInputPanel).syncFromConfig(_config.onLeft as RetriggerInputAction);
                    else if (isLeft && isFast && _config != null && _config.onLeftFast != null)
                        (panel as Panels.RetriggerInputPanel).syncFromConfig(_config.onLeftFast as RetriggerInputAction);
                    else if (!isLeft && !isFast && _config != null && _config.onRight != null)
                        (panel as Panels.RetriggerInputPanel).syncFromConfig(_config.onRight as RetriggerInputAction);
                    else if (!isLeft && isFast && _config != null && _config.onRightFast != null)
                        (panel as Panels.RetriggerInputPanel).syncFromConfig(_config.onRightFast as RetriggerInputAction);
                    break;
            }

            if (panel != null)
            {
                panel.Padding = new Padding(0, 0, 0, 0);
                panel.Width = owner.Width;
                owner.Controls.Add(panel);
                panel.Height = owner.Height - 3;
            }
        }

        public void syncFromConfig(InputConfig.EncoderInputConfig config)
        {
            if (config == null) return;

            _config = config;

            if (_config.onLeft != null)
            {
                onLeftActionTypePanel.syncFromConfig(_config.onLeft);
            }

            if (_config.onLeftFast != null)
            {
                onLeftFastActionTypePanel.syncFromConfig(_config.onLeftFast);
            }

            if (_config.onRight != null)
            {
                onRightActionTypePanel.syncFromConfig(_config.onRight);
            }

            if (_config.onRightFast != null)
            {
                onRightFastActionTypePanel.syncFromConfig(_config.onRightFast);
            }
        }

        public void ToConfig(InputConfig.EncoderInputConfig config)
        {
            // for on press check the action type
            if (onLeftActionTypePanel.ActionTypeComboBox.SelectedItem != null)
            {
                switch (onLeftActionTypePanel.ActionTypeComboBox.SelectedItem.ToString())
                {
                    case "FSUIPC Offset":
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as FsuipcConfigPanel).ToConfig();
                        break;
                    case "Key":
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as KeyboardInputPanel).ToConfig();
                        break;
                    case "Event ID":
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as EventIdInputPanel).ToConfig();
                        break;
                    case PmdgEventIdInputAction.Label:
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as PmdgEventIdInputPanel).ToConfig();
                        break;
                    case "Jeehell DataPipe":
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as JeehellInputPanel).ToConfig();
                        break;
                    case LuaMacroInputAction.Label:
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as LuaMacroInputPanel).ToConfig();
                        break;
                    case RetriggerInputAction.Label:
                        config.onLeft = (onLeftActionConfigPanel.Controls[0] as RetriggerInputPanel).ToConfig();
                        break;
                    default:
                        config.onLeft = null;
                        break;
                }
            }

            // for on fast press check the action type
            if (onLeftFastActionTypePanel.ActionTypeComboBox.SelectedItem != null)
            {
                switch (onLeftFastActionTypePanel.ActionTypeComboBox.SelectedItem.ToString())
                {
                    case "FSUIPC Offset":
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as FsuipcConfigPanel).ToConfig();
                        break;
                    case "Key":
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as KeyboardInputPanel).ToConfig();
                        break;
                    case "Event ID":
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as EventIdInputPanel).ToConfig();
                        break;
                    case PmdgEventIdInputAction.Label:
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as PmdgEventIdInputPanel).ToConfig();
                        break;
                    case "Jeehell DataPipe":
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as JeehellInputPanel).ToConfig();
                        break;
                    case LuaMacroInputAction.Label:
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as LuaMacroInputPanel).ToConfig();
                        break;

                    case RetriggerInputAction.Label:
                        config.onLeftFast = (onLeftFastActionConfigPanel.Controls[0] as RetriggerInputPanel).ToConfig();
                        break;
                    default:
                        config.onLeftFast = null;
                        break;
                }
            }

            if (onRightActionTypePanel.ActionTypeComboBox.SelectedItem != null)
            {
                switch (onRightActionTypePanel.ActionTypeComboBox.SelectedItem.ToString())
                {
                    case "FSUIPC Offset":
                        config.onRight = (onRightActionConfigPanel.Controls[0] as FsuipcConfigPanel).ToConfig();
                        break;
                    case "Key":
                        config.onRight = (onRightActionConfigPanel.Controls[0] as KeyboardInputPanel).ToConfig();
                        break;
                    case "Event ID":
                        config.onRight = (onRightActionConfigPanel.Controls[0] as EventIdInputPanel).ToConfig();
                        break;
                    case PmdgEventIdInputAction.Label:
                        config.onRight = (onRightActionConfigPanel.Controls[0] as PmdgEventIdInputPanel).ToConfig();
                        break;
                    case "Jeehell DataPipe":
                        config.onRight = (onRightActionConfigPanel.Controls[0] as JeehellInputPanel).ToConfig();
                        break;
                    case LuaMacroInputAction.Label:
                        config.onRight = (onRightActionConfigPanel.Controls[0] as LuaMacroInputPanel).ToConfig();
                        break;
                    case RetriggerInputAction.Label:
                        config.onRight = (onRightActionConfigPanel.Controls[0] as RetriggerInputPanel).ToConfig();
                        break;
                    default:
                        config.onRight = null;
                        break;
                }
            }

            if (onRightFastActionTypePanel.ActionTypeComboBox.SelectedItem != null)
            {
                switch (onRightFastActionTypePanel.ActionTypeComboBox.SelectedItem.ToString())
                {
                    case "FSUIPC Offset":
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as FsuipcConfigPanel).ToConfig();
                        break;
                    case "Key":
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as KeyboardInputPanel).ToConfig();
                        break;
                    case "Event ID":
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as EventIdInputPanel).ToConfig();
                        break;
                    case PmdgEventIdInputAction.Label:
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as PmdgEventIdInputPanel).ToConfig();
                        break;
                    case "Jeehell DataPipe":
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as JeehellInputPanel).ToConfig();
                        break;
                    case LuaMacroInputAction.Label:
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as LuaMacroInputPanel).ToConfig();
                        break;
                    case RetriggerInputAction.Label:
                        config.onRightFast = (onRightFastActionConfigPanel.Controls[0] as RetriggerInputPanel).ToConfig();
                        break;
                    default:
                        config.onRightFast = null;
                        break;
                }
            }
        }
    }
}
