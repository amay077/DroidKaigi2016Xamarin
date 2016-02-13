using System;
using Android.Support.V7.Widget;
using Android.Graphics.Drawables;
using Android.Content;
using Android.Graphics;
using Android.Views;

namespace DroidKaigi2016Xamarin.Droid.Widgets
{
    public class DividerItemDecoration : RecyclerView.ItemDecoration 
    {
        private static readonly int[] ATTRS = new int[]{global::Android.Resource.Attribute.ListDivider};

        private Drawable divider;

        public DividerItemDecoration(Context context) 
        {
            var a = context.ObtainStyledAttributes(ATTRS);
            divider = a.GetDrawable(0);
            a.Recycle();
        }

        public override void OnDraw(Canvas c, RecyclerView parent, RecyclerView.State state) 
        {
            DrawVertical(c, parent);
        }

        public void DrawVertical(Canvas c, RecyclerView parent) 
        {
            var left = parent.PaddingLeft;
            var right = parent.Width - parent.PaddingRight;

            var childCount = parent.ChildCount;
            for (var i = 0; i < childCount; i++) 
            {
                var child = parent.GetChildAt(i);
                var lparams = child.LayoutParameters as RecyclerView.LayoutParams;
                var top = child.Bottom + lparams.BottomMargin;
                var bottom = top + divider.IntrinsicHeight;
                divider.SetBounds(left, top, right, bottom);
                divider.Draw(c);
            }
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state) 
        {
            outRect.Set(0, 0, 0, divider.IntrinsicHeight);
        }
    }
}

