using System;
using Android.Content;
using Android.Content.Res;
using Android.App;
using Android.Widget;
using Android.Text.Method;
using Android.Text;
using Android.Util;
using Java.Util;
using Android.Support.CustomTabs;
using Android.Support.V4.Content;
using Android.Text.Style;
using Android.Views;
using System.Linq;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public static class AppUtil
    {
        private static readonly string TAG =  typeof(AppUtil).Name;

        private static readonly string TWITTER_URL = "https://twitter.com/";
        private static readonly string GITHUB_URL = "https://github.com/";
        private static readonly string FACEBOOK_URL = "https://www.facebook.com/";

        private static readonly string LANG_STRING_RES_PREFIX = "lang_";
        private static readonly string STRING_RES_TYPE = "string";
        private static readonly string LANG_EN_ID = "en";
        public static readonly string[] SUPPORT_LANG = new string[]{ LANG_EN_ID, "ja"};
        private static readonly Locale DEFAULT_LANG = new Locale(LANG_EN_ID);

        public static String GetTwitterUrl(string name) 
        {
            return TWITTER_URL + name;
        }

        public static string GetGitHubUrl(string name) 
        {
            return GITHUB_URL + name;
        }

        public static string GetFacebookUrl(string name) 
        {
            return FACEBOOK_URL + name;
        }

        public static void InitLocale(Context context) 
        {
            SetLocale(context, GetCurrentLanguageId(context));
        }

        public static void SetLocale(Context context, string languageId) 
        {
            var config = new Configuration();
            PrefUtil.Put(context, PrefUtil.KEY_CURRENT_LANGUAGE_ID, languageId);
            config.Locale = new Locale(languageId);
            context.Resources.UpdateConfiguration(config, context.Resources.DisplayMetrics);
        }

        public static Locale GetCurrentLocale(Context context) 
        {
            return new Locale(GetCurrentLanguageId(context));
        }

        public static string GetCurrentLanguageId(Context context) 
        {
            string languageId = null;
            try 
            {
                languageId = PrefUtil.Get(context, PrefUtil.KEY_CURRENT_LANGUAGE_ID, null);
                languageId = null;
                if (languageId == null) 
                {
                    languageId = Java.Util.Locale.Default.Language.ToLower();
                }
                if (!SUPPORT_LANG.Contains(languageId)) 
                {
                    languageId = LANG_EN_ID;
                }
            }
            catch (Exception e) 
            {
                Log.Error(TAG, e.ToString());
            } 
            finally 
            {
                if (TextUtils.IsEmpty(languageId)) 
                {
                    languageId = LANG_EN_ID;
                }
            }
            return languageId;
        }

        public static String GetCurrentLanguage(Context context) 
        {
            return GetLanguage(context, GetCurrentLanguageId(context));
        }

        public static string GetLanguage(Context context, string languageId) 
        {
            return GetString(context, LANG_STRING_RES_PREFIX + languageId);
        }

        public static string GetString(Context context, string resName) 
        {
            try 
            {
                int resourceId = context.Resources.GetIdentifier(
                    resName, STRING_RES_TYPE, context.PackageName);
                if (resourceId > 0) 
                {
                    return context.GetString(resourceId);
                } 
                else 
                {
                    Log.Debug(TAG, "String resource id: " + resName + " is not found.");
                    return "";
                }
            } 
            catch (Exception e) 
            {
                Log.Error(TAG, "String resource id: " + resName + " is not found.", e);
                return "";
            }
        }

        public static string GetVersionName(Context context) 
        {
            try 
            {
                var packageInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
                return context.GetString(Resource.String.about_version_prefix, packageInfo.VersionName);
            } 
            catch (Exception e) 
            {
                Log.Error(TAG, e.Message + "");
                return "";
            }
        }

        public static void Linkify(Activity activity, TextView textView, string linkText, string url) 
        {
            var text = textView.Text;

            var builder = new SpannableStringBuilder();
            builder.Append(text);

            builder.SetSpan(new MyClickableSpan(v =>
                ShowWebPage(activity, url)), 
                text.IndexOf(linkText),
                text.IndexOf(linkText) + linkText.Length,
                SpanTypes.ExclusiveExclusive
            );
            textView.MovementMethod = LinkMovementMethod.Instance;
        }

        public static void ShowWebPage(Activity activity, string url) 
        {
            var intent = new CustomTabsIntent.Builder()
                .SetShowTitle(true)
                .SetToolbarColor(ContextCompat.GetColor(activity, Resource.Color.theme500))
                .Build();

            intent.LaunchUrl(activity, Android.Net.Uri.Parse(url));
        }

        class MyClickableSpan :ClickableSpan
        {
            private readonly Action<View> onClickHandler;

            public MyClickableSpan(Action<View> onClickHandler)
            {
                this.onClickHandler = onClickHandler;
            }

            #region implemented abstract members of ClickableSpan
            public override void OnClick(View widget)
            {
                onClickHandler(widget);
            }
            #endregion
        }

    }
}

