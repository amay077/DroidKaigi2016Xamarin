using System;
using Android.OS;
using Java.Interop;
using Newtonsoft.Json;

namespace DroidKaigi2016Xamarin.Droid.Utils
{
    public static class Parcels
    {
        public static IParcelable Wrap<T>(T sessions)
        {
            var json = JsonConvert.SerializeObject(sessions);
            return new ParcelableString(json);
        }

        public static T Unwrap<T>(IParcelable parcelable)
        {
            var parcelStr = parcelable as ParcelableString;
            return JsonConvert.DeserializeObject<T>(parcelStr.Text);
        }
     }

    public class ParcelableString : Java.Lang.Object, IParcelable 
    {
        public string Text { get; internal set; }

        public ParcelableString(string text)
        {
            Text = text;
        }

        #region IParcelable implementation

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
        {
            dest.WriteString(this.Text);
        }

        #endregion

        [ExportField("CREATOR")]
        public static IParcelableCreator GetCreator()
        {
            return new ParcelableCreator();
        }
    }

    // Parcelable.Creator の代わり
    public class ParcelableCreator : Java.Lang.Object, IParcelableCreator
    {
        #region IParcelableCreator implementation
        Java.Lang.Object IParcelableCreator.CreateFromParcel(Parcel source)
        {
            return new ParcelableString(source.ReadString());
        }

        Java.Lang.Object[] IParcelableCreator.NewArray(int size)
        {
            return new ParcelableString[size];
        }
        #endregion
    }
}

