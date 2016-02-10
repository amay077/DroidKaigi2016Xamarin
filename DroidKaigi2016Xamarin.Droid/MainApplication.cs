using System;
using Android.App;
using Android.Runtime;

namespace DroidKaigi2016Xamarin.Droid
{
    [Application(
        AllowBackup=true,
        Icon="@mipmap/ic_launcher",
        SupportsRtl=true,
        Theme="@style/AppTheme")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}

