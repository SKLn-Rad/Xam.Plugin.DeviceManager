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
using Xam.Plugin.DeviceManager.Interface;
using Xamarin.Forms;
using Xam.Plugin.DeviceManager.Droid;
using Android.Hardware.Input;
using Xam.Plugin.DeviceManager.Enumerations;
using static Xam.Plugin.DeviceManager.DeviceManager;
using Xam.Plugin.DeviceManager.Droid.Listeners;

[assembly: Dependency(typeof(InternalDeviceManager))]
namespace Xam.Plugin.DeviceManager.Droid
{
    [Preserve]
    public class InternalDeviceManager : IDeviceManager
    {

        #region events and delegates
        public event DeviceStatusChanged OnDeviceChanged;
        #endregion

        #region properties
        DeviceListener _listener;
        InputManager _inputManager;
        #endregion

        #region methods
        public static void ForceLinkerPreservation()
        {
            var now = DateTime.Now;
        }

        public void InitializeComponent()
        {
            _listener = new DeviceListener(this);
            _inputManager = (InputManager) Forms.Context.GetSystemService(Context.InputService);
            _inputManager.RegisterInputDeviceListener(_listener, new Handler(Looper.MainLooper));

            RegisterDefaults();
        }

        void RegisterDefaults()
        {
            foreach (int id in _inputManager.GetInputDeviceIds())
                AddDevice(id);

            // All Android devices by spec need this
            AddDevice(DeviceType.TouchScreen);
        }

        internal DeviceType ResolveDeviceTypeFromIdentifier(int id)
        {
            switch (id)
            {
                case (int) InputSourceType.Keyboard:
                    return DeviceType.Keyboard;
                case (int) InputSourceType.Mouse:
                    return DeviceType.Mouse;
                default:
                    return DeviceType.None;
            }
        }

        void AddDevice(DeviceType device)
        {
            OnDeviceChanged?.Invoke(device, true);
        }

        void RemoveDevice(DeviceType device)
        {
            OnDeviceChanged?.Invoke(device, false);
        }

        internal void AddDevice(int id)
        {
            var device = ResolveDeviceTypeFromIdentifier(id);
            if (device == DeviceType.None)
                return;

            AddDevice(device);
        }

        internal void RemoveDevice(int id)
        {
            var device = ResolveDeviceTypeFromIdentifier(id);
            if (device == DeviceType.None)
                return;

            AddDevice(device);
        }
        #endregion
    }
}