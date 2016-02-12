using System;
using Android.Support.V4.App;
using DroidKaigi2016Xamarin.Droid.Activities;
using Stiletto;
using DroidKaigi2016Xamarin.Core.Models;
using Android.OS;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.Support.V7.App;
using Android.Content;
using DroidKaigi2016Xamarin.Droid.Extensions;
using Android.Views;
using Android.App;
using Fragment = Android.Support.V4.App.Fragment;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionDetailFragment : Fragment 
    {
        private static readonly string TAG = typeof(SessionDetailFragment).Name;

//        [Inject]
//        SessionDao dao;
        [Inject]
        public ActivityNavigator ActivityNavigator { get; set; }

        private SessionDetailFragmentBinding binding;
        private Session session;

        public static SessionDetailFragment Create(Session session) 
        {
            var fragment = new SessionDetailFragment();
            var args = new Bundle();
            args.PutParcelable(typeof(Session).Name, Parcels.Wrap(session));
            fragment.Arguments = args;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            session = Parcels.Unwrap<Session>(Arguments.GetParcelable(typeof(Session).Name) as IParcelable);

            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop) 
            {
                // Change theme by category
                Activity.SetTheme(session.category.GetThemeResId());
            }
        }

        private void InitToolbar() 
        {
            var activity = ((AppCompatActivity)Activity);
            activity.SetSupportActionBar(binding.toolbar);
            var bar = activity.SupportActionBar;
            if (bar != null) 
            {
                bar.SetDisplayHomeAsUpEnabled(true);
                bar.SetDisplayShowHomeEnabled(true);
                bar.SetDisplayShowTitleEnabled(false);
                bar.SetHomeButtonEnabled(true);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
        {
            binding = SessionDetailFragmentBinding.Inflate(inflater, container, false);
            InitToolbar();
            binding.SetSession(session);

            binding.fab.SetOnClickAction(v => 
                {
                    var isChecked = !binding.fab.Selected;
                    binding.fab.Selected = isChecked;
                    session.IsChecked = isChecked;
//                    dao.UpdateChecked(session);
                    SetResult();
                });

            binding.txtFeedback.Click += (sender, e) => ActivityNavigator.ShowFeedback(Activity);

            return binding.Root;
        }

        private void SetResult() 
        {
            var intent = new Intent();
            intent.PutExtra(typeof(Session).Name, Parcels.Wrap(session));
            Activity.SetResult(Result.Ok, intent);
        }

        public override void OnAttach(Context context) 
        {
            base.OnAttach(context);
            MainApplication.GetComponent(this).Inject(this);
        }

    }
}

