using System;
using Android.Runtime;
using DroidKaigi2016Xamarin.Droid.DIs;
using Stiletto;
using Android.Support.V7.App;
using Android.Support.V4.App;

namespace DroidKaigi2016Xamarin.Droid
{
    [Android.App.Application(
        AllowBackup=true,
        Icon="@mipmap/ic_launcher",
        SupportsRtl=true,
        Theme="@style/AppTheme")]
    public class MainApplication : Android.App.Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        AppComponent appComponent;

        public static FragmentComponent GetComponent(Fragment fragment) 
        {
            var activity = (AppCompatActivity) fragment.Activity;
            var application = (MainApplication)fragment.Context.ApplicationContext;
            return application.appComponent
                .Plus(new ActivityModule(activity))
                .Plus(new FragmentModule(fragment));
        }


        public static ActivityComponent GetComponent(AppCompatActivity activity)
        {
            var application = (MainApplication)activity.ApplicationContext;
            return application.appComponent
                .Plus(new ActivityModule(activity));
        }


        public AppComponent Component  
        {
            get { return appComponent; }
        }

        public override void OnCreate()
        {
            base.OnCreate();

            appComponent = new AppComponent(this);

//            appComponent = DaggerAppComponent.builder()
//                .appModule(new AppModule(this))
//                .build();

//            Fabric.with(this, new Crashlytics());
//
//            new StethoWrapper(this).setup();
//
//            AndroidThreeTen.init(this);
        }
    }
}

