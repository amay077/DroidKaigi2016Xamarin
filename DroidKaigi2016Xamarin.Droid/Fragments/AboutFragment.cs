using System;
using Android.Support.V4.App;
using DroidKaigi2016Xamarin.Droid.Activities;
using Android.Views;
using Android.Content;
using Stiletto;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.OS;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class AboutFragment : Fragment 
    {
        private static readonly string TAG = typeof(AboutFragment).Name;

        private static readonly string REP_TWITTER_NAME = "mhidaka";
        private static readonly string SITE_URL = "https://droidkaigi.github.io/2016";
        private static readonly string CONF_TWITTER_NAME = "DroidKaigi";
        private static readonly string CONF_FACEBOOK_NAME = "DroidKaigi";
        private static readonly string LICENSE_URL = "file:///android_asset/license.html";

        [Inject]
        public ActivityNavigator ActivityNavigator { get; set; }

        private AboutFragmentBinding binding;

        public static AboutFragment NewInstance() 
        {
            return new AboutFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
        {
            binding = AboutFragmentBinding.Inflate(inflater, container, false);
            InitView();
            return binding.Root;
        }

        public override void OnAttach(Context context) 
        {
            base.OnAttach(context);
            MainApplication.GetComponent(this).Inject(this);
        }

        private void InitView() 
        {
            var repText = GetString(Resource.String.about_rep, REP_TWITTER_NAME);
            binding.txtRep.Text = repText;
            AppUtil.Linkify(Activity, binding.txtRep, REP_TWITTER_NAME, AppUtil.GetTwitterUrl(REP_TWITTER_NAME));

            binding.txtSiteUrl.Text = SITE_URL;
            AppUtil.Linkify(Activity, binding.txtSiteUrl, SITE_URL, SITE_URL);

            binding.imgFacebookClicker.Click += (_, __) => 
                AppUtil.ShowWebPage(Activity, AppUtil.GetFacebookUrl(CONF_FACEBOOK_NAME));
            binding.imgTwitterClicker.Click += (_, __) => 
                AppUtil.ShowWebPage(Activity, AppUtil.GetTwitterUrl(CONF_TWITTER_NAME));

            binding.txtTerms.Click += (_, __) => 
                {
                    // TODO
                };
            binding.txtQuestionnaire.Click += (_, __) => 
                AppUtil.ShowWebPage(Activity, GetString(Resource.String.about_inquiry_url));
            binding.txtLicense.Click += (_, __) => 
                ActivityNavigator.ShowWebView(Activity, LICENSE_URL, GetString(Resource.String.about_license));

            binding.txtVersion.Text = AppUtil.GetVersionName(Activity);
        }
    }
}

