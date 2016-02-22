using System;
using System.Collections.Generic;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public class PlaceMap 
    {
        public readonly int nameRes;

        public readonly int buildingNameRes;

        public readonly int markerRes;

        public readonly double latitude;

        public readonly double longitude;

        public PlaceMap(int nameRes, int buildingNameRes, int markerRes, double latitude, double longitude) 
        {
            this.nameRes = nameRes;
            this.buildingNameRes = buildingNameRes;
            this.markerRes = markerRes;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}

