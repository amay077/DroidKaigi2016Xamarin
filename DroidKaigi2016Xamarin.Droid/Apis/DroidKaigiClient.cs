using System;
using Refit;
using System.Threading.Tasks;
using DroidKaigi2016Xamarin.Core.Models;
using System.Collections.Generic;
using Stiletto;

namespace DroidKaigi2016Xamarin.Core.Apis
{
    interface IDroidKaigiService 
    {
        [Get("/konifar/droidkaigi2016/master/app/src/main/res/raw/sessions_ja.json")]
        Task<IList<Session>> GetSessionsJa();

        [Get("/konifar/droidkaigi2016/master/app/src/main/res/raw/sessions_en.json")]
        Task<IList<Session>> GetSessionsEn();
    }    

    [Singleton]
    public class DroidKaigiClient
    {
        private static readonly string END_POINT = "https://raw.githubusercontent.com";

        private readonly IDroidKaigiService service;

        [Inject]
        public DroidKaigiClient() 
        {
            service = RestService.For<IDroidKaigiService>(END_POINT);
        }

        public Task<IList<Session>> GetSessions(string languageId) 
        {
            if ("ja".Equals(languageId)) 
            {
                return service.GetSessionsJa();
            } 
            else
            {
                return service.GetSessionsEn();
            }
        }
    }
}

