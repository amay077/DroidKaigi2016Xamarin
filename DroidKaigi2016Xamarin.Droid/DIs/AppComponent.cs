using System;
using Stiletto;
using Android.App;

namespace DroidKaigi2016Xamarin.Droid.DIs
{
    [Singleton]
//    [Component]
//    @Component(modules = {AppModule.class})
    public class AppComponent 
    {
        private AppModule appModule;

        public AppComponent(MainApplication app)
        {
            appModule = new AppModule(app);
        }

//        void Inject(StethoWrapper stethoDelegator);
//
        public ActivityComponent Plus(ActivityModule module)
        {
            return new ActivityComponent(appModule, module);
        }

    }
}

