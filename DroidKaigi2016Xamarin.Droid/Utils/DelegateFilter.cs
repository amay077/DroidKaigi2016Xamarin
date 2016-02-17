using System;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;
using Java.Util;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public class DelegateFilter : Filter
    {
        private readonly Func<string, AbstractList> performFilteringHandler;
        private readonly Action<string, AbstractList> publishResultsHandler;

        public static DelegateFilter Create(
            Func<string, AbstractList> performFilteringHandler, 
            Action<string, AbstractList> publishResultsHandler)
        {
            return new DelegateFilter(performFilteringHandler, publishResultsHandler);
        }

        private DelegateFilter(
            Func<string, AbstractList> performFilteringHandler, 
            Action<string, AbstractList> publishResultsHandler) : base()
        {
            this.performFilteringHandler = performFilteringHandler;
            this.publishResultsHandler = publishResultsHandler;
        }

        #region implemented abstract members of Filter
        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var results = performFilteringHandler(constraint.ToString());
            return new FilterResults
            {
                Count = results.Size(),
                Values = results
            };
        }
        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            publishResultsHandler(constraint.ToString(), results.Values as AbstractList);
        }
        #endregion
    }
}

