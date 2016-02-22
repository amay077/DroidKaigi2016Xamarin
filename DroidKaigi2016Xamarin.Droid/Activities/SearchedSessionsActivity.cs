using System;
using Android.App;
using Android.Content.PM;
using Android.Support.V7.App;
using DroidKaigi2016Xamarin.Droid.Utils;
using DroidKaigi2016Xamarin.Droid.Daos;
using Android.Content;
using DroidKaigi2016Xamarin.Core.Models;
using Android.OS;
using Stiletto;
using Android.Views;
using DroidKaigi2016Xamarin.Droid.Fragments;

using Fragment = Android.Support.V4.App.Fragment;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    [Activity(
        ConfigurationChanges = ConfigChanges.Keyboard
        | ConfigChanges.KeyboardHidden
        | ConfigChanges.ScreenLayout
        | ConfigChanges.ScreenSize
        | ConfigChanges.Orientation,
        Label = "@string/app_name", 
        Theme = "@style/AppTheme.ColoredStatusBar")]
    public class SearchedSessionsActivity : AppCompatActivity 
    {
        [Inject]
        public AnalyticsTracker AnalyticsTracker { get; set; }
        [Inject]
        public SessionDao Dao { get; set; }

        private SearchedSessionsActivityBinding binding;

        public static Intent CreateIntent(Context context, ISearchGroup searchGroup) 
        {
            Intent intent = new Intent(context,  typeof(SearchedSessionsActivity));
            intent.PutExtra(typeof(ISearchGroup).Name, Parcels.Wrap(searchGroup));
            return intent;
        }

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            binding = SearchedSessionsActivityBinding.SetContentView(this, Resource.Layout.activity_searched_sessions);
            MainApplication.GetComponent(this).Inject(this);

            var searchGroup = Parcels.Unwrap<ISearchGroup>(Intent.GetParcelableExtra(typeof(ISearchGroup).Name) as IParcelable);

            InitToolbar(searchGroup);

            ReplaceFragment(SearchedSessionsFragment.NewInstance(searchGroup));
        }

        private void InitToolbar(ISearchGroup searchGroup) 
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

            binding.toolbar.Title =searchGroup.Name;
        }

        private void ReplaceFragment(Fragment fragment) 
        {
            var ft = SupportFragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.content_view, fragment, fragment.GetType().Name);
            ft.Commit();
        }

        protected override void OnStart() 
        {
            base.OnStart();
            AnalyticsTracker.SendScreenView("searchedSessions");
        }

        public override bool OnOptionsItemSelected(IMenuItem item) 
        {
            if (item.ItemId == global::Android.Resource.Id.Home) 
            {
                OnBackPressed();
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            var fragment = SupportFragmentManager.FindFragmentByTag(typeof(SearchedSessionsFragment).Name);
            if (fragment != null) 
            {
                fragment.OnActivityResult(requestCode, (int)resultCode, data);
            }
        }
    }
}
