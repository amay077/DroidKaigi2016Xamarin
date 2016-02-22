using System;
using Newtonsoft.Json;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public sealed class Place : ISearchGroup
    {
        public int id { get; set; }
        public string name { get; set; }

        #region ISearchGroup implementation

        [JsonIgnore]
        public int Id { get { return id; } }

        [JsonIgnore]
        public string Name { get { return name; } }

        [JsonIgnore]
        public SearchType Type { get { return SearchType.PLACE; } }

        #endregion

        public override bool Equals(object obj)
        {
            return obj is Place &&  (obj as Place).id == id || base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return id;
        }

    }
}

