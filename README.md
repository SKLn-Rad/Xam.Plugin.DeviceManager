# Xam.Plugin.DeviceManager
Easily expose device capabilities and provide bindings for Xamarin Forms.

## Changelog
Version 1.0.0 -> Initial Implementation

## Installation
https://www.nuget.org/packages/Xam.Plugin.DeviceManager

## How to use?
```c#
public App()
{
    // Add this to your app.xaml.cs!
    DeviceManager.Instance.Initialize();
}
```

## Donations
PayPal: ryandixon1993@gmail.com
Thank you all, who support my plugins and feel free to message me if you need any custom made!

## Bindables
* DeviceManager.KeyboardConnectedProperty -> Will detect if an external keyboard is present
* DeviceManager.MouseConnectedProperty -> Will detect if an external mouse is present
* DeviceManager.TouchScreenConnectedProperty -> Will detect if an touch screen is present

## Limitations
* Apple do not provide support for external keyboard detection, so this will ALWAYS return false (for now)
* Windows do not provide events for when the device state changes, therefore the plugin will poll for this information.
```c#
public static void SetPollingFrequency(TimeSpan pollingFrequency)
{
    // You can configure the polling frequency by calling this method in InternalDeviceManager, inside the WinRT and UWP projects
    // By default this is 5 seconds.
    _pollingFrequency = pollingFrequency;
}
```

## Platform Support
*Please note: I have only put in platforms I have tested myself.*
* Xamarin.iOS : iOS 8 +
* Xamarin.Droid : API 16 +
* Windows Phone/Store RT : 8.1 +
* Windows UWP : 10 +
* Xamarin Forms : 2.3.3.180
