using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Android.Widget;
using Akavache;
using Splat;

namespace Hdp.Droid.DroidExtensions
{
    public static class ImageViewExtensions
    {
        public static async Task LoadAndCacheFromUrl (this ImageView @this, string url)
        {
            var bitmap = await BlobCache.LocalMachine.LoadImageFromUrl (url);
            @this.SetImageDrawable (bitmap.ToNative());
        }
    }
}

