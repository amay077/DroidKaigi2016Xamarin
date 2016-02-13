using System;
using DroidKaigi2016Xamarin.Droid.Activities;
using Stiletto;
using Android.App;

namespace DroidKaigi2016Xamarin.Droid.DIs
{
//    @ActivityScope
//    @Subcomponent(modules = {ActivityModule.class})
    public class ActivityComponent
    {
        private object[] modules;

        public ActivityComponent(params object[] modules)
        {
            this.modules = modules;
        }

        public void Inject(MainActivity activity)
        {
            var container = Container.Create(modules);
            container.Inject(activity);
        }

        public void Inject(SessionDetailActivity activity) 
        {
            var container = Container.Create(modules);
            container.Inject(activity);
        }

        public void Inject(SearchActivity activity) 
        {
            var container = Container.Create(modules);
            container.Inject(activity);
        }

//
//        void inject(SearchedSessionsActivity activity);

        public FragmentComponent Plus(FragmentModule fragmentModule)
        {
            var newMods = new object[modules.Length + 1];
            Array.Copy(modules, newMods, modules.Length);
            newMods[newMods.Length - 1] = fragmentModule;
            return new FragmentComponent(newMods);
        }
    }
}

