using System;
using Hdp.CoreRx.Models;
using System.Text.RegularExpressions;

namespace Hdp.CoreRx.Helpers
{
    public static class YoutubeHelper
    {
        public static string GetVideoId(string videoUrl)
        {
            var videoIdRegexp = new Regex (@"watch\?v=(.*)");
            var videoIdOpenRegexp = new Regex (@"youtu\.be\/(.*)");

            var videoIdMatch = videoIdRegexp.Match (videoUrl);
            var videoIdOpenMatch = videoIdOpenRegexp.Match (videoUrl);

            string videoId;

            if (videoIdMatch.Success)
            {
                videoId = videoIdMatch.Groups [1].Value;
            }

            else if (videoIdOpenMatch.Success)
            {
                videoId = videoIdOpenMatch.Groups [1].Value;
            }

            else
            {
                return null;
            }

            return videoId;
        }

        public static string GetThumbnailUrl(DeviceType deviceType, string videoUrl)
        {
            var videoId = GetVideoId (videoUrl);

            string thumbnailType = "mqdefault.jpg";

            switch (deviceType) {
            case DeviceType.ios:
                thumbnailType = "mqdefault.jpg";
                break;
            case DeviceType.ios2x:
                thumbnailType = "sddefault.jpg";
                break;
            case DeviceType.ios3x:
                thumbnailType = "maxresdefault.jpg";
                break;
            }

            return "http://img.youtube.com/vi/" + videoId + "/" + thumbnailType;
        }
    }
}

