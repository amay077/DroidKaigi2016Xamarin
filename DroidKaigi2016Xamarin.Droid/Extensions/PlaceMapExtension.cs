using System;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class PlaceMapExtension
    {
        public static IList<PlaceMap> CreateList() 
        {
            var list = new List<PlaceMap>();

            list.Add(new PlaceMap(Resource.String.map_main_name, Resource.String.map_main_building,
                Resource.Drawable.ic_place_red_500_36dp, 35.605899, 139.683541));
            list.Add(new PlaceMap(Resource.String.map_ab_name, Resource.String.map_ab_building,
                Resource.Drawable.ic_place_green_500_36dp, 35.603012, 139.684206));
            list.Add(new PlaceMap(Resource.String.map_cd_name, Resource.String.map_cd_building,
                Resource.Drawable.ic_place_blue_500_36dp, 35.603352, 139.684249));
            list.Add(new PlaceMap(Resource.String.map_party_name, Resource.String.map_party_building,
                Resource.Drawable.ic_place_purple_500_36dp, 35.607513, 139.684689));

            return list;
        }
    }
}

