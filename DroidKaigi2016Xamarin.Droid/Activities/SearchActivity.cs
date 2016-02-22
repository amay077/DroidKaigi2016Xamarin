using System;
using Android.Support.V7.App;
using Android.Text;
using DroidKaigi2016Xamarin.Droid.Utils;
using Stiletto;
using DroidKaigi2016Xamarin.Droid.Daos;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using System.Reactive.Linq;
using DroidKaigi2016Xamarin.Droid.Widgets;
using Android.Support.V7.Widget;
using DroidKaigi2016Xamarin.Droid.Extensions;
using Android.Views;
using Android.App;
using Android.Widget;
using Android.Text.Style;
using Android.Support.V4.Content;
using Fragment = Android.Support.V4.App.Fragment;
using Java.Lang;
using System.Linq;
using Java.Util;
using Android.Content.PM;

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
    public class SearchActivity : AppCompatActivity, ITextWatcher 
    {
        private static readonly string TAG = typeof(SearchActivity).Name;

        public static readonly string RESULT_STATUS_CHANGED_SESSIONS = "statusChangedSessions";
        private const int REQ_DETAIL = 1;

        private const int REQ_SEARCH_PLACES_AND_CATEGORIES_VIEW = 2;

        [Inject]
        public AnalyticsTracker AnalyticsTracker { get;set; }
        [Inject]
        public ActivityNavigator ActivityNavigator { get;set; }
        [Inject]
        public SessionDao SessionDao { get;set; }
        [Inject]
        public PlaceDao PlaceDao { get;set; }
        [Inject]
        public CategoryDao CategoryDao { get;set; }

        IList<Session> statusChangedSessions = new List<Session>();

        private SearchResultsAdapter adapter;
        private SearchActivityBinding binding;

        public static void Start(Fragment fragment, int requestCode) 
        {
            var intent = new Intent(fragment.Context, typeof(SearchActivity));
            fragment.StartActivityForResult(intent, requestCode);
            fragment.Activity.OverridePendingTransition(0, Resource.Animation.activity_fade_exit);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            binding = SearchActivityBinding.SetContentView(this, Resource.Layout.activity_search);
            MainApplication.GetComponent(this).Inject(this);

            InitToolbar();
            InitRecyclerView();
            InitPlacesAndCategoriesView();

            LoadData();
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            statusChangedSessions = Parcels.Unwrap<IList<Session>>(
                savedInstanceState.GetParcelable(RESULT_STATUS_CHANGED_SESSIONS) as IParcelable);
        }

        private void InitPlacesAndCategoriesView() 
        {
            binding.searchPlacesAndCategoriesView.AddPlaces(PlaceDao.FindAll().Wait());
            binding.searchPlacesAndCategoriesView.AddCategories(CategoryDao.FindAll().Wait());
            binding.searchPlacesAndCategoriesView.SetOnClickSearchGroup(searchGroup => 
                {
                    StartActivityForResult(SearchedSessionsActivity.CreateIntent(this, searchGroup),
                        REQ_SEARCH_PLACES_AND_CATEGORIES_VIEW);
                });
        }

        protected override void OnStart()
        {
            base.OnStart();
            AnalyticsTracker.SendScreenView("search");
        }

        private void InitToolbar() 
        {
            SetSupportActionBar(binding.searchToolbar.GetToolbar());

            var bar = SupportActionBar;
            if (bar != null) 
            {
                bar.SetDisplayHomeAsUpEnabled(true);
                bar.SetDisplayShowHomeEnabled(true);
                bar.SetDisplayShowTitleEnabled(false);
                bar.SetHomeButtonEnabled(true);
            }

            binding.searchToolbar.AddTextChangedListener(this);
        }

        private void InitRecyclerView() 
        {
            adapter = new SearchResultsAdapter(this, ActivityNavigator, binding);

            binding.recyclerView.SetAdapter(adapter);
            binding.recyclerView.AddItemDecoration(new DividerItemDecoration(this));
            binding.recyclerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        private void LoadData() 
        {
            // TODO It's waste logic...
            var sessions = SessionDao.FindAll().Wait();
            var titleResults = sessions.ToObservable()
                .Select(session => session.CreateTitleType())
                .ToList().Wait();

            var descriptionResults = sessions.ToObservable()
                .Select(session => session.CreateDescriptionType())
                .ToList().Wait();

            var speakerResults = sessions.ToObservable()
                .Select(session => session.CreateSpeakerType())
                .ToList().Wait();

            titleResults.AddAll(descriptionResults);
            titleResults.AddAll(speakerResults);
            adapter.SetAllList(titleResults);
        }

        public override bool OnOptionsItemSelected(IMenuItem item) 
        {
            if (item.ItemId == global::Android.Resource.Id.Home) 
            {
                OnBackPressed();
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void Finish()
        {
            var intent = new Intent();
            intent.PutExtra(RESULT_STATUS_CHANGED_SESSIONS, Parcels.Wrap(statusChangedSessions));
            SetResult(Result.Ok, intent);
            OverridePendingTransition(0, Resource.Animation.activity_fade_exit);
            base.Finish();
        }

        public override void OnBackPressed()
        {
            Finish();
        }


        public void BeforeTextChanged(Java.Lang.ICharSequence s, int start, int count, int after)
        {
        }

        public void OnTextChanged(Java.Lang.ICharSequence s, int start, int before, int count)
        {
            adapter.Filter.InvokeFilter(s);
        }

        public void AfterTextChanged(IEditable s)
        {
            if (TextUtils.IsEmpty(s)) 
            {
                binding.searchPlacesAndCategoriesView.Visibility = ViewStates.Visible;
                binding.recyclerView.Visibility = ViewStates.Gone;
            } 
            else 
            {
                binding.searchPlacesAndCategoriesView.Visibility = ViewStates.Gone;
                binding.recyclerView.Visibility = ViewStates.Visible;
            }
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            switch (requestCode) 
            {
                case REQ_DETAIL: 
                    {
                        if (resultCode != Result.Ok) 
                        {
                            return;
                        }
                        var session = Parcels.Unwrap<Session>(data.GetParcelableExtra(typeof(Session).Name) as IParcelable);
                        statusChangedSessions.Add(session);
                        break;
                    }
                case REQ_SEARCH_PLACES_AND_CATEGORIES_VIEW: 
                    {
                        if (resultCode != Result.Ok) 
                        {
                            return;
                        }
                        var sessions = Parcels.Unwrap<IList<Session>>(data.GetParcelableExtra(RESULT_STATUS_CHANGED_SESSIONS) as IParcelable);
                        statusChangedSessions.AddAll(sessions);
                        break;
                    }
            }
        }

        private class SearchResultItemViewBinder : RecyclerView.ViewHolder
        {
            public View Root { get; }
            public readonly TextView txtType;
            public readonly TextView txtSearchResult;
            public readonly ImageView imgSpeaker;

            public static SearchResultItemViewBinder NewInstance(Context context, ViewGroup parent, int layoutId)
            {
                var view = LayoutInflater.From(context).Inflate(layoutId, parent, false);

                return new SearchResultItemViewBinder(view);
            }

            private SearchResultItemViewBinder(View view) : base(view)
            {
                Root = view;
                txtType = view.FindViewById<TextView>(Resource.Id.txt_type);
                txtSearchResult = view.FindViewById<TextView>(Resource.Id.txt_search_result);
                imgSpeaker = view.FindViewById<ImageView>(Resource.Id.img_speaker);

            }

            public void SetSearchResult(SearchResult searchResult)
            {
                txtType.Text = searchResult?.session?.title ?? string.Empty;
                imgSpeaker.SetSpeakerImageUrlWithSize(searchResult?.speakerImageUrl ?? string.Empty, 
                    Root.Context.Resources.GetDimension(Resource.Dimension.user_image_small));
            }
        }

        private class SearchResultsAdapter : ArrayRecyclerAdapter<SearchResult>, IFilterable 
        {
            private static readonly string ELLIPSIZE_TEXT = "...";
            private static readonly int ELLIPSIZE_LIMIT_COUNT = 30;

            private readonly TextAppearanceSpan textAppearanceSpan;
            private readonly AbstractList filteredList;
            private IList<SearchResult> allList;

            private readonly ActivityNavigator activityNavigator;
            private readonly SearchActivityBinding binding;

            public SearchResultsAdapter(Context context, ActivityNavigator activityNavigator, SearchActivityBinding binding) : base(context)
            {
                this.activityNavigator = activityNavigator;
                this.binding = binding;
                this.filteredList = new ArrayList();
                this.textAppearanceSpan = new TextAppearanceSpan(context, Resource.Style.SearchResultAppearance);
            }

            public void SetAllList(IList<SearchResult> searchResults) 
            {
                this.allList = searchResults;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                return SearchResultItemViewBinder.NewInstance(Context, parent, Resource.Layout.item_search_result);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var searchResult = GetItem(position);
                var itemBinding = holder as SearchResultItemViewBinder;
                itemBinding.SetSearchResult(searchResult);

                var icon = ContextCompat.GetDrawable(Context, searchResult.iconRes);
                if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr1) 
                {
                    itemBinding.txtType.SetCompoundDrawablesRelativeWithIntrinsicBounds(icon, null, null, null);
                } 
                else if (LocaleUtil.ShouldRtl(Context)) 
                {
                    itemBinding.txtType.SetCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);
                } 
                else 
                {
                    itemBinding.txtType.SetCompoundDrawablesWithIntrinsicBounds(icon, null, null, null);
                }

                BindText(itemBinding.txtSearchResult, searchResult, binding.searchToolbar.GetText());

                itemBinding.Root.SetOnClickAction(v =>
                    activityNavigator.ShowSessionDetail(Context as Activity, searchResult.session, REQ_DETAIL));
            }

            private void BindText(TextView textView, SearchResult searchResult, string searchText) 
            {
                var text = searchResult.text;
                if (TextUtils.IsEmpty(text)) return;

                text = text.Replace("\n", "  ");

                if (TextUtils.IsEmpty(searchText)) 
                {
                    textView.Text = text;
                } 
                else 
                {
                    int idx = text.ToLower().IndexOf(searchText.ToLower());
                    if (idx >= 0) 
                    {
                        var builder = new SpannableStringBuilder();
                        builder.Append(text);
                        builder.SetSpan(
                            textAppearanceSpan,
                            idx,
                            idx + searchText.Length,
                            SpanTypes.ExclusiveExclusive
                        );

                        if (idx > ELLIPSIZE_LIMIT_COUNT && searchResult.IsDescriptionType()) 
                        {
                            builder.Delete(0, idx - ELLIPSIZE_LIMIT_COUNT);
                            builder.Insert(0, ELLIPSIZE_TEXT);
                        }

                        textView.Text = builder.ToString();
                    } 
                    else 
                    {
                        textView.Text = text;
                    }
                }
            }

            public Filter Filter
            {
                get
                {
                    return DelegateFilter.Create(
                        constraint =>
                        {
                            filteredList.Clear();
//                            var results = new FilterResults();

                            if (constraint.Length > 0)
                            {
                                var filterPattern = constraint.ToLower().Trim();
                                allList.ToObservable()
                                    .Where(searchResult => searchResult.text.ToLower().Contains(filterPattern))
                                    .Subscribe(searchResult => filteredList.Add(searchResult));
                            }

//                            results.values = filteredList;
//                            results.count = filteredList.Count;

                            return filteredList;
                        },
                        (constraint, results) =>
                        {
                            Clear();

                            for (int i = 0; i < results.Size(); i++) 
                            {
                                var result = results.Get(i) as SearchResult;    
                                if (result == null) 
                                {
                                    continue;    
                                }
                                AddItem(result);
                            }

                            NotifyDataSetChanged();
                        }
                    );
                }
            }


//            @Override
//            public Filter getFilter() {
//                return new Filter() {
//                    @Override
//                    protected FilterResults performFiltering(CharSequence constraint) {
//                        filteredList.clear();
//                        FilterResults results = new FilterResults();
//
//                        if (constraint.length() > 0) {
//                            final String filterPattern = constraint.toString().toLowerCase().trim();
//                            Observable.from(allList)
//                                .filter(searchResult -> searchResult.text.toLowerCase().contains(filterPattern))
//                                .forEach(filteredList::add);
//                        }
//
//                        results.values = filteredList;
//                        results.count = filteredList.size();
//
//                        return results;
//                    }
//
//                    @Override
//                    protected void publishResults(CharSequence constraint, FilterResults results) {
//                        clear();
//                        addAll((List<SearchResult>) results.values);
//                        notifyDataSetChanged();
//                    }
//                };
//            }
//
        }
    }
}

