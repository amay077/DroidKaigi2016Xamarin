using System;
using Refit;
using System.Threading.Tasks;
using DroidKaigi2016Xamarin.Core.Models;
using System.Collections.Generic;

namespace DroidKaigi2016Xamarin.Core.Apis
{
    interface IDroidKaigiService 
    {
//            @GET("/konifar/droidkaigi2016/master/app/src/main/res/raw/sessions_ja.json")
        [Get("/konifar/droidkaigi2016/master/app/src/main/res/raw/sessions_ja.json")]
        Task<IList<Session>> GetSessions();

    }    

    public class DroidKaigiClient
    {
        private static readonly string END_POINT = "https://raw.githubusercontent.com";
//        private static readonly string JSON_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        private readonly IDroidKaigiService service;

//        @Inject
        public DroidKaigiClient() 
        {

            service = RestService.For<IDroidKaigiService>(END_POINT);

//            Gson gson = new GsonBuilder().setDateFormat(JSON_DATE_FORMAT).create();
//            Retrofit feedburnerRetrofit = new Retrofit.Builder()
//                .client(client)
//                .baseUrl(END_POINT)
//                .addCallAdapterFactory(RxJavaCallAdapterFactory.create())
//                .addConverterFactory(GsonConverterFactory.create(gson))
//                .build();
//            service = feedburnerRetrofit.create(DroidKaigiService.class);
        }

        public Task<IList<Session>> GetSessions() 
        {
            return service.GetSessions();
        }
    }
}

