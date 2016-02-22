using System;
using Stiletto;
using DroidKaigi2016Xamarin.Core.Models;
using System.Reactive;
using Akavache;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;

namespace DroidKaigi2016Xamarin.Droid.Daos
{
    [Singleton]
    public class CategoryDao
    {
        private readonly string KEY_CATEGORIES = "categories";

        private readonly IBlobCache blob = BlobCache.LocalMachine;

        [Inject]
        public CategoryDao()
        {
        }

        public IObservable<Unit> InsertAll(IList<Category> categories)
        {
            return blob.GetOrCreateObject<IList<Category>>(KEY_CATEGORIES, () => new List<Category>())
                .Select(source => 
                    {
                        return categories.Union(source);
                    })
                .SelectMany(merged => blob.InsertObject(KEY_CATEGORIES, merged));
        }        

        public IObservable<IList<Category>> FindAll()
        {
            return blob.GetOrCreateObject<IList<Category>>(KEY_CATEGORIES, () => new List<Category>());
        }
    }
}

