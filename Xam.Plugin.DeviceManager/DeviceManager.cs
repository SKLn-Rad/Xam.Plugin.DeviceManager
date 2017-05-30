using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.DeviceManager.Enumerations;
using Xam.Plugin.DeviceManager.Interface;
using Xamarin.Forms;

namespace Xam.Plugin.DeviceManager
{
    public class DeviceManager : BindableObject
    {

        #region delegates and events
        public delegate void DeviceStatusChanged(DeviceType device, bool connected);
        public static event DeviceStatusChanged OnDeviceChanged;
        #endregion

        #region properties
        public readonly BindableProperty DevicesProperty = BindableProperty.Create(nameof(Devices), typeof(ISet<DeviceType>), typeof(DeviceManager), new HashSet<DeviceType>());
        public readonly BindableProperty KeyboardConnectedProperty = BindableProperty.Create(nameof(KeyboardConnected), typeof(bool), typeof(DeviceManager), false);
        public readonly BindableProperty MouseConnectedProperty = BindableProperty.Create(nameof(MouseConnected), typeof(bool), typeof(DeviceManager), false);
        public readonly BindableProperty TouchScreenConnectedProperty = BindableProperty.Create(nameof(TouchScreenConnected), typeof(bool), typeof(DeviceManager), false);
        #endregion

        #region construction
        public static DeviceManager Instance => _instance ?? (_instance = new DeviceManager());
        static DeviceManager _instance;
        DeviceManager() { }
        #endregion

        #region properties
        IDeviceManager _internalDeviceManager;
        #endregion

        #region autoproperties
        IDeviceManager InternalDeviceManager
        {
            get { return _internalDeviceManager; }
            set { _internalDeviceManager = value; }
        }

        public ISet<DeviceType> Devices
        {
            get { return (ISet<DeviceType>) GetValue(DevicesProperty); }
            set { SetValue(DevicesProperty, value); }
        }

        public bool KeyboardConnected
        {
            get { return (bool) GetValue(KeyboardConnectedProperty); }
            set { SetValue(KeyboardConnectedProperty, value); }
        }

        public bool MouseConnected
        {
            get { return (bool)GetValue(MouseConnectedProperty); }
            set { SetValue(MouseConnectedProperty, value); }
        }

        public bool TouchScreenConnected
        {
            get { return (bool)GetValue(TouchScreenConnectedProperty); }
            set { SetValue(TouchScreenConnectedProperty, value); }
        }
        #endregion

        #region methods
        public void Initialize()
        {
            // Obtain
            InternalDeviceManager = DependencyService.Get<IDeviceManager>();

            // Validate
            if (InternalDeviceManager == null)
            {
                Debug.WriteLine("Device manager failed to initialize...");
                return;
            }

            // Register
            InternalDeviceManager.OnDeviceChanged += HandleDeviceChanged;

            // Initialize
            InternalDeviceManager.InitializeComponent();
        }

        void HandleDeviceChanged(DeviceType device, bool connected)
        {
            Debug.WriteLine(string.Format("Device {0} status set to {1}.", device, connected));

            // Update General
            if (connected)
                Devices.Add(device);
            else if (Devices.Contains(device))
                Devices.Remove(device);

            // Update Specific
            switch (device)
            {
                case DeviceType.Keyboard:
                    KeyboardConnected = connected;
                    break;
                case DeviceType.Mouse:
                    MouseConnected = connected;
                    break;
                case DeviceType.TouchScreen:
                    TouchScreenConnected = connected;
                    break;
                default:
                    // Do nothing
                    break;
            }

            // Notify
            OnDeviceChanged?.Invoke(device, connected);
        }
        #endregion
    }
}
