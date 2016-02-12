using System;
using Stiletto;
using Android.Gms.Analytics;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public class AnalyticsTracker
    {
        readonly Tracker tracker;

        [Inject]
        public AnalyticsTracker(Tracker tracker) 
        {
            this.tracker = tracker;
        }

        public Tracker GetTracker() 
        {
            return tracker;
        }

        public void SendScreenView(String screenName) 
        {
            tracker.SetScreenName(screenName);
//            tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
        }

        public void SendEvent(String category, String action) 
        {
//            tracker.Send(new HitBuilders.EventBuilder(category, action).Build());
        }
    }
}

