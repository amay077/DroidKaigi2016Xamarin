using System;
using Stiletto;
using DroidKaigi2016Xamarin.Core.Models;

namespace DroidKaigi2016Xamarin.Droid.Daos
{
    [Singleton]
    public class CategoryDao
    {
        [Inject]
        public CategoryDao()
        {
        }

        public IObservable<Category> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}

