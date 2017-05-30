using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware.Input;
using Xam.Plugin.DeviceManager.Enumerations;

namespace Xam.Plugin.DeviceManager.Droid.Listeners
{
    public class DeviceListener : Java.Lang.Object, InputManager.IInputDeviceListener
    {

        InternalDeviceManager _deviceManager;

        public DeviceListener(InternalDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        public void OnInputDeviceAdded(int deviceId) => _deviceManager.AddDevice(deviceId);
        public void OnInputDeviceRemoved(int deviceId) => _deviceManager.RemoveDevice(deviceId);

        public void OnInputDeviceChanged(int deviceId)
        {
            // Do we care about this? Will other platforms support it?
        }
    }
}