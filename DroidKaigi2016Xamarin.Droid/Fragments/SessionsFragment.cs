using System;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Views;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Runtime;
using System.Reactive.Disposables;
using DroidKaigi2016Xamarin.Droid.Utils;
using DroidKaigi2016Xamarin.Droid.Extensions;
using System.Reactive.Linq;
using Android.OS;
using DroidKaigi2016Xamarin.Core.Apis;
using System.Reactive.Threading.Tasks;
using System.Reactive.Concurrency;
using Android.Util;
using Stiletto;
using DroidKaigi2016Xamarin.Droid.Activities;
using System.Threading;
using DroidKaigi2016Xamarin.Droid.Daos;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionsFragment : Fragment
    {
        public static readonly string TAG = typeof(SessionsFragment).Name;
        private static readonly string ARG_SHOULD_REFRESH = "should_refresh";
        private static readonly int REQ_SEARCH = 2;

        [Inject]
        public DroidKaigiClient Client { get; set; }

        [Inject]
        public SessionDao Dao { get; set; }
        [Inject]
        public CompositeDisposable CompositeSubscription { get; set; }
        [Inject]
        public ActivityNavigator ActivityNavigator { get; set; }
        [Inject]
        public MainContentStateBrokerProvider BrokerProvider { get; set; }

        private SessionsPagerAdapter adapter;
        private SessionsFragmentBinding binding;

        public static SessionsFragment NewInstance() 
        {
            return new SessionsFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            binding = SessionsFragmentBinding.Inflate(inflater, container, false);
            HasOptionsMenu = true;
            InitViewPager();
            InitEmptyView();
            CompositeSubscription.Add(LoadData());
            CompositeSubscription.Add(FetchAndSave());
            return binding.Root;
        }

        public override void OnAttach(Android.Content.Context context)
        {
            base.OnAttach(context);
            MainApplication.GetComponent(this).Inject(this);
        }

        private void InitViewPager() 
        {
            adapter = new SessionsPagerAdapter(FragmentManager);
            binding.viewPager.Adapter = adapter;
            binding.tabLayout.SetupWithViewPager(binding.viewPager);
        }

        private void InitEmptyView() 
        {
            binding.emptyViewButton.Click += (sender, e) => 
                {
                    BrokerProvider.Get().Set(Page.ALL_SESSIONS);
                };
        }

        private IDisposable FetchAndSave() 
        {
            return Client.GetSessions().ToObservable()
                .Do(sessions => Dao.UpdateAll(sessions).Subscribe()) // await しないのは、保存完了を待つ必要がないから
                .SubscribeOn(Scheduler.Default)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe();
        }

        protected virtual IDisposable LoadData() 
        {
            return Dao.FindAll()
                .SelectMany(sessions => 
                    {
                        if (sessions.Count == 0) 
                        {
                            return Client.GetSessions().ToObservable()
                                .Do(x => Dao.UpdateAll(x).Subscribe()); // await しないのは、保存完了を待つ必要がないから
                        } else {
                            return Observable.Return(sessions);
                        }
                    })
                .SubscribeOn(Scheduler.Default)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(
                    GroupByDateSessions, 
                    throwable => Log.Error(TAG, "Load failed", throwable));
        }

        protected void ShowEmptyView() 
        {
            binding.emptyView.Visibility = ViewStates.Visible;
        }

        protected void HideEmptyView() 
        {
            binding.emptyView.Visibility = ViewStates.Gone;
        }

        protected void GroupByDateSessions(IList<Session> sessions) 
        {
            var sessionsByDate = new Dictionary<string, IList<Session>>();
            foreach (var session in sessions) 
            {
                var key = DateUtil.GetMonthDate(session.stime.ToJavaDate(), Activity);
                if (sessionsByDate.ContainsKey(key)) 
                {
                    sessionsByDate[key].Add(session);
                } 
                else 
                {
                    var list = new List<Session>();
                    list.Add(session);
                    sessionsByDate.Add(key, list);
                }
            }

            foreach (var e in sessionsByDate) 
            {
                AddFragment(e.Key, e.Value);
            }

            binding.tabLayout.SetupWithViewPager(binding.viewPager);

            if (sessions.Count == 0) 
            {
                ShowEmptyView();
            } 
            else 
            {
                HideEmptyView();
            }
        }

        private void AddFragment(string title, IList<Session> sessions) 
        {
            var fragment = SessionsTabFragment.NewInstance(sessions);
            adapter.Add(title, fragment);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.menu_sessions, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) 
            {
                case Resource.Id.item_search:
                    ActivityNavigator.ShowSearch(this, REQ_SEARCH);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            var fragment = adapter.GetItem(binding.viewPager.CurrentItem);
            if (fragment != null)
            {
                fragment.OnActivityResult(requestCode, resultCode, data);
            }
        }
    }

    class SessionsPagerAdapter : FragmentStatePagerAdapter 
    {
        private IList<SessionsTabFragment> fragments = new List<SessionsTabFragment>();
        private IList<string> titles = new List<string>();

        public SessionsPagerAdapter(FragmentManager fm) : base(fm)
        {
        }

        #region implemented abstract members of PagerAdapter
        public override int Count
        {
            get { return fragments.Count; }
        }
        #endregion

        #region implemented abstract members of FragmentStatePagerAdapter
        public override Fragment GetItem(int position)
        {
            if (position >= 0 && position < fragments.Count) 
            {
                return fragments[position];
            }
            return null;
        }
        #endregion

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(titles[position]);
        }

        public void Add(String title, SessionsTabFragment fragment) 
        {
            fragments.Add(fragment);
            titles.Add(title);
            NotifyDataSetChanged();
        }

    }
}

