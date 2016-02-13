using System;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public enum SearchType 
    {
        CATEGORY, 
        PLACE, 
        TITLE
    }

    public interface ISearchGroup 
    {
        int Id { get; }

        string Name { get; }

        SearchType Type { get; }
    }
}

