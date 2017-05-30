using static Xam.Plugin.DeviceManager.DeviceManager;

namespace Xam.Plugin.DeviceManager.Interface
{
    public interface IDeviceManager
    {
        void InitializeComponent();
        event DeviceStatusChanged OnDeviceChanged;
    }
}
