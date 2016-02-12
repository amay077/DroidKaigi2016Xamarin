using System;
using Android.Support.V7.App;
using DroidKaigi2016Xamarin.Droid.Utils;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Content;
using Stiletto;
using Android.App;
using Android.OS;
using DroidKaigi2016Xamarin.Droid.Fragments;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Views;
using Android.Content.PM;

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    [Activity(
        ConfigurationChanges = ConfigChanges.Keyboard
        | ConfigChanges.KeyboardHidden
        | ConfigChanges.ScreenLayout
        | ConfigChanges.ScreenSize
        | ConfigChanges.Orientation,
        Label = "@string/session_detail", 
        Theme = "@style/AppTheme.NoActionBar")]
    public class SessionDetailActivity : AppCompatActivity 
    {
        private static readonly string TAG =  typeof(SessionDetailActivity).Name;

        [Inject]
        public AnalyticsTracker AnalyticsTracker { get; set; }

        private SessionDetailActivityBinding binding;
        private Session session;

        private static Intent CreateIntent(Context context, Session session) 
        {
            var intent = new Intent(context, typeof(SessionDetailActivity));
            intent.PutExtra(typeof(Session).Name, Parcels.Wrap(session));
            return intent;
        }

        public static void StartForResult(Activity activity, Session session, int requestCode) 
        {
            Intent intent = CreateIntent(activity, session);
            activity.StartActivityForResult(intent, requestCode);
        }

        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            binding = SessionDetailActivityBinding.SetContentView(this, Resource.Layout.activity_session_detail);
            MainApplication.GetComponent(this).Inject(this);

            session = Parcels.Unwrap<Session>(Intent.GetParcelableExtra(typeof(Session).Name) as IParcelable);

            ReplaceFragment(SessionDetailFragment.Create(session));
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
            AnalyticsTracker.SendScreenView("session_detail");
        }

        public override bool OnOptionsItemSelected(IMenuItem item) 
        {
            if (item.ItemId == global::Android.Resource.Id.Home) 
            {
                OnBackPressed();
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed() 
        {
            Finish();
        }

    }
}

