using System;
using DroidKaigi2016Xamarin.Core.Models;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Droid.Fragments;
using System.Linq;
using Android.Support.V4.App;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class PageExtension
    {
        private static readonly IDictionary<Page, PageConfig> configMap = new Dictionary<Page, PageConfig>
        {
            { Page.ALL_SESSIONS, new PageConfig(Resource.Id.nav_all_sessions, Resource.String.all_sessions, false, ()=>SessionsFragment.NewInstance()) },
            { Page.MY_SCHEDULE,  new PageConfig(Resource.Id.nav_my_schedule,  Resource.String.my_schedule,  false, ()=>MyScheduleFragment.NewInstance() ) },
//            { Page.MAP,          new PageConfig(Resource.Id.nav_map,          Resource.String.map,          true,  ()=>MapFragment.NewInstance() ) },
            { Page.SETTINGS,     new PageConfig(Resource.Id.nav_settings,     Resource.String.settings,     true, ()=>SettingsFragment.NewInstance() ) },
//            { Page.SPONSORS,     new PageConfig(Resource.Id.nav_sponsors,     Resource.String.sponsors,     true,  ()=>SponsorsFragment.NewInstance() ) },
//            { Page.ABOUT,        new PageConfig(Resource.Id.nav_about,        Resource.String.about,        true,  ()=>AboutFragment.NewInstance() ) },
        };

        public static int GetMenuId(this Page self) 
        {
            return configMap.FirstOrDefault(pair => pair.Key == self).Value?.menuId ?? 0;
        }

        public static bool ShouldToggleToolbar(this Page self) 
        {
            return configMap.FirstOrDefault(pair => pair.Key == self).Value?.toggleToolbar ?? false;
        }

        public static int GetTitleResId(this Page self) 
        {
            return configMap.FirstOrDefault(pair => pair.Key == self).Value?.titleResId ?? 0;
        }

        public static Fragment CreateFragment(this Page self)
        {
            return configMap.FirstOrDefault(pair => pair.Key == self).Value?.createFragment() ?? null;
        }

        public static Page ToPage(this IMenuItem self) 
        {
            return configMap.FirstOrDefault(pair => pair.Value.menuId == self.ItemId).Key;
        }
    }

    class PageConfig
    {
        internal readonly int menuId;
        internal readonly int titleResId;
        internal readonly bool toggleToolbar;
        internal readonly Func<Fragment> createFragment;

        public PageConfig(int menuId, int titleResId, bool toggleToolbar, Func<Fragment> createFragment)
        {
            this.menuId = menuId;
            this.titleResId = titleResId;
            this.toggleToolbar = toggleToolbar;
            this.createFragment = createFragment;
        }
    }
}

