using System;
using Android.Support.V4.App;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using Android.OS;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.Widget;
using Android.Views;
using Android.App;
using Android.Support.V7.Widget;
using Android.Content;
using DroidKaigi2016Xamarin.Droid.Widgets;
using DroidKaigi2016Xamarin.Droid.Extensions;
using io.github.droidkaigi.confsched.widget;
using DroidKaigi2016Xamarin.Droid.Activities;
using Stiletto;
using DroidKaigi2016Xamarin.Droid.Daos;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionsTabFragment : Android.Support.V4.App.Fragment
    {
        private static readonly string TAG = typeof(SessionsTabFragment).Name;
        private static readonly string ARG_SESSIONS = "sessions";
        private const int REQ_DETAIL = 1;

        [Inject]
        public SessionDao Dao { get; set; }

        [Inject]
        public ActivityNavigator ActivityNavigator { get; set; }

        private SessionsAdapter adapter;
        private SessionsTabFragmentBinding binding;

        private IList<Session> sessions;

        public static SessionsTabFragment NewInstance(IList<Session> sessions)
        {
            var fragment = new SessionsTabFragment();
            var args = new Bundle();
            args.PutParcelable(ARG_SESSIONS, Parcels.Wrap(sessions));
            fragment.Arguments = args;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            sessions = Parcels.Unwrap<IList<Session>>(Arguments.GetParcelable(ARG_SESSIONS) as IParcelable);
        }

        public override void OnAttach(Android.Content.Context context)
        {
            base.OnAttach(context);
            MainApplication.GetComponent(this).Inject(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            binding = SessionsTabFragmentBinding.Inflate(inflater, container, false);
            BindData();
            return binding.Root;
        }

        private void BindData() 
        {
            adapter = new SessionsAdapter(ActivityNavigator, Dao, Activity);

            binding.recyclerView.SetAdapter(adapter);
            binding.recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            int spacing = Resources.GetDimensionPixelSize(Resource.Dimension.spacing_xsmall);
            binding.recyclerView.AddItemDecoration(new SpaceItemDecoration(spacing));
            adapter.AddAll(sessions);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode) 
            {
                case REQ_DETAIL:
                    if (resultCode == -1 /*Result.Ok*/) 
                    {
                        var session = Parcels.Unwrap<Session>(data.GetParcelableExtra(typeof(Session).Name) as IParcelable);
                        if (session != null) 
                        {
                            adapter.Refresh(session);
                        }
                    }
                    break;
            }
        }

        class SessionsAdapter : ArrayRecyclerAdapter<Session>
        {
            private readonly ActivityNavigator activityNavigator;
            private readonly SessionDao dao;

            public SessionsAdapter(ActivityNavigator activityNavigator, SessionDao dao, Activity activity) : base(activity)
            {
                this.activityNavigator = activityNavigator;
                this.dao = dao;
            }

            public void Refresh(Session session) 
            {
                // TODO It may be heavy logic...
                for (var i = 0; i < ItemCount; i++) 
                {
                    var s = GetItem(i);
                    if (session.Equals(s)) 
                    {
                        s.IsChecked = session.IsChecked;
                        NotifyItemChanged(i);
                    }
                }
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                return SessionItemViewBinder.NewInstance(parent);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var binding = holder as SessionItemViewBinder;
                var session = GetItem(position);
                binding.SetSession(session);

                if (position > 0 && position < ItemCount) 
                {
                    Session prevSession = GetItem(position - 1);
                    if (prevSession.stime.Ticks == session.stime.Ticks) 
                    {
                        binding.txtStime.Visibility = ViewStates.Invisible;
                    } 
                    else 
                    {
                        binding.txtStime.Visibility = ViewStates.Visible;
                    }
                } 
                else 
                {
                    binding.txtStime.Visibility = ViewStates.Visible;
                }

                binding.btnStar.SetOnLikeAction(
                    v =>
                    {
                        session.IsChecked = true;
                        dao.UpdateChecked(session).Subscribe();
                    },
                    v =>
                    {
                        session.IsChecked = false;
                        dao.UpdateChecked(session).Subscribe();
                    });

                binding.cardView.SetOnClickAction(v => 
                    activityNavigator.ShowSessionDetail(this.Context as Activity, session, REQ_DETAIL));
            }
        }
    }
}

