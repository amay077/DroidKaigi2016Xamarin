using System;
using System.Reactive.Subjects;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public class MainContentStateBroker
    {
        private readonly ISubject<Page> bus = new Subject<Page>();

        public void Set(Page page) 
        {
            bus.OnNext(page);
        }

        public IObservable<Page> Observe() 
        {
            return bus;
        }
    }
}

