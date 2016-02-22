using System;
using Android.Support.V4.App;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using MapFragment = DroidKaigi2016Xamarin.Droid.Fragments.MapFragment;
using DroidKaigi2016Xamarin.Core.Models;
using Android.OS;
using Android.Views;
using DroidKaigi2016Xamarin.Droid.Extensions;
using Android.Gms.Maps;

namespace DroidKaigi2016Xamarin.Droid.Fragments
{
    public class MapFragment : Fragment 
    {
        private static readonly string TAG = typeof(MapFragment).Name;
        public static readonly LatLng LAT_LNG_CENTER = new LatLng(35.604757, 139.683788);
        private static readonly int DEFAULT_ZOOM = 17;

        private MapFragmentBinding binding;
        private IList<PlaceMap> placeMapList;
        private IDictionary<int, Marker> markers = new Dictionary<int, Marker>();

        public static MapFragment NewInstance() 
        {
            return new MapFragment();
        }

        public override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            placeMapList = PlaceMapExtension.CreateList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
        {
            binding = MapFragmentBinding.Inflate(inflater, container, false);
            InitGoogleMap();
            HasOptionsMenu = true;
            InitBackPressed();
            return binding.Root;
        }

        private void InitBackPressed() 
        {
            binding.Root.FocusableInTouchMode = true;
            binding.Root.KeyPress += (sender, e) => 
                {
                    if (e.KeyCode == Keycode.Back && binding.mapSearchView.IsVisible) 
                    {
                        binding.mapSearchView.RevealOff();
                        e.Handled = true;
                    }
                };
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.menu_map, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId) 
            {
                case Resource.Id.item_search:
                    binding.mapSearchView.Toggle();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void InitGoogleMap() 
        {
            var mapFragment = (SupportMapFragment) ChildFragmentManager.FindFragmentById(Resource.Id.map);

            mapFragment.GetMapAsync(new OnMapReadyCallback(googleMap => 
                {
                    binding.mapSearchView.BindData(placeMapList, (_, placeMap) => 
                        {
                            LatLng latLng = new LatLng(placeMap.latitude, placeMap.longitude);
                            int duration = Resources.GetInteger(Resource.Integer.map_camera_move_mills);
                            googleMap.AnimateCamera(CameraUpdateFactory.NewLatLng(latLng), duration, null);

                            var marker = markers[placeMap.nameRes];
                            if (marker != null) 
                            {
                                marker.ShowInfoWindow();
                            }
                        });

                    binding.loadingView.Visibility = ViewStates.Gone;
                    googleMap.MapType = GoogleMap.MapTypeNormal;
                    googleMap.SetIndoorEnabled(true);
                    googleMap.BuildingsEnabled = true;
                    googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(LAT_LNG_CENTER, DEFAULT_ZOOM));
                    var mapUiSettings = googleMap.UiSettings;
                    mapUiSettings.CompassEnabled = true;
                    RenderMarkers(placeMapList, googleMap);
                }));
        }

        private void RenderMarkers(IList<PlaceMap> placeMaps, GoogleMap googleMap) 
        {
            (placeMaps as List<PlaceMap>).ForEach(placeMap => 
                {
                    var options = new MarkerOptions()
                        .SetPosition(new LatLng(placeMap.latitude, placeMap.longitude))
                        .SetTitle(GetString(placeMap.nameRes))
                        .SetIcon(BitmapDescriptorFactory.FromResource(placeMap.markerRes))
                        .SetSnippet(GetString(placeMap.buildingNameRes));
                    var marker = googleMap.AddMarker(options);
                    markers.Add(placeMap.nameRes, marker);
                });
        }

        class OnMapReadyCallback : Java.Lang.Object, IOnMapReadyCallback
        {
            private readonly Action<GoogleMap> onMapReadyHandler;

            public OnMapReadyCallback (Action<GoogleMap> onMapReadyHandler)
            {
                this.onMapReadyHandler = onMapReadyHandler;
            }

            public void OnMapReady(GoogleMap googleMap)
            {
                onMapReadyHandler(googleMap);
            }
        }

    }
}

