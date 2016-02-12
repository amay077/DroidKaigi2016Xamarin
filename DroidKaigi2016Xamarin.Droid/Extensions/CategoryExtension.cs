using System;
using DroidKaigi2016Xamarin.Core.Models;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class CategoryExtension
    {
        public static int GetThemeResId(this Category self) 
        {
            switch (self.id) 
            {
                case 1:
                    return Resource.Style.AppTheme_NoActionBar_Amber;
                case 2:
                    return Resource.Style.AppTheme_NoActionBar_Indigo;
                case 3:
                    return Resource.Style.AppTheme_NoActionBar_Orange;
                case 4:
                    return Resource.Style.AppTheme_NoActionBar_Pink;
                case 5:
                    return Resource.Style.AppTheme_NoActionBar_Purple;
                case 6:
                    return Resource.Style.AppTheme_NoActionBar_Teal;
                case 7:
                    return Resource.Style.AppTheme_NoActionBar_LightGreen;
                case 8:
                    return Resource.Style.AppTheme_NoActionBar_Red;
                default:
                    return Resource.Style.AppTheme_NoActionBar_Indigo;
            }
        }

        public static int GetVividColorResId(this Category self) 
        {
            switch (self.id) 
            {
                case 1:
                    return Resource.Color.amber500;
                case 2:
                    return Resource.Color.indigo500;
                case 3:
                    return Resource.Color.orange500;
                case 4:
                    return Resource.Color.pink500;
                case 5:
                    return Resource.Color.purple500;
                case 6:
                    return Resource.Color.teal500;
                case 7:
                    return Resource.Color.lightgreen500;
                case 8:
                    return Resource.Color.red500;
                default:
                    return Resource.Color.indigo500;
            }
        }

        public static int GetPaleColorResId(this Category self)  
        {
            switch (self.id) 
            {
                case 1:
                    return Resource.Color.amber500_alpha_54;
                case 2:
                    return Resource.Color.indigo500_alpha_54;
                case 3:
                    return Resource.Color.orange500_alpha_54;
                case 4:
                    return Resource.Color.pink500_alpha_54;
                case 5:
                    return Resource.Color.purple500_alpha_54;
                case 6:
                    return Resource.Color.teal500_alpha_54;
                case 7:
                    return Resource.Color.lightgreen500_alpha_54;
                case 8:
                    return Resource.Color.red500_alpha_54;
                default:
                    return Resource.Color.indigo500_alpha_54;
            }
        }        
    }
}

