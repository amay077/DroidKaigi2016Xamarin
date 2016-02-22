using System;
using Android.Support.V7.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.App;
using Android.Content.PM;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    [Activity(
        ConfigurationChanges = ConfigChanges.Keyboard
        | ConfigChanges.KeyboardHidden
        | ConfigChanges.ScreenLayout
        | ConfigChanges.ScreenSize
        | ConfigChanges.Orientation,
        Label = "@string/app_name", 
        Theme = "@style/AppTheme.ColoredStatusBar",
        Exported = false)]
    public class WebViewActivity : AppCompatActivity 
    {
        private static readonly string EXTRA_URL = "url";
        private static readonly string EXTRA_TITLE = "title";

        private WebViewActivityBinding binding;

        public static void Start(Context context, string url, string title) 
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(title)) 
            {
                var intent = new Intent(context, typeof(WebViewActivity));
                intent.PutExtra(EXTRA_URL, url);
                intent.PutExtra(EXTRA_TITLE, title);
                context.StartActivity(intent);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            var title = Intent.GetStringExtra(EXTRA_TITLE);
            var url = Intent.GetStringExtra(EXTRA_URL);

            binding = WebViewActivityBinding.SetContentView(this, Resource.Layout.activity_web_view);

            InitToolbar(title);
            InitWebView(url);
        }

        private void InitWebView(string url) 
        {
            binding.webview.SetWebViewClient(new MyWebViewClient());
            binding.webview.LoadUrl(url);
        }

        public override bool OnOptionsItemSelected(IMenuItem item) 
        {
            if (item.ItemId == global::Android.Resource.Id.Home) 
            {
                OnBackPressed();
            }
            return base.OnOptionsItemSelected(item);
        }

        private void InitToolbar(string title) 
        {
            SetSupportActionBar(binding.toolbar);

            var bar = SupportActionBar;
            if (bar != null) 
            {
                bar.SetDisplayHomeAsUpEnabled(true);
                bar.SetDisplayShowHomeEnabled(true);
                bar.SetDisplayShowTitleEnabled(false);
                bar.SetHomeButtonEnabled(true);
            }
            binding.toolbar.Title = title;
        }

        class MyWebViewClient : Android.Webkit.WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
            {
                return false;
            }
        }
    }
}

