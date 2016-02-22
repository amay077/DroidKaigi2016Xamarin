using System;
using System.Collections.Generic;

namespace DroidKaigi2016Xamarin.Core.Models
{
    public class Sponsor
    {
        public string ImageUrl { get; }

        public string Url  { get; }

        private Sponsor(String imageUrl, String url) 
        {
            this.ImageUrl = imageUrl;
            this.Url = url;
        }

        public static IList<Sponsor> CreatePlatinumList() 
        {
            var list = new List<Sponsor>();
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/mixi.png", "https://mixi.co.jp"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/findjob.png", "http://www.find-job.net"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/mixiagent.png", "http://mixi-agent.jp"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/google.png", "https://developers.google.com/"));
            return list;
        }

        public static IList<Sponsor> CreateVideoList() 
        {
            var list = new List<Sponsor>();
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/smartnews.png", "http://about.smartnews.com"));
            return list;
        }

        public static List<Sponsor> CreateFoodsList() 
        {
            var list = new List<Sponsor>();
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/sansan.png", "https://www.sansan.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/mercari.png", "https://www.mercari.com/jp"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/yj.png", "http://www.yahoo.co.jp"));
            return list;
        }

        public static List<Sponsor> CreateNormalList() 
        {
            var list = new List<Sponsor>();
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/gmo_pepabo.png", "http://pepabo.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/seesaa.png", "http://www.seesaa.co.jp"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/cookpad.png", "https://info.cookpad.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/zaim.png", "http://zaim.net"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/deploygate.png", "https://deploygate.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/fril.png", "https://fablic.co.jp"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/caraquri.png", "http://caraquri.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/goodpatch.png", "http://goodpatch.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/uphyca.png", "http://www.uphyca.com"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/gamewith.png", "http://gamewith.co.jp"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/gunosy.png", "https://gunosy.co.jp/"));
            list.Add(new Sponsor("https://droidkaigi.github.io/2016/images/logo/nikkei.png", "https://s.nikkei.com/saiyo"));
            return list;
        }
    }
}

