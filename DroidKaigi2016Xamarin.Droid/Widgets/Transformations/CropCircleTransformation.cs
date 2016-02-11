using System;
using Square.Picasso;
using Android.Graphics;

namespace DroidKaigi2016Xamarin.Droid.Widgets.Transformations
{
    public class CropCircleTransformation : Java.Lang.Object, ITransformation 
    {
        public Bitmap Transform(Bitmap source)
        {
            int size = Math.Min(source.Width, source.Height);

            int width = (source.Width - size) / 2;
            int height = (source.Height - size) / 2;

            var bitmap = Bitmap.CreateBitmap(size, size, Bitmap.Config.Argb4444);

            var canvas = new Canvas(bitmap);
            var paint = new Paint();
            var shader =
                new BitmapShader(source, BitmapShader.TileMode.Clamp, BitmapShader.TileMode.Clamp);
            if (width != 0 || height != 0) 
            {
                // source isn't square, move viewport to center
                var matrix = new Matrix();
                matrix.SetTranslate(-width, -height);
                shader.SetLocalMatrix(matrix);
            }
            paint.SetShader(shader);
            paint.AntiAlias = true;

            float r = size / 2f;
            canvas.DrawCircle(r, r, r, paint);

            source.Recycle();

            return bitmap;
        }

        public string Key
        {
            get { return "CropCircleTransformation()"; }
        }


        //        @Override
        //        public Bitmap transform(Bitmap source) {
        //            int size = Math.min(source.getWidth(), source.getHeight());
        //
        //            int width = (source.getWidth() - size) / 2;
        //            int height = (source.getHeight() - size) / 2;
        //
        //            Bitmap bitmap = Bitmap.createBitmap(size, size, Bitmap.Config.ARGB_4444);
        //
        //            Canvas canvas = new Canvas(bitmap);
        //            Paint paint = new Paint();
        //            BitmapShader shader =
        //                new BitmapShader(source, BitmapShader.TileMode.CLAMP, BitmapShader.TileMode.CLAMP);
        //            if (width != 0 || height != 0) {
        //                // source isn't square, move viewport to center
        //                Matrix matrix = new Matrix();
        //                matrix.setTranslate(-width, -height);
        //                shader.setLocalMatrix(matrix);
        //            }
        //            paint.setShader(shader);
        //            paint.setAntiAlias(true);
        //
        //            float r = size / 2f;
        //            canvas.drawCircle(r, r, r, paint);
        //
        //            source.recycle();
        //
        //            return bitmap;
        //        }
        //
        //        @Override
        //        public String key() {
        //            return "CropCircleTransformation()";
        //        }

    }

}

