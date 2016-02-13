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

namespace DroidKaigi2016Xamarin.Droid.Activities
{
    public class SearchActivity : AppCompatActivity, ITextWatcher 
    {
        public void AfterTextChanged(IEditable s)
        {
            throw new NotImplementedException();
        }

        public void BeforeTextChanged(Java.Lang.ICharSequence s, int start, int count, int after)
        {
            throw new NotImplementedException();
        }

        public void OnTextChanged(Java.Lang.ICharSequence s, int start, int before, int count)
        {
            throw new NotImplementedException();
        }

        private static readonly string TAG = typeof(SearchActivity).Name;

        public static readonly string RESULT_STATUS_CHANGED_SESSIONS = "statusChangedSessions";
        private static readonly int REQ_DETAIL = 1;

        private static readonly int REQ_SEARCH_PLACES_AND_CATEGORIES_VIEW = 2;

        [Inject]
        public AnalyticsTracker AnalyticsTracker { get;set; }
        [Inject]
        public ActivityNavigator ActivityNavigator { get;set; }
        [Inject]
        public SessionDao AessionDao { get;set; }
        [Inject]
        public PlaceDao PlaceDao { get;set; }
//        @Inject
//        CategoryDao categoryDao;

        IList<Session> statusChangedSessions = new List<Session>();

//        private SearchResultsAdapter adapter;
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

//            InitToolbar();
//            InitRecyclerView();
//            InitPlacesAndCategoriesView();
//
//            LoadData();
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            statusChangedSessions = Parcels.Unwrap<IList<Session>>(
                savedInstanceState.GetParcelable(RESULT_STATUS_CHANGED_SESSIONS) as IParcelable);
        }

        private void InitPlacesAndCategoriesView() 
        {
            binding.searchPlacesAndCategoriesView.AddPlaces(PlaceDao.FindAll());
//            binding.searchPlacesAndCategoriesView.addCategories(categoryDao.findAll());
//            binding.searchPlacesAndCategoriesView.setOnClickSearchGroup(searchGroup -> {
//                startActivityForResult(SearchedSessionsActivity.createIntent(SearchActivity.this, searchGroup),
//                    REQ_SEARCH_PLACES_AND_CATEGORIES_VIEW);
//            });
        }

//        @Override
//        protected void onStart() {
//            super.onStart();
//            analyticsTracker.sendScreenView("search");
//        }
//
//        private void initToolbar() {
//            setSupportActionBar(binding.searchToolbar.getToolbar());
//
//            ActionBar bar = getSupportActionBar();
//            if (bar != null) {
//                bar.setDisplayHomeAsUpEnabled(true);
//                bar.setDisplayShowHomeEnabled(true);
//                bar.setDisplayShowTitleEnabled(false);
//                bar.setHomeButtonEnabled(true);
//            }
//
//            binding.searchToolbar.addTextChangedListener(this);
//        }
//
//        private void initRecyclerView() {
//            adapter = new SearchResultsAdapter(this);
//
//            binding.recyclerView.setAdapter(adapter);
//            binding.recyclerView.addItemDecoration(new DividerItemDecoration(this));
//            binding.recyclerView.setLayoutManager(new LinearLayoutManager(this));
//        }
//
//        private void loadData() {
//            // TODO It's waste logic...
//            List<Session> sessions = sessionDao.findAll().toBlocking().single();
//
//            List<SearchResult> titleResults = Observable.from(sessions)
//                .map(SearchResult::createTitleType)
//                .toList().toBlocking().single();
//
//            List<SearchResult> descriptionResults = Observable.from(sessions)
//                .map(SearchResult::createDescriptionType)
//                .toList().toBlocking().single();
//
//            List<SearchResult> speakerResults = Observable.from(sessions)
//                .map(SearchResult::createSpeakerType)
//                .toList().toBlocking().single();
//
//            titleResults.addAll(descriptionResults);
//            titleResults.addAll(speakerResults);
//            adapter.setAllList(titleResults);
//        }
//
//        @Override
//        public boolean onOptionsItemSelected(MenuItem item) {
//            if (item.getItemId() == android.R.id.home) {
//                onBackPressed();
//            }
//            return super.onOptionsItemSelected(item);
//        }
//
//        @Override
//        public void finish() {
//            Intent intent = new Intent();
//            intent.putExtra(RESULT_STATUS_CHANGED_SESSIONS, Parcels.wrap(statusChangedSessions));
//            setResult(Activity.RESULT_OK, intent);
//            overridePendingTransition(0, R.anim.activity_fade_exit);
//            super.finish();
//        }
//
//        @Override
//        public void onBackPressed() {
//            finish();
//        }
//
//        @Override
//        public void beforeTextChanged(CharSequence s, int start, int count, int after) {
//            // Do nothing
//        }
//
//        @Override
//        public void onTextChanged(CharSequence s, int start, int before, int count) {
//            adapter.getFilter().filter(s);
//        }
//
//        @Override
//        public void afterTextChanged(Editable s) {
//            if (TextUtils.isEmpty(s)) {
//                binding.searchPlacesAndCategoriesView.setVisibility(View.VISIBLE);
//                binding.recyclerView.setVisibility(View.GONE);
//            } else {
//                binding.searchPlacesAndCategoriesView.setVisibility(View.GONE);
//                binding.recyclerView.setVisibility(View.VISIBLE);
//            }
//        }
//
//        @Override
//        protected void onActivityResult(int requestCode, int resultCode, Intent data) {
//            super.onActivityResult(requestCode, resultCode, data);
//            switch (requestCode) {
//                case REQ_DETAIL: {
//                        if (resultCode != Activity.RESULT_OK) {
//                            return;
//                        }
//                        Session session = Parcels.unwrap(data.getParcelableExtra(Session.class.getSimpleName()));
//                        statusChangedSessions.add(session);
//                        break;
//                    }
//                case REQ_SEARCH_PLACES_AND_CATEGORIES_VIEW: {
//                        if (resultCode != Activity.RESULT_OK) {
//                            return;
//                        }
//                        List<Session> sessions = Parcels.unwrap(data.getParcelableExtra(RESULT_STATUS_CHANGED_SESSIONS));
//                        statusChangedSessions.addAll(sessions);
//                        break;
//                    }
//            }
//        }
//
//        private class SearchResultsAdapter
//        extends ArrayRecyclerAdapter<SearchResult, BindingHolder<ItemSearchResultBinding>>
//        implements Filterable {
//
//            private static final String ELLIPSIZE_TEXT = "...";
//            private static final int ELLIPSIZE_LIMIT_COUNT = 30;
//
//            private TextAppearanceSpan textAppearanceSpan;
//            private List<SearchResult> filteredList;
//            private List<SearchResult> allList;
//            ;
//
//            public SearchResultsAdapter(@NonNull Context context) {
//                super(context);
//                this.filteredList = new ArrayList<>();
//                this.textAppearanceSpan = new TextAppearanceSpan(context, R.style.SearchResultAppearance);
//            }
//
//            public void setAllList(List<SearchResult> searchResults) {
//                this.allList = searchResults;
//            }
//
//            @Override
//            public BindingHolder<ItemSearchResultBinding> onCreateViewHolder(ViewGroup parent, int viewType) {
//                return new BindingHolder<>(getContext(), parent, R.layout.item_search_result);
//            }
//
//            @Override
//            public void onBindViewHolder(BindingHolder<ItemSearchResultBinding> holder, int position) {
//                SearchResult searchResult = getItem(position);
//                ItemSearchResultBinding itemBinding = holder.binding;
//                itemBinding.setSearchResult(searchResult);
//
//                Drawable icon = ContextCompat.getDrawable(getContext(), searchResult.iconRes);
//                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN_MR1) {
//                    itemBinding.txtType.setCompoundDrawablesRelativeWithIntrinsicBounds(icon, null, null, null);
//                } else if (LocaleUtil.shouldRtl(getContext())) {
//                    itemBinding.txtType.setCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);
//                } else {
//                    itemBinding.txtType.setCompoundDrawablesWithIntrinsicBounds(icon, null, null, null);
//                }
//
//                bindText(itemBinding.txtSearchResult, searchResult, binding.searchToolbar.getText());
//
//                itemBinding.getRoot().setOnClickListener(v ->
//                    activityNavigator.showSessionDetail(SearchActivity.this, searchResult.session, REQ_DETAIL));
//            }
//
//            private void bindText(TextView textView, SearchResult searchResult, String searchText) {
//                String text = searchResult.text;
//                if (TextUtils.isEmpty(text)) return;
//
//                text = text.replace("\n", "  ");
//
//                if (TextUtils.isEmpty(searchText)) {
//                    textView.setText(text);
//                } else {
//                    int idx = text.toLowerCase().indexOf(searchText.toLowerCase());
//                    if (idx >= 0) {
//                        SpannableStringBuilder builder = new SpannableStringBuilder();
//                        builder.append(text);
//                        builder.setSpan(
//                            textAppearanceSpan,
//                            idx,
//                            idx + searchText.length(),
//                            Spannable.SPAN_EXCLUSIVE_EXCLUSIVE
//                        );
//
//                        if (idx > ELLIPSIZE_LIMIT_COUNT && searchResult.isDescriptionType()) {
//                            builder.delete(0, idx - ELLIPSIZE_LIMIT_COUNT);
//                            builder.insert(0, ELLIPSIZE_TEXT);
//                        }
//
//                        textView.setText(builder);
//                    } else {
//                        textView.setText(text);
//                    }
//                }
//            }
//
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
//        }
    }
}

