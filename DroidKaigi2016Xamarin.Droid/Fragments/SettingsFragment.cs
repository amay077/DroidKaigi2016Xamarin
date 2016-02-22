using System;
using Android.Support.V4.App;
using DroidKaigi2016Xamarin.Droid.Activities;
using Android.Views;
using Android.Content;
using Stiletto;
using System.Collections.Generic;
using Android.OS;
using DroidKaigi2016Xamarin.Droid.Utils;
using System.Linq;
using Android.App;
using Android.Util;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SettingsFragment : Android.Support.V4.App.Fragment 
    {
        private static readonly string TAG = typeof(SettingsFragment).Name;

        [Inject]
        public ActivityNavigator ActivityNavigator { get; set; }
        private SettingsFragmentBinding binding;

        public static SettingsFragment NewInstance() 
        {
            return new SettingsFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
        {
            binding = SettingsFragmentBinding.Inflate(inflater, container, false);
            InitView();
            return binding.Root;
        }

        public override void OnAttach(Context context) 
        {
            base.OnAttach(context);
            MainApplication.GetComponent(this).Inject(this);
        }

        private void InitView() 
        {
            binding.txtLanguage.Text = AppUtil.GetCurrentLanguage(Activity);
            binding.languageSettingsContainer.Click += (_, __) => ShowLanguagesDialog();
            binding.txtBugreport.Click += (_, __) => ShowBugReport();
        }

        private void ShowLanguagesDialog() 
        {
            var languageIds = AppUtil.SUPPORT_LANG;
            var languages = languageIds.Select(languageId => AppUtil.GetLanguage(Activity, languageId));

            var currentLanguageId = AppUtil.GetCurrentLanguageId(Activity);
            int defaultItem = languageIds.Select((langId, index) => new { langId, index }).FirstOrDefault(x => x.langId == currentLanguageId).index;
            String[] items = languages.ToArray();
            var selectedLanguageIds = new List<string>();
            selectedLanguageIds.Add(currentLanguageId);

            new AlertDialog.Builder(Activity)
                .SetTitle(Resource.String.settings_language)
                .SetSingleChoiceItems(items, defaultItem, (dialog, e) => 
                    {
                        selectedLanguageIds.Clear();
                        selectedLanguageIds.Add(languageIds[e.Which]);
                    })
                .SetPositiveButton(Resource.String.ok, (dialog, which) => 
                    {
                        if (selectedLanguageIds.Count > 0) 
                        {
                            var selectedLanguageId = selectedLanguageIds[0];
                            if (!currentLanguageId.Equals(selectedLanguageId)) 
                            {
                                Log.Debug(TAG, "Selected language_id: " + selectedLanguageId);
                                AppUtil.SetLocale(Activity, selectedLanguageId);
                                Restart();
                            }
                        }
                    })
                .SetNegativeButton(Resource.String.cancel, (EventHandler<DialogClickEventArgs>)null)
                .Show();
        }

        private void Restart() 
        {
            ActivityNavigator.ShowMain(Activity);
            Activity.Finish();
        }

        private void ShowBugReport() 
        {
            StartActivity(IntentUtil.ToBrowser(GetString(Resource.String.bug_report_url)));
        }

    }
}

