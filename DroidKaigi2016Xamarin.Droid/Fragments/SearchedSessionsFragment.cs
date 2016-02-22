using System;
using Android.OS;
using DroidKaigi2016Xamarin.Droid.Utils;
using DroidKaigi2016Xamarin.Core.Models;
using System.Collections.Generic;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SearchedSessionsFragment : SessionsFragment 
    {
        private ISearchGroup searchGroup;

        public static SearchedSessionsFragment NewInstance(ISearchGroup searchGroup) 
        {
            var fragment = new SearchedSessionsFragment();
            var bundle = new Bundle();
            bundle.PutParcelable(typeof(ISearchGroup).Name, Parcels.Wrap(searchGroup));
            fragment.Arguments = bundle;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            searchGroup = Parcels.Unwrap<ISearchGroup>(Arguments.GetParcelable(typeof(ISearchGroup).Name) as IParcelable);
        }

        protected override IDisposable LoadData() 
        {
            return GetSessionsAsObservable().Subscribe(sessions =>
                {
                    if (sessions.Count == 0) 
                    {
                        GroupByDateSessions(sessions);
                    }
                });
        }

        private IObservable<IList<Session>> GetSessionsAsObservable() 
        {
            switch (searchGroup.Type) 
            {
                case SearchType.CATEGORY:
                    return Dao.FindByCategory(searchGroup.Id);
                case SearchType.PLACE:
                    return Dao.FindByPlace(searchGroup.Id);
                default:
                    throw new InvalidOperationException($"Search type: {searchGroup.Type.ToString()} is invalid.");
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            // Do nothing
        }
    }
}

