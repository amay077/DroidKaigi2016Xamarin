using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using DroidKaigi2016Xamarin.Droid;
using Android.Graphics.Drawables;
using Android.OS;
using DroidKaigi2016Xamarin.Droid.Extensions;
using DroidKaigi2016Xamarin.Droid.Utils;
using Android.Text;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace io.github.droidkaigi.confsched.widget
{
    public class SearchToolbar : FrameLayout 
    {
        private static readonly int DRAWABLE_LEFT = 0;
        private static readonly int DRAWABLE_RIGHT = 2;

        private SearchToolbarBinding binding;

        public SearchToolbar(Context context) : this(context, null)
        {
        }

        public SearchToolbar(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public SearchToolbar(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            binding = SearchToolbarBinding.Inflate(LayoutInflater.From(context), Resource.Layout.toolbar_search, this, true);

            if (!IsInEditMode) 
            {
                var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.SearchToolbar);

                try 
                {
                    var focus = a.GetBoolean(Resource.Styleable.SearchToolbar_searchFocus, false);
                    var hintResId = a.GetResourceId(Resource.Styleable.SearchToolbar_searchHint, Resource.String.search_hint);
                    SetHint(hintResId);
                    if (focus) 
                    {
                        binding.editSearch.RequestFocus();
                    } 
                    else 
                    {
                        ClearFocus();
                    }
                    ToggleCloseButtonVisible(false);
                    InitView();
                } finally {
                    a.Recycle();
                }
            }
        }

        private Drawable GetCloseDrawable() 
        {
            Drawable closeDrawable = null;
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr1) 
            {
                closeDrawable = binding.editSearch.GetCompoundDrawablesRelative()[DRAWABLE_RIGHT];
            } 


            if (closeDrawable == null)
            {
                closeDrawable = binding.editSearch.GetCompoundDrawables()[DRAWABLE_RIGHT];
            }

            return closeDrawable;
        }

        private void ToggleCloseButtonVisible(bool visible) 
        {
            GetCloseDrawable().SetAlpha(visible ? 255 : 0);
        }

        private void InitView() 
        {
            binding.editSearch.AddTextChangedAction((s, start, before, count) =>
                {
                    var visible = count > 0 || start > 0;
                    ToggleCloseButtonVisible(visible);
                });


            binding.editSearch.Touch += (sender, e) => 
                {
                    var evt = e.Event;
                    if (evt.Action == MotionEventActions.Up) 
                    {
                        var shouldClear = false;
                        if (LocaleUtil.ShouldRtl(Context)) 
                        {
                            int rightEdgeOfRightDrawable = binding.editSearch.Left + GetCloseDrawable().Bounds.Width();
                            shouldClear = evt.RawX <= rightEdgeOfRightDrawable;
                        }
                        else 
                        {
                            int leftEdgeOfRightDrawable = binding.editSearch.Right - GetCloseDrawable().Bounds.Width();
                            shouldClear = evt.RawX >= leftEdgeOfRightDrawable;
                        }

                        if (shouldClear) 
                        {
                            ClearText();
                            e.Handled = true;
                        }
                    }
                    e.Handled = false;
                };
        }

        private void ClearText() 
        {
            binding.editSearch.Text = string.Empty;
        }

        public void SetHint(int resId) 
        {
            binding.editSearch.SetHint(resId);
        }

        public String GetText() 
        {
            return binding.editSearch.Text;
        }

        public void AddTextChangedListener(ITextWatcher textWatcher) 
        {
            binding.editSearch.AddTextChangedListener(textWatcher);
        }

        public Toolbar GetToolbar() 
        {
            return binding.toolbar;
        }

    }
}

