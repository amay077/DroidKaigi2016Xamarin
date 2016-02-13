using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using DroidKaigi2016Xamarin.Droid;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using Android.Support.V7.Widget;
using DroidKaigi2016Xamarin.Droid.Widgets;

namespace io.github.droidkaigi.confsched.widget
{
    public class SearchPlacesAndCategoriesView : FrameLayout 
    {
//        public interface OnClickSearchGroup {
//
//            void onClickSearchGroup(SearchGroup searchGroup);
//        }
//
        private SearchPlacesAndCategoriesViewBinding binding;

        private SearchGroupsAdapter adapter;

        public SearchPlacesAndCategoriesView(Context context) : this(context, null)
        {
        }

        public SearchPlacesAndCategoriesView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public SearchPlacesAndCategoriesView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            binding = SearchPlacesAndCategoriesViewBinding.Inflate(LayoutInflater.From(context),
                Resource.Layout.view_search_places_and_categories, this, true);

            InitRecyclerView();
        }

        public void AddPlaces(IList<Place> places) 
        {
            adapter.AddItem(new SearchTitle(Context.GetString(Resource.String.search_by_place)));
            adapter.AddAll(new List<Place>(places));
        }

        public void AddCategories(IList<Category> categories) 
        {
            adapter.AddItem(new SearchTitle(Context.GetString(Resource.String.search_by_category)));
            adapter.AddAll(new List<Category>(categories));
        }

        private void InitRecyclerView() 
        {
            adapter = new SearchGroupsAdapter(Context);

            binding.recyclerView.SetAdapter(adapter);
            binding.recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            binding.recyclerView.AddItemDecoration(new DividerItemDecoration(Context));
        }

        public void SetOnClickSearchGroup(Action<ISearchGroup> onClickSearchGroup) 
        {
            adapter.SetOnClickSearchGroup(onClickSearchGroup);
        }

        private class SearchGroupsAdapter : ArrayRecyclerAdapter<ISearchGroup> 
        {
            private static readonly int TYPE_CATEGORY = 0;
            private static readonly int TYPE_PLACE = 1;
            private static readonly int TYPE_TITLE = 2;

            private Action<ISearchGroup> onClickSearchGroup = searchGroup => {
                // no op
            };

            public SearchGroupsAdapter(Context context) : base(context)
            {
            }

            public void SetOnClickSearchGroup(Action<ISearchGroup> onClickSearchGroup) 
            {
                this.onClickSearchGroup = onClickSearchGroup;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                switch (viewType) 
                {
                    case TYPE_CATEGORY:
                        return new SearchGroupsViewBinder(Context, parent, Resource.Layout.item_search_category);
                    case TYPE_PLACE:
                        return new SearchGroupsViewBinder(Context, parent, Resource.Layout.item_search_place);
                    case TYPE_TITLE:
                        return new SearchGroupsViewBinder(Context, parent, Resource.Layout.item_search_title);
                    default:
                        throw new InvalidOperationException("ViewType is invalid: " + viewType);
                }
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var searchGroup = GetItem(position);
                switch (searchGroup.Type) 
                {
                    case SearchType.CATEGORY:
                        var categoryBinding = holder.binding as SearchCategoryItemBinding;
                        categoryBinding.setCategory(searchGroup as Category);
                        categoryBinding.getRoot().setOnClickListener(v -> showSearchedSessions(searchGroup));
                        break;
                    case SearchType.PLACE:
                        var placeBinding = holder.binding as SearchPlaceItemBinding;
                        placeBinding.setPlace(searchGroup as Place);
                        placeBinding.getRoot().setOnClickListener(v -> showSearchedSessions(searchGroup));
                        break;
                    default:
                        var titleBinding = holder.binding as SearchTitleItemBinding;
                        titleBinding.txtTitle.setText(searchGroup.Name);
                        break;
                }            
            }

            @Override
            public void onBindViewHolder(BindingHolder<ViewDataBinding> holder, int position) {
                SearchGroup searchGroup = getItem(position);
                switch (searchGroup.getType()) {
                    case CATEGORY:
                        ItemSearchCategoryBinding categoryBinding = (ItemSearchCategoryBinding) holder.binding;
                        categoryBinding.setCategory((Category) searchGroup);
                        categoryBinding.getRoot().setOnClickListener(v -> showSearchedSessions(searchGroup));
                        break;
                    case PLACE:
                        ItemSearchPlaceBinding placeBinding = (ItemSearchPlaceBinding) holder.binding;
                        placeBinding.setPlace((Place) searchGroup);
                        placeBinding.getRoot().setOnClickListener(v -> showSearchedSessions(searchGroup));
                        break;
                    default:
                        ItemSearchTitleBinding titleBinding = (ItemSearchTitleBinding) holder.binding;
                        titleBinding.txtTitle.setText(searchGroup.getName());
                        break;
                }
            }

            private void showSearchedSessions(SearchGroup searchGroup) {
                onClickSearchGroup.onClickSearchGroup(searchGroup);
            }

            @Override
            public int getItemViewType(int position) {
                SearchGroup searchGroup = getItem(position);

                switch (searchGroup.getType()) {
                    case CATEGORY:
                        return TYPE_CATEGORY;
                    case PLACE:
                        return TYPE_PLACE;
                    case TITLE:
                        return TYPE_TITLE;
                    default:
                        throw new IllegalStateException("ViewType: " + searchGroup.getType() + " is invalid.");
                }
            }

        }

        private class SearchTitle : ISearchGroup 
        {
            #region ISearchGroup implementation

            public int Id { get; }
            public string Name { get; }
            public SearchType Type { get; }

            #endregion

            public SearchTitle(string title) 
            {
                this.Name = title;
                this.Id = 0;
                this.Type = SearchType.TITLE;
            }
        }
    }
}

