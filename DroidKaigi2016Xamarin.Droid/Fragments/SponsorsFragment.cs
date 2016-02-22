using System;
using Android.Support.V4.App;
using Stiletto;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.Views;
using Android.OS;
using Android.Content;
using Apmem;
using DroidKaigi2016Xamarin.Core.Models;
using System.Linq;
using DroidKaigi2016Xamarin.Droid.Widgets;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class SponsorsFragment : Fragment 
    {
        private static readonly string TAG = typeof(SponsorsFragment).Name;

        private SponsorsFragmentBinding binding;

        [Inject]
        public AnalyticsTracker AnalyticsTracker { get; set; }

        public static SponsorsFragment NewInstance() 
        {
            return new SponsorsFragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
        {
            binding = SponsorsFragmentBinding.Inflate(inflater, container, false);
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
            Sponsor.CreatePlatinumList().ToList()
                .ForEach(sponsor => AddView(sponsor, binding.platinumContainer));
            Sponsor.CreateVideoList().ToList()
                .ForEach(sponsor => AddView(sponsor, binding.videoContainer));
            Sponsor.CreateFoodsList().ToList()
                .ForEach(sponsor => AddView(sponsor, binding.foodsContainer));
            Sponsor.CreateNormalList().ToList()
                .ForEach(sponsor => AddView(sponsor, binding.normalContainer));
        }

        private void AddView(Sponsor sponsor, FlowLayout container) 
        {
            var imageView = new SponsorImageView(Activity);
            imageView.BindData(sponsor, (_, __) => 
                {
                    if (string.IsNullOrEmpty(sponsor.Url))
                    {
                        return;
                    }
                    AnalyticsTracker.SendEvent("sponsor", sponsor.Url);
                    AppUtil.ShowWebPage(Activity, sponsor.Url);
                });
            
            var lparams = new FlowLayout.LayoutParams(
                FlowLayout.LayoutParams.WrapContent, FlowLayout.LayoutParams.WrapContent);
            int margin = (int) Resources.GetDimension(Resource.Dimension.spacing_small);
            lparams.SetMargins(margin, margin, 0, 0);
            container.AddView(imageView, lparams);
        }

    }

}

