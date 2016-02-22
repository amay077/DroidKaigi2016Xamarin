using System;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class MyScheduleFragment : SessionsFragment 
    {

        public static MyScheduleFragment NewInstance() 
        {
            return new MyScheduleFragment();
        }

        protected override IDisposable LoadData() 
        {
            var cachedSessions = Dao.FindByChecked();
            return cachedSessions.Subscribe(sessions => 
                {
                    if (sessions.Count == 0) 
                    {
                        ShowEmptyView();
                    } 
                    else 
                    {
                        GroupByDateSessions(sessions);
                    }
                });
        }

    }

}

