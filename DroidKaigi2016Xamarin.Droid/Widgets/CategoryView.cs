﻿using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Support.V4.Content;
using Android.Views;
using DroidKaigi2016Xamarin.Core.Models;
using DroidKaigi2016Xamarin.Droid.Extensions;
using DroidKaigi2016Xamarin.Droid;
using Android.Graphics;

namespace io.github.droidkaigi.confsched.widget
{
    public class CategoryView : TextView 
    {

        public CategoryView(Context context) : this(context, null)
        {
        }

        public CategoryView(Context context, IAttributeSet attrs) : this(context, attrs, 0)
        {
        }

        public CategoryView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

//        @SuppressWarnings("unused")
//        @BindingAdapter("category")
        public static void SetCategory(CategoryView categoryView, Category category) 
        {
            if (category != null) 
            {
                categoryView.SetTextColor(new Color(ContextCompat.GetColor(categoryView.Context, category.GetVividColorResId())));
                categoryView.SetBackgroundResource(Resource.Drawable.tag_language);
                categoryView.Text = category.name;
            } else {
                categoryView.Visibility = ViewStates.Invisible;
            }
        }

    }
}

