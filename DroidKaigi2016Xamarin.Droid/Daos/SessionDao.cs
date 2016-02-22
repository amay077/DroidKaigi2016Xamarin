using System;
using Stiletto;
using Akavache;
using System.Collections.Generic;
using DroidKaigi2016Xamarin.Core.Models;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;

namespace DroidKaigi2016Xamarin.Droid.Daos
{
    /// <summary>
    /// すごく手抜きです
    /// </summary>
    [Singleton]
    public class SessionDao
    {
        private readonly string KEY_SESSIONS = "sessions";

        private readonly IBlobCache blob = BlobCache.LocalMachine;

        private readonly CategoryDao categoryDao = new CategoryDao();
        private readonly PlaceDao placeDao = new PlaceDao();

        [Inject]
        public SessionDao()
        {
        }

        public IObservable<Unit> InsertAll(IList<Session> sessions)
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(source => 
                    {
                        var checkedSessions = sessions.Where(session => session.IsChecked).ToDictionary(session => session.id);
                        var merged = sessions.Union(source);

                        return merged.Select(session => 
                            {
                                session.IsChecked = checkedSessions.ContainsKey(session.id);
                                return session;
                            });
                    })
                .SelectMany(merged => 
                    { 
                        return Observable.Merge(
                            merged.ToObservable()
                                .Select(session => session.category)
                                .ToList().Distinct()
                                .SelectMany(categoryDao.InsertAll),
                            merged.ToObservable()
                                .Select(session => session.place)
                                .ToList().Distinct()
                                .SelectMany(placeDao.InsertAll),
                            blob.InsertObject(KEY_SESSIONS, merged)
                        );
                    });
        }

        public IObservable<IList<Session>> FindAll() 
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>());
        }

        public IObservable<IList<Session>> FindByChecked() 
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(sessions => sessions.Where(s => s.IsChecked).ToList());
        }

        public IObservable<IList<Session>> FindByPlace(int placeId) 
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(sessions => sessions.Where(s => placeId == s?.place?.id).ToList());
        }

        public IObservable<IList<Session>> FindByCategory(int categoryId) 
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(sessions => sessions.Where(s => categoryId == s?.category?.id).ToList());
        }

        public IObservable<Unit> DeleteAll() 
        {
            return blob.InvalidateObject<IList<Session>>(KEY_SESSIONS);
        }

        public IObservable<Unit> UpdateAll(IList<Session> sessions) 
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(source => 
                    {
                        var checkedSessions = sessions.Where(session => session.IsChecked).ToDictionary(session => session.id);
                        var merged = sessions.Union(source);

                        return merged.Select(session => 
                            {
                                session.IsChecked = checkedSessions.ContainsKey(session.id);
                                return session;
                            });
                    })
                .SelectMany(merged => 
                    { 
                        return Observable.Merge(
                            merged.ToObservable()
                            .Select(session => session.category)
                            .GroupBy(x => x.Id).SelectMany(x => x.Take(1))
                            .ToList()
                            .SelectMany(categoryDao.InsertAll),
                            merged.ToObservable()
                            .Select(session => session.place)
                            .GroupBy(x => x.Id).SelectMany(x => x.Take(1))
                            .ToList()
                            .SelectMany(placeDao.InsertAll),
                            blob.InsertObject(KEY_SESSIONS, merged)
                        );
                    });
        }
//
//        private void update(Session session) {
//            Session_Updater updater = sessionRelation().updater()
//                .idEq(session.id)
//                .title(session.title)
//                .description(session.description)
//                .speakerId(session.speaker.id)
//                .stime(session.stime)
//                .etime(session.etime)
//                .placeId(session.place.id)
//                .languageId(session.languageId)
//                .slideUrl(session.slideUrl);
//
//            if (session.category != null) {
//                updater.categoryId(session.category.id);
//            }
//
//            updater.execute();
//        }
//
        public IObservable<Unit> UpdateChecked(Session session) 
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(source => 
                    {
                        var target = source.FirstOrDefault(s => s.id == session.id);
                        if (target != null) 
                        {
                            target.IsChecked = session.IsChecked;   
                        }

                        return source;
                    })
                .SelectMany(updated => blob.InsertObject(KEY_SESSIONS, updated));
        }
    }
}

