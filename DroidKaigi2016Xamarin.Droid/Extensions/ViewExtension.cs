using System;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class ViewExtension
    {
        public static void SetOnClickAction(this View self, Action<View> clickHandler)
        {
            self.SetOnClickListener(new DelegateOnClickListener(clickHandler));
        }

        class DelegateOnClickListener : Java.Lang.Object, View.IOnClickListener
        {
            readonly Action<View> clickHandler;

            public DelegateOnClickListener(Action<View> clickHandler)
            {
                this.clickHandler = clickHandler;    
            }

            public void OnClick(View v)
            {
                clickHandler(v);
            }
        }
    }
}

