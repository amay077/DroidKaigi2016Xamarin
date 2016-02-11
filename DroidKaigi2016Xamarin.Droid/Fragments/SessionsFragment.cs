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

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SessionsFragment : Fragment
    {
        public static readonly string TAG = typeof(SessionsFragment).Name;

//    @Inject
//    DroidKaigiClient client;
//    @Inject
//    SessionDao dao;
        CompositeDisposable compositeSubscription = new CompositeDisposable();
//    @Inject
//    ActivityNavigator activityNavigator;
//    @Inject
//    MainContentStateBrokerProvider brokerProvider;

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
//            compositeSubscription.Add(LoadData());
            LoadData();
//            compositeSubscription.Add(FetchAndSave());
            return binding.Root;
        }

        public override void OnAttach(Android.Content.Context context)
        {
            base.OnAttach(context);
//        MainApplication.getComponent(this).inject(this);
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
//                    brokerProvider.get().set(Page.ALL_SESSIONS);
                };
        }

        private IDisposable FetchAndSave() 
        {
//            return client.getSessions()
//                .doOnNext(dao::updateAll)
//                .subscribeOn(Schedulers.io())
//                .observeOn(AndroidSchedulers.mainThread())
//                .subscribe();
            throw new NotImplementedException("FetchAndSave");
        }

        protected IDisposable LoadData() 
        {
            var sessions = new List<Session>
            {
                new Session
                {
                        id = 1,
                        title = "OSSの動向を捉えた実装方針",
                        description = "■ 概要\n 近年、Androidアプリ開発において、どんなライブラリが存在するのか知っておくのは必須といっても過言ではないでしょう。\n しかし、初めてAndroidアプリの開発文化に触れてみて、業界としてどういうものがスタンダードになっているのか\n みんなが何に注目しているのかを知るのには少し経験が必要になります。\n 代表的なものを中心に、個人的に今から作るとしたらこんな感じにしようかなっていうのを話していければと思います。\n\n■ 対象者\n　Android 初級 〜 中級\n\n■ 話すこと\n 最低限知っておくべき、各ライブラリのメリット・デメリットを簡単に説明します。\n  また、最後に、自分ならこうする！みたいなのもまとめとして話したいと思います。\n　- 開発環境\n　- Language(Java8, Kotlin)\n　　Java8に近いものを実現するためのライブラリ(Lightweight-Stream-API, Retrolambda, ThreeTenABP)\n　- Support Library (AppCompat, Design, Annotations, RecyclerView ...etc)\n　- DataBinding\n　- Network (Volley, Retrofit OkHttp ...etc)\n　- Serialization (GSON, ProtoBuf)\n　- Image Loader (Picasso, Glide, Fresco ...etc)\n　- Effect (GPUImage ...etc)\n　- DI (ButterKnife, Dagger, RoboGuice)\n　- FRP (RxJava, RxAndroid, RxLifecycle ...etc)\n　- DB/ORM (Realm, ActiveAndroid, RxPreferences, Sqlbrite ...etc)\n　- Pub/Sub (Otto, EventBus ...etc)\n　- UI (ObservableScrollview, Calligraphy ...etc)\n　- Debug (Crashlytics, Timber, Hugo, Steho, LeakCanary, Takt ...etc)",
                        stime = DateTime.Parse("2016-02-18 10:00:00"),
                        etime = DateTime.Parse("2016-02-18 11:00:00"),
                        language_id = "ja",
                        slide_url = "",
                        speaker = new Speaker 
                            {
                                id = 1,
                                name = "wasabeef",
                                image_url = "https://pbs.twimg.com/profile_images/427481863343452160/i-G-x-Gw.jpeg",
                                twitter_name = "wasabeef_jp",
                                github_name = "wasabeef"
                            },
                        category = new Category
                            {
                                id = 1,
                                name = "基調講演"
                            },
                        place = new Place
                            {
                                id = 1,
                                name = "基調講演会場"
                            }
                },
                new Session
                {
                    title = "SessionB",
                    stime = DateTime.Now
                }
            };

            GroupByDateSessions(sessions);

//            IObservable<IList<Session>> cachedSessions = dao.findAll();
//            return cachedSessions.flatMap(sessions -> {
//                if (sessions.isEmpty()) {
//                    return client.getSessions().doOnNext(dao::updateAll);
//                } else {
//                    return Observable.just(sessions);
//                }
//            })
//                .subscribeOn(Schedulers.io())
//                .observeOn(AndroidSchedulers.mainThread())
//                .subscribe(
//                    this::groupByDateSessions,
//                    throwable -> Log.e(TAG, "Load failed", throwable)
//                );
          //  throw new NotImplementedException();
            return null;
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
                } else {
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
//                    activityNavigator.showSearch(Activity);
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

