using System;
using Newtonsoft.Json;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public sealed class Session
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Speaker speaker { get; set; }
        public DateTime stime { get; set; }
        public DateTime etime { get; set; }
        public Category category { get; set; }
        public Place place { get; set; }
        public string language_id { get; set; }
        public string slide_url { get; set; }    

        public bool IsChecked { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Session &&  (obj as Session).id == id || base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}

