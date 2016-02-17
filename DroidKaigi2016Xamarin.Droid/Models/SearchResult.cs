using System;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public class SearchResult : Java.Lang.Object
    {
        public readonly string text;

        public readonly int iconRes;

        public readonly int typeRes;

        public readonly string speakerImageUrl;

        public readonly Session session;

        public SearchResult(string text, int iconRes, int typeRes, string speakerImageUrl, Session session) 
        {
            this.text = text;
            this.iconRes = iconRes;
            this.typeRes = typeRes;
            this.speakerImageUrl = speakerImageUrl;
            this.session = session;
        }    
    }
}

