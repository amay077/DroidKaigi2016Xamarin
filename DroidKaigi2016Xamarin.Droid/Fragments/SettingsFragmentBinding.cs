using System;
using Android.Views;
using Android.Widget;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SettingsFragmentBinding
    {
        public View Root { get; }

        public readonly TextView txtLanguage;
        public readonly View languageSettingsContainer;
        public readonly TextView txtBugreport;

        public static SettingsFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new SettingsFragmentBinding(inflater, root, attachToRoot);
        }

        public SettingsFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_settings, root, attachToRoot);
            txtLanguage = Root.FindViewById<TextView>(Resource.Id.txt_language);
            languageSettingsContainer = Root.FindViewById<View>(Resource.Id.language_settings_container);
            txtBugreport = Root.FindViewById<TextView>(Resource.Id.txt_bugreport);
        }    
    }
}

