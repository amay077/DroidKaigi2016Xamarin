using System;
using Stiletto;
using DroidKaigi2016Xamarin.Core.Models;
using Akavache;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DroidKaigi2016Xamarin.Droid.Daos
{
    [Singleton]
    public class PlaceDao
    {
        private readonly string KEY_PLACES = "places";

        private readonly IBlobCache blob = BlobCache.LocalMachine;

        [Inject]
        public PlaceDao()
        {
        }

        public IObservable<Unit> InsertAll(ICollection<Place> places)
        {
            return blob.GetOrCreateObject<IList<Place>>(KEY_PLACES, () => new List<Place>())
                .Select(source => 
                    {
                        return places.Union(source);
                    })
                .SelectMany(merged => blob.InsertObject(KEY_PLACES, merged));
        }        

        public IObservable<IList<Place>> FindAll()
        {
            return blob.GetOrCreateObject<IList<Place>>(KEY_PLACES, () => new List<Place>());
        }
    }
}

