using System;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public enum SearchType : int
    {
        CATEGORY = 0, 
        PLACE = 1, 
        TITLE = 2
    }

    public interface ISearchGroup 
    {
        int Id { get; }

        string Name { get; }

        SearchType Type { get; }
    }
}

