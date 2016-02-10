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
        Android.Support.V4.App.Fragment currentFragment;
        MainActivityBinding binding;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
//            AppUtil.initLocale(this);
            SetContentView(Resource.Layout.activity_main);
            binding = MainActivityBinding.SetContentView(this, Resource.Layout.activity_main);
//            MainApplication.getComponent(this).inject(this);
//
//            subscription.add(brokerProvider.get().observe().subscribe(page -> {
//                toggleToolbarElevation(page.shouldToggleToolbar());
//                changePage(page.getTitleResId(), page.createFragment());
//                binding.navView.setCheckedItem(page.getMenuId());
//            }));
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

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            binding.drawer.CloseDrawer(GravityCompat.Start);

//            Page page = Page.forMenuId(item);
//            ToggleToolbarElevation(page.shouldToggleToolbar());
//            ChangePage(page.getTitleResId(), page.createFragment());

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
    }
}


