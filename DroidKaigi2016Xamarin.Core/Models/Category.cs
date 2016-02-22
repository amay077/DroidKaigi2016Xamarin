using System;
using Newtonsoft.Json;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public sealed class Category : ISearchGroup
    {
        public int id { get; set; }
        public string name { get; set; }

        #region ISearchGroup implementation

        [JsonIgnore]
        public int Id { get { return id; } }

        [JsonIgnore]
        public string Name { get { return name; } }

        [JsonIgnore]
        public SearchType Type { get { return SearchType.CATEGORY; } }

        #endregion

        public override bool Equals(object obj)
        {
            return obj is Category &&  (obj as Category).id == id || base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}

