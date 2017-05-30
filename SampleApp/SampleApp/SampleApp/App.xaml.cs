using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xam.Plugin.DeviceManager;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class App : Application
    {
        public App()
        {
            // Device Manager
            DeviceManager.Instance.Initialize();

            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
