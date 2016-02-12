using System;
using Com.Like;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class LikeButtonExtension
    {
        public static void SetOnLikeAction(this LikeButton self, Action<LikeButton> likeHandler, Action<LikeButton> unlikeHandler)
        {
            self.SetOnLikeListener(new DelegateOnLikeListener(likeHandler, unlikeHandler));
        }

            class DelegateOnLikeListener : Java.Lang.Object, IOnLikeListener
        {
            readonly Action<LikeButton> likeHandler;
            readonly Action<LikeButton> unlikeHandler;

            public DelegateOnLikeListener(Action<LikeButton> likeHandler, Action<LikeButton> unlikeHandler)
            {
                this.likeHandler = likeHandler;    
                this.unlikeHandler = unlikeHandler;    
            }

            public void Liked(LikeButton button)
            {
                likeHandler(button);
            }

            public void UnLiked(LikeButton button)
            {
                unlikeHandler(button);
            }

        }    
    }
}

