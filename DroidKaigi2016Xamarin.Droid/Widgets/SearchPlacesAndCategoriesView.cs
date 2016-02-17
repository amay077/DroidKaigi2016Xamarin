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
using System.Linq;
using DroidKaigi2016Xamarin.Droid.Extensions;

namespace io.github.droidkaigi.confsched.widget
{
    public class SearchPlacesAndCategoriesView : FrameLayout 
    {
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
            adapter.AddAll(places.Select(x=>(ISearchGroup)x).ToList()); // sharow copy
        }

        public void AddCategories(IList<Category> categories) 
        {
            adapter.AddItem(new SearchTitle(Context.GetString(Resource.String.search_by_category)));
            adapter.AddAll(categories.Select(x=>(ISearchGroup)x).ToList()); // sharow copy
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
                return SearchGroupsViewBinder.NewInstance(Context, parent, viewType);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder h, int position)
            {
                var holder = h as SearchGroupsViewBinder;
                var searchGroup = GetItem(position);
                switch (searchGroup.Type) 
                {
                    case SearchType.CATEGORY:
                        var categoryBinding = holder.binding as SearchCategoryItemBinding;
                        categoryBinding.SetCategory(searchGroup as Category);
                        categoryBinding.Root.SetOnClickAction(v => ShowSearchedSessions(searchGroup));
                        break;
                    case SearchType.PLACE:
                        var placeBinding = holder.binding as SearchPlaceItemBinding;
                        placeBinding.SetPlace(searchGroup as Place);
                        placeBinding.Root.SetOnClickAction(v => ShowSearchedSessions(searchGroup));
                        break;
                    default:
                        var titleBinding = holder.binding as SearchTitleItemBinding;
                        titleBinding.txtTitle.Text = searchGroup.Name;
                        break;
                }            
            }

            private void ShowSearchedSessions(ISearchGroup searchGroup) 
            {
                onClickSearchGroup(searchGroup);
            }

            public override int GetItemViewType(int position)
            {
                var searchGroup = GetItem(position);

                switch (searchGroup.Type) 
                {
                    case SearchType.CATEGORY:
                        return TYPE_CATEGORY;
                    case SearchType.PLACE:
                        return TYPE_PLACE;
                    case SearchType.TITLE:
                        return TYPE_TITLE;
                    default:
                        throw new InvalidOperationException("ViewType: " + searchGroup.Type.ToString() + " is invalid.");
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

