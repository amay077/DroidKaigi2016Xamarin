using System;
using Android.Support.V4.App;
using DroidKaigi2016Xamarin.Droid.Activities;
using Android.Views;
using Android.Widget;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    class AboutFragmentBinding
    {
        public View Root { get; }

        public readonly TextView txtRep;
        public readonly TextView txtSiteUrl;
        public readonly View imgTwitterClicker;
        public readonly View imgFacebookClicker;
        public readonly TextView txtTerms;
        public readonly TextView txtQuestionnaire;
        public readonly TextView txtLicense;
        public readonly TextView txtVersion;

        public static AboutFragmentBinding Inflate(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            return new AboutFragmentBinding(inflater, root, attachToRoot);
        }

        public AboutFragmentBinding(LayoutInflater inflater, ViewGroup root, bool attachToRoot)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_about, root, attachToRoot);
            txtRep = Root.FindViewById<TextView>(Resource.Id.txt_rep);
            txtSiteUrl = Root.FindViewById<TextView>(Resource.Id.txt_site_url);
            imgTwitterClicker = Root.FindViewById<View>(Resource.Id.img_twitter_clicker);
            imgFacebookClicker = Root.FindViewById<View>(Resource.Id.img_facebook_clicker);
            txtTerms = Root.FindViewById<TextView>(Resource.Id.txt_terms);
            txtQuestionnaire = Root.FindViewById<TextView>(Resource.Id.txt_questionnaire);
            txtLicense = Root.FindViewById<TextView>(Resource.Id.txt_license);
            txtVersion = Root.FindViewById<TextView>(Resource.Id.txt_version);
        }            
    }
}

