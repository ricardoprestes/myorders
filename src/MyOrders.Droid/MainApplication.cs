using System;
using Android.App;
using Android.Runtime;

namespace MyOrders.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
        : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            App.Initialize();
        }
    }
}
