﻿using System;
using Stiletto;
using DroidKaigi2016Xamarin.Core.Models;

namespace DroidKaigi2016Xamarin.Droid.Daos
{
    [Singleton]
    public class PlaceDao
    {
        [Inject]
        public PlaceDao()
        {
        }

        public IObservable<Place> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}

