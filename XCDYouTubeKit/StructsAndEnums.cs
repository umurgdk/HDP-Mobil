using System;

namespace XCDYouTubeKit
{
    using ObjCRuntime;

    [Native]
    public enum XCDYouTubeVideoQuality : ulong
    {
        Small240 = 36,
        Medium360 = 18,
        Hd720 = 22,
        Hd1080 = 37
    }

    [Native]
    public enum XCDYouTubeErrorCode : long
    {
        NoStreamAvailable = -2,
        Network = -1,
        InvalidVideoIdentifier = 2,
        RemovedVideo = 100,
        RestrictedPlayback = 150
    }

}

