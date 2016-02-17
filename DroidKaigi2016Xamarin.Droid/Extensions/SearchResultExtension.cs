using System;
using DroidKaigi2016Xamarin.Core.Models;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class SearchResultExtension
    {
        public static bool IsDescriptionType(this SearchResult self) 
        {
            return self.typeRes == Resource.String.description;
        }        
    }
}

