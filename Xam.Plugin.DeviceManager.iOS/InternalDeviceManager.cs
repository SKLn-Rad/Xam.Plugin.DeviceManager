using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xam.Plugin.DeviceManager.iOS;
using Xamarin.Forms;
using Xam.Plugin.DeviceManager.Interface;
using static Xam.Plugin.DeviceManager.DeviceManager;
using ExternalAccessory;
using Xam.Plugin.DeviceManager.Enumerations;

[assembly: Dependency(typeof(InternalDeviceManager))]
namespace Xam.Plugin.DeviceManager.iOS
{
    [Preserve]
    public class InternalDeviceManager : IDeviceManager
    {
        #region events and delegates
        public event DeviceStatusChanged OnDeviceChanged;
        #endregion

        #region properties
        #endregion

        #region methods
        public static void ForceLinkerPreservation()
        {
            var now = DateTime.Now;
        }

        public void InitializeComponent()
        {
            // Always TS support
            OnDeviceChanged?.Invoke(DeviceType.TouchScreen, true);

            // Never M support
            OnDeviceChanged?.Invoke(DeviceType.Mouse, false);

            // Sometimes KB support? This is not supported... yet!
            OnDeviceChanged?.Invoke(DeviceType.Keyboard, false);
        }
        #endregion
    }
}