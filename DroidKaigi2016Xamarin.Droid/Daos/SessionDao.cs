﻿using System;
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

        [Inject]
        public SessionDao()
        {
        }

        public IObservable<Unit> InsertAll(IList<Session> sessions)
        {
            return blob.GetOrCreateObject<IList<Session>>(KEY_SESSIONS, () => new List<Session>())
                .Select(source => 
                    {
                        return sessions.Union(source);
                    })
                .SelectMany(merged => blob.InsertObject(KEY_SESSIONS, merged));
        }


//        public void insertAll(@NonNull List<Session> sessions) {
//            orma.transactionAsync(new TransactionTask() {
//                @Override
//                public void execute() throws Exception {
//                    for (Session session : sessions) {
//                        session.prepareSave();
//                        insertSpeaker(session.speaker);
//                        insertCategory(session.category);
//                        insertPlace(session.place);
//                    }
//
//                    sessionRelation().inserter().executeAll(sessions);
//                }
//            });
//        }
//
//        private void insertSpeaker(Speaker speaker) {
//            if (speaker != null && speakerRelation().selector().idEq(speaker.id).count() == 0) {
//                speakerRelation().inserter().execute(speaker);
//            }
//        }
//
//        private void insertPlace(Place place) {
//            if (place != null && placeRelation().selector().idEq(place.id).count() == 0) {
//                placeRelation().inserter().execute(place);
//            }
//        }
//
//        private void insertCategory(Category category) {
//            if (category != null && categoryRelation().selector().idEq(category.id).count() == 0) {
//                categoryRelation().inserter().execute(category);
//            }
//        }
//
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
                        return sessions.Union(source);
                    })
                .SelectMany(merged => blob.InsertObject(KEY_SESSIONS, merged));
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

