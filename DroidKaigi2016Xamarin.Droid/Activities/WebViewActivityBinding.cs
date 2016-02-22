using System;
using Android.Support.V7.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.App;
using Android.Webkit;
using Android.Support.V7.Widget;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
	class WebViewActivityBinding
	{
        public ViewGroup Root { get; }

        public readonly WebView webview;
        public readonly Toolbar toolbar;

        public static WebViewActivityBinding SetContentView(Activity activity, int layoutId)
        {
            return new WebViewActivityBinding(activity, layoutId);
        }

        private WebViewActivityBinding(Activity activity, int layoutId)
        {
            activity.SetContentView(layoutId);

            webview = activity.FindViewById<WebView>(Resource.Id.webview);
            toolbar = activity.FindViewById<Toolbar>(Resource.Id.toolbar);

            var decorView = activity.Window.DecorView;
            Root = (ViewGroup) decorView.FindViewById(global::Android.Resource.Id.Content);
        }
	}
}

