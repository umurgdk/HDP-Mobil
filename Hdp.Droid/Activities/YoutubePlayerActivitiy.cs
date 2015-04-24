using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.YouTube.Player;

namespace Hdp.Droid.Activities
{
    [Activity (Label = "YoutubePlayerActivitiy", Theme = "@style/Theme.HDP")]	
    public class YoutubePlayerActivitiy : YouTubeBaseActivity, IYouTubePlayerOnFullscreenListener, IYouTubePlayerOnInitializedListener
    {
        private YouTubePlayerView _playerView;
        private IYouTubePlayer _player;

        private string _videoId;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            _videoId = Intent.GetStringExtra ("VIDEO_ID");

            _playerView = new YouTubePlayerView (this);
            _playerView.Initialize ("AIzaSyAUine_14xO0zRGibiwFgK_jLdEtQZsvpQ", this);

            SetContentView (_playerView);

            // Create your application here
        }

        public void OnFullscreen (bool p0)
        {
            
        }

        public void OnInitializationFailure (IYouTubePlayerProvider p0, YouTubeInitializationResult p1)
        {
            Finish ();
        }

        public void OnInitializationSuccess (IYouTubePlayerProvider playerProvider, IYouTubePlayer player, bool wasRestored)
        {
            _player = player;
            _player.SetOnFullscreenListener (this);

            if (!wasRestored)
            {
                player.CueVideo (_videoId);
            }
        }
    }
}

