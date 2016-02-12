using System;
using Stiletto;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public class MainContentStateBrokerProvider
    {
        private static readonly MainContentStateBroker BROKER = new MainContentStateBroker();

        [Inject]
        public MainContentStateBrokerProvider() 
        {
        }

        public MainContentStateBroker Get() 
        {
            return BROKER;
        }
    }
}

