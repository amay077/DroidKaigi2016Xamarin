using System;
using Stiletto;
using Android.Content;
using Android.App;
using Android.Net;
using System.Reactive.Disposables;
using DroidKaigi2016Xamarin.Droid.Activities;
using Android.Gms.Analytics;

namespace DroidKaigi2016Xamarin.Droid.DIs
{
    [Module(IncludedModules = new [] {
        typeof(ActivityModule)
    })]
    public class AppModule 
    {

        static readonly string CACHE_FILE_NAME = "okhttp.cache";
        static readonly long MAX_CACHE_SIZE = 4 * 1024 * 1024;
        static readonly string SHARED_PREF_NAME = "preferences";

        private Context context;

        public AppModule(Application app) 
        {
            context = app;
        }

//        [Provides]
//        public Context GetProvideContext()
//        {
//            return context;
//        }

        [Singleton]
        [Provides]
        public Tracker ProvidesGoogleAnalyticsTracker(Context context) 
        {
            GoogleAnalytics ga = GoogleAnalytics.GetInstance(context);
//            var tracker = ga.NewTracker(BuildConfig.GA_TRACKING_ID);
            var tracker = ga.NewTracker(string.Empty);
            tracker.EnableAdvertisingIdCollection(true);
            tracker.EnableExceptionReporting(true);
            return tracker;
        }

        [Provides]
        public ConnectivityManager ProvideConnectivityManager(Context context) 
        {
            return (ConnectivityManager) context.GetSystemService(Context.ConnectivityService);
        }

//        [Singleton]
//        [Provides]
//        public OkHttpClient provideHttpClient(Context context, Interceptor interceptor) {
//            File cacheDir = new File(context.getCacheDir(), CACHE_FILE_NAME);
//            Cache cache = new Cache(cacheDir, MAX_CACHE_SIZE);
//
//            OkHttpClient.Builder c = new OkHttpClient.Builder()
//                .cache(cache)
//                .addInterceptor(interceptor);
//
//            return c.build();
//        }

//        [Provides]
//        public Interceptor ProvideRequestInterceptor(ConnectivityManager connectivityManager) {
//            return new RequestInterceptor(connectivityManager);
//        }

        [Provides]
        public ISharedPreferences ProvideSharedPreferences(Context context) 
        {
            return context.GetSharedPreferences(SHARED_PREF_NAME, FileCreationMode.Private);
        }

        [Provides]
        public CompositeDisposable ProvideCompositeSubscription() 
        {
            return new CompositeDisposable();
        }

//        @Singleton
//        @Provides
//        public OrmaDatabase provideOrmaDatabase(Context context) {
//            return OrmaDatabase.builder(context).build();
//        }

        [Singleton]
        [Provides]
        public ActivityNavigator ProvideActivityNavigator() 
        {
            return new ActivityNavigator();
        }

    }
}

