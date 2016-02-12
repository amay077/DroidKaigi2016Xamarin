using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
//using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Content.PM;
using Android.Content;
using Android.Support.V4.View;
using DroidKaigi2016Xamarin.Droid.Fragments;
using DroidKaigi2016Xamarin.Droid.Utils;
using System.Reactive.Disposables;
using Stiletto;
using DroidKaigi2016Xamarin.Core.Models;
using DroidKaigi2016Xamarin.Droid.Extensions;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    [Activity(
        ConfigurationChanges = ConfigChanges.Keyboard
        | ConfigChanges.KeyboardHidden
        | ConfigChanges.ScreenLayout
        | ConfigChanges.ScreenSize
        | ConfigChanges.Orientation,
        Label = "@string/app_name", 
        Theme = "@style/AppTheme.NoActionBar",
        MainLauncher = true)]
    [IntentFilter (new []{ Intent.ActionMain }, 
        Categories = new []{ Intent.CategoryDefault })]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private static readonly string TAG = typeof(MainActivity).Name;
        private static readonly int BACK_BUTTON_PRESSED_INTERVAL = 3000;

        [Inject]
        public AnalyticsTracker AnalyticsTracker { get; set; }

        [Inject]
        public MainContentStateBrokerProvider BrokerProvider { get; set; }

        [Inject]
        public CompositeDisposable Subscription { get; set; }

        private MainActivityBinding binding;
        private Android.Support.V4.App.Fragment currentFragment;
        private bool isPressedBackOnce = false;

        static void Start(Activity activity) 
        {
            Intent intent = new Intent(activity, typeof(MainActivity));
            activity.StartActivity(intent);
            activity.OverridePendingTransition(Resource.Animation.activity_fade_enter, Resource.Animation.activity_fade_exit);
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppUtil.InitLocale(this);
            binding = MainActivityBinding.SetContentView(this, Resource.Layout.activity_main);
            MainApplication.GetComponent(this).Inject(this);

            Subscription.Add(BrokerProvider.Get().Observe().Subscribe(page => 
                {
                    ToggleToolbarElevation(page.ShouldToggleToolbar());
                    ChangePage(page.GetTitleResId(), page.CreateFragment());
                    binding.navView.SetCheckedItem(page.GetMenuId());
                }));
            InitView();

            ReplaceFragment(SessionsFragment.NewInstance());
        }

        private void InitView() 
        {
            SetSupportActionBar(binding.toolbar);
            var toggle = new ActionBarDrawerToggle(this,
                binding.drawer, binding.toolbar, Resource.String.open, Resource.String.close);
            binding.drawer.SetDrawerListener(toggle);
            toggle.SyncState();
            binding.navView.SetNavigationItemSelectedListener(this);
            binding.navView.SetCheckedItem(Resource.Id.nav_all_sessions);
        }

        private void ReplaceFragment(Android.Support.V4.App.Fragment fragment) 
        {
            var ft = SupportFragmentManager.BeginTransaction();
            ft.SetCustomAnimations(Resource.Animation.fragment_fade_enter, Resource.Animation.fragment_fade_exit);
            ft.Replace(Resource.Id.content_view, fragment, fragment.Class.SimpleName);
            ft.Commit();

            currentFragment = fragment;
        }

        protected override void OnStart() 
        {
            base.OnStart();
            AnalyticsTracker.SendScreenView("main");
        }

        public override void OnBackPressed() 
        {
            if (binding.drawer.IsDrawerOpen(GravityCompat.Start)) 
            {
                binding.drawer.CloseDrawer(GravityCompat.Start);
            } 
            else if (isPressedBackOnce) 
            {
                base.OnBackPressed();
                return;
            }

            isPressedBackOnce = true;
            ShowSnackBar(GetString(Resource.String.app_close_confirm));
            new Handler().PostDelayed(() => isPressedBackOnce = false, BACK_BUTTON_PRESSED_INTERVAL);
        }

        private void ShowSnackBar(string text) 
        {
            Snackbar.Make(binding.Root, text, Snackbar.LengthLong)
                .SetAction(Resource.String.app_close_now, v => Finish())
                .Show();
        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            binding.drawer.CloseDrawer(GravityCompat.Start);

            var page = item.ToPage();
            ToggleToolbarElevation(page.ShouldToggleToolbar());
            ChangePage(page.GetTitleResId(), page.CreateFragment());

            return true;
        }

        private void ToggleToolbarElevation(bool enable) 
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop) 
            {
                float elevation = enable ? Resources.GetDimension(Resource.Dimension.elevation) : 0;
                binding.toolbar.TranslationZ = elevation; // toolbar.setElevation(elevation);
            }
        }

        private void ChangePage(int titleRes, Android.Support.V4.App.Fragment fragment) 
        {
            new Handler().PostDelayed(() => 
                {
                    binding.toolbar.SetTitle(titleRes);
                    ReplaceFragment(fragment);
                }, 300);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (currentFragment != null) 
            {
                currentFragment.OnActivityResult(requestCode, resultCode == Result.Ok ? -1 : 0, data);
            }
        }

        public override void Finish() 
        {
            base.Finish();
            OverridePendingTransition(Resource.Animation.activity_fade_enter, Resource.Animation.activity_fade_exit);
        }

    }
}


