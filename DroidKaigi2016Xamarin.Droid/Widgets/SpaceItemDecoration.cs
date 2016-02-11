using System;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Widgets
{
    public class SpaceItemDecoration : RecyclerView.ItemDecoration 
    {
        private int space;

        public SpaceItemDecoration(int space) 
        {
            this.space = space;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            if (parent.GetChildLayoutPosition(view) != 0) 
            {
                outRect.Top = space;
            }
        }
    }
}

