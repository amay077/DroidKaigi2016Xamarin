using System;
using Android.Widget;
using Android.Views.Animations;
using Android.Content;
using Android.Util;
using Android.Views;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using System.Linq;
using Android.Graphics;
using Android.Animation;
using DroidKaigi2016Xamarin.Droid.Widgets;
using DroidKaigi2016Xamarin.Droid;

namespace io.github.droidkaigi.confsched.widget
{
    public class MapSearchView : FrameLayout 
    {
        private static readonly ITimeInterpolator INTERPOLATOR = new AccelerateDecelerateInterpolator();

        private MapSearchViewBinding binding;

        public MapSearchView(Context context) : this(context, null)
        {
        }

        public MapSearchView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public MapSearchView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            binding = MapSearchViewBinding.Inflate(LayoutInflater.From(context), Resource.Layout.view_map_search, this, true);
        }

        public void BindData(IList<PlaceMap> placeMaps, EventHandler<PlaceMap> listener) 
        {
            placeMaps.ToList().ForEach(map => 
                {
                    var item = new MapSearchViewItem(Context);
                    item.BindData(map, (s, v) => 
                        {
                            listener(s, map);
                            RevealOff();
                        });
                    binding.mapListContainer.AddView(item);
                }
            );

            binding.mapListContainer.Click += (_, __) => RevealOff();
        }

        public bool IsVisible
        {
            get 
            {
                return binding.mapListContainer.Visibility == ViewStates.Visible;
            }
        }

        public void Toggle() 
        {
            if (binding.mapListContainer.Visibility != ViewStates.Visible) 
            {
                RevealOn();
            } 
            else 
            {
                RevealOff();
            }
        }

        private void RevealOn() 
        {
            if (binding.mapListContainer.Visibility == ViewStates.Visible) 
            {
                return;
            }

            var container = binding.mapListContainer;
            var animator = ViewAnimationUtils.CreateCircularReveal(
                container,
                container.Right,
                container.Top,
                0,
                (float) Java.Lang.Math.Hypot(container.Width, container.Height));
            animator.SetInterpolator(INTERPOLATOR);
            animator.SetDuration(Resources.GetInteger(Resource.Integer.view_reveal_mills));

            binding.mapListContainer.Visibility = ViewStates.Visible; // TODO

//            animator.addListener(new SupportAnimator.AnimatorListener() {
//                @Override
//                public void onAnimationStart() {
//                    binding.mapListContainer.setVisibility(VISIBLE);
//                }
//
//                @Override
//                public void onAnimationEnd() {
//                    // Do nothing
//                }
//
//                @Override
//                public void onAnimationCancel() {
//                    // Do nothing
//                }
//
//                @Override
//                public void onAnimationRepeat() {
//                    // Do nothing
//                }
//            });
//
//            animator.start();
        }

        public void RevealOff() 
        {
            if (binding.mapListContainer.Visibility != ViewStates.Visible)
            {
                return;
            }

            var container = binding.mapListContainer;
            var animator = ViewAnimationUtils.CreateCircularReveal(
                container,
                container.Right,
                container.Top,
                (float) Java.Lang.Math.Hypot(container.Width, container.Height),
                0);
            animator.SetInterpolator(INTERPOLATOR);
            animator.SetDuration(Resources.GetInteger(Resource.Integer.view_reveal_mills));
            binding.mapListContainer.Visibility = ViewStates.Invisible; // TODO
//            animator.addListener(new SupportAnimator.AnimatorListener() {
//                @Override
//                public void onAnimationStart() {
//                    // Do nothing
//                }
//
//                @Override
//                public void onAnimationEnd() {
//                    binding.mapListContainer.setVisibility(INVISIBLE);
//                }
//
//                @Override
//                public void onAnimationCancel() {
//                    // Do nothing
//                }
//
//                @Override
//                public void onAnimationRepeat() {
//                    // Do nothing
//                }
//            });
//
//            animator.start();
        }

//        public interface OnItemClickListener {
//            void onClick(PlaceMap placeMap);
//        }
    }
}

