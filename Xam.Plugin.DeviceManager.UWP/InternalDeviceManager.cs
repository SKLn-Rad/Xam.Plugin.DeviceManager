using System;
using System.Threading;
using Windows.Devices.Input;
using Xam.Plugin.DeviceManager.Enumerations;
using Xam.Plugin.DeviceManager.Interface;
using Xam.Plugin.DeviceManager.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static Xam.Plugin.DeviceManager.DeviceManager;

[assembly: Dependency(typeof(InternalDeviceManager))]
namespace Xam.Plugin.DeviceManager.UWP
{
    [Preserve]
    public class InternalDeviceManager : IDeviceManager
    {
        #region events and delegates
        public event DeviceStatusChanged OnDeviceChanged;
        #endregion

        #region properties
        TouchCapabilities _touchCapabilities;
        MouseCapabilities _mouseCapabilities;
        KeyboardCapabilities _keyboardCapabilities;

        int _knownTouchCapability = 0;
        int _knownKeyboardCapability = 0;
        int _knownMouseCapability = 0;

        Timer _pollTimer;
        static TimeSpan _pollingFrequency = TimeSpan.FromSeconds(5);
        #endregion

        #region methods
        public static void ForceLinkerPreservation()
        {
            var now = DateTime.Now;
        }

        public static void SetPollingFrequency(TimeSpan pollingFrequency)
        {
            _pollingFrequency = pollingFrequency;
        }

        public void InitializeComponent()
        {
            _touchCapabilities = new TouchCapabilities();
            _mouseCapabilities = new MouseCapabilities();
            _keyboardCapabilities = new KeyboardCapabilities();

            // Start Polling
            _pollTimer = new Timer(e => PollDevices(), null, TimeSpan.Zero, _pollingFrequency);
        }

        void PollDevices()
        {
            if (_touchCapabilities.TouchPresent != _knownTouchCapability)
            {
                _knownTouchCapability = _touchCapabilities.TouchPresent;
                OnDeviceChanged?.Invoke(DeviceType.TouchScreen, _knownTouchCapability == 1);
            }

            if (_mouseCapabilities.MousePresent != _knownMouseCapability)
            {
                _knownMouseCapability = _mouseCapabilities.MousePresent;
                OnDeviceChanged?.Invoke(DeviceType.Mouse, _knownMouseCapability == 1);
            }

            if (_keyboardCapabilities.KeyboardPresent != _knownKeyboardCapability)
            {
                _knownKeyboardCapability = _keyboardCapabilities.KeyboardPresent;
                OnDeviceChanged?.Invoke(DeviceType.Keyboard, _knownKeyboardCapability == 1);
            }
        }
        #endregion
    }
}
