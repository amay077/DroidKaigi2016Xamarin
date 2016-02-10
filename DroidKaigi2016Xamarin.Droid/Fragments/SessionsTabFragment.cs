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

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionsTabFragment : Android.Support.V4.App.Fragment
    {
        private static readonly string TAG = typeof(SessionsTabFragment).Name;
        private static readonly string ARG_SESSIONS = "sessions";
        private const int REQ_DETAIL = 1;

//        @Inject
//        SessionDao dao;
//        @Inject
//        ActivityNavigator activityNavigator;
//
        private SessionsAdapter adapter;
        private SessionsTabFragmentBinding binding;
//
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
//            MainApplication.getComponent(this).inject(this);
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            binding = SessionsTabFragmentBinding.Inflate(inflater, container, false);
            BindData();
            return binding.Root;
        }

        private void BindData() 
        {
            adapter = new SessionsAdapter(Activity);

            binding.recyclerView.SetAdapter(adapter);
            binding.recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            int spacing = Resources.GetDimensionPixelSize(Resource.Dimension.spacing_xsmall);
//            binding.recyclerView.AddItemDecoration(new SpaceItemDecoration(spacing));
            adapter.AddAll(sessions);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode) 
            {
                case REQ_DETAIL:
                    if (resultCode == 1 /*Result.Ok*/) 
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


        class SessionItemViewHolder : RecyclerView.ViewHolder 
        {
            public View Root { get; }

            public readonly TextView txtTitle;

            public readonly TextView txtStime;

            public readonly View btnStar;

            public static SessionItemViewHolder NewInstance(ViewGroup parent)
            {
                return new SessionItemViewHolder(parent);
            }
            
            private SessionItemViewHolder(ViewGroup parent) : base(parent)
            {
                var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_session, parent, false);

                txtTitle = view.FindViewById<TextView>(Resource.Id.txt_title);
                txtStime = view.FindViewById<TextView>(Resource.Id.txt_stime);
                btnStar = view.FindViewById<View>(Resource.Id.btn_star);

                Root = view;
            }
        }

        class SessionsAdapter : ArrayRecyclerAdapter<Session>
        {
            public SessionsAdapter(Activity activity) : base(activity)
            {
            }

            public void Refresh(Session session) {
                // TODO It may be heavy logic...
                for (int i = 0; i < ItemCount; i++) 
                {
                    Session s = GetItem(i);
                    if (session.Equals(s)) 
                    {
                        s.IsChecked = session.IsChecked;
                        NotifyItemChanged(i);
                    }
                }
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                return SessionItemViewHolder.NewInstance(parent);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var binding = holder as SessionItemViewHolder;
                var session = GetItem(position);

                binding.txtTitle.Text = session.title;
                binding.txtStime.Text = session.stime.ToString();

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

//                binding.btnStar.setOnLikeListener(new OnLikeListener() {
//                    @Override
//                    public void liked(LikeButton likeButton) {
//                        session.checked = true;
//                        dao.updateChecked(session);
//                    }
//
//                    @Override
//                    public void unLiked(LikeButton likeButton) {
//                        session.checked = false;
//                        dao.updateChecked(session);
//                    }
//                });
//
//                binding.cardView.setOnClickListener(v ->
//                    activityNavigator.showSessionDetail(getActivity(), session, REQ_DETAIL));
//            }
            }
        }

//        private class SessionsAdapter extends ArrayRecyclerAdapter<Session, BindingHolder<ItemSessionBinding>> {
//
//            public SessionsAdapter(@NonNull Context context) {
//                super(context);
//            }
//
//            private void refresh(@NonNull Session session) {
//                // TODO It may be heavy logic...
//                for (int i = 0; i < adapter.getItemCount(); i++) {
//                    Session s = adapter.getItem(i);
//                    if (session.equals(s)) {
//                        s.checked = session.checked;
//                        adapter.notifyItemChanged(i);
//                    }
//                }
//            }
//
//            @Override
//            public BindingHolder<ItemSessionBinding> onCreateViewHolder(ViewGroup parent, int viewType) {
//                return new BindingHolder<>(getContext(), parent, R.layout.item_session);
//            }
//
//            @Override
//            public void onBindViewHolder(BindingHolder<ItemSessionBinding> holder, int position) {
//                Session session = getItem(position);
//                ItemSessionBinding binding = holder.binding;
//                binding.setSession(session);
//
//                if (position > 0 && position < getItemCount()) {
//                    Session prevSession = getItem(position - 1);
//                    if (prevSession.stime.getTime() == session.stime.getTime()) {
//                        binding.txtStime.setVisibility(View.INVISIBLE);
//                    } else {
//                        binding.txtStime.setVisibility(View.VISIBLE);
//                    }
//                } else {
//                    binding.txtStime.setVisibility(View.VISIBLE);
//                }
//
//                binding.btnStar.setOnLikeListener(new OnLikeListener() {
//                    @Override
//                    public void liked(LikeButton likeButton) {
//                        session.checked = true;
//                        dao.updateChecked(session);
//                    }
//
//                    @Override
//                    public void unLiked(LikeButton likeButton) {
//                        session.checked = false;
//                        dao.updateChecked(session);
//                    }
//                });
//
//                binding.cardView.setOnClickListener(v ->
//                    activityNavigator.showSessionDetail(getActivity(), session, REQ_DETAIL));
//            }
//
//        }
//        --

    }
}

