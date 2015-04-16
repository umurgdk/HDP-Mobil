using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using MediaPlayer;

namespace XCDYouTubeKit
{
    
    partial interface Constants
    {
        // extern NSString *const XCDYouTubeVideoQualityHTTPLiveStreaming;
        [Field ("XCDYouTubeVideoQualityHTTPLiveStreaming")]
        NSString XCDYouTubeVideoQualityHTTPLiveStreaming { get; }

        // extern NSString *const XCDYouTubeVideoErrorDomain;
        [Field ("XCDYouTubeVideoErrorDomain")]
        NSString XCDYouTubeVideoErrorDomain { get; }
    }

    partial interface Constants
    {
        // extern NSString *const XCDMoviePlayerPlaybackDidFinishErrorUserInfoKey;
        [Field ("XCDMoviePlayerPlaybackDidFinishErrorUserInfoKey")]
        NSString XCDMoviePlayerPlaybackDidFinishErrorUserInfoKey { get; }

        // extern NSString *const XCDYouTubeVideoPlayerViewControllerDidReceiveVideoNotification;
        [Field ("XCDYouTubeVideoPlayerViewControllerDidReceiveVideoNotification")]
        NSString XCDYouTubeVideoPlayerViewControllerDidReceiveVideoNotification { get; }

        // extern NSString *const XCDYouTubeVideoUserInfoKey;
        [Field ("XCDYouTubeVideoUserInfoKey")]
        NSString XCDYouTubeVideoUserInfoKey { get; }
    }

    // @interface XCDYouTubeVideoPlayerViewController : MPMoviePlayerViewController
    [BaseType (typeof(MPMoviePlayerViewController))]
    interface XCDYouTubeVideoPlayerViewController
    {
        // -(instancetype)initWithVideoIdentifier:(NSString *)videoIdentifier __attribute__((objc_designated_initializer));
        [Export ("initWithVideoIdentifier:")]
        IntPtr Constructor (string videoIdentifier);

        // @property (copy, nonatomic) NSString * videoIdentifier;
        [Export ("videoIdentifier")]
        string VideoIdentifier { get; set; }

        // @property (copy, nonatomic) NSArray * preferredVideoQualities;
        [Export ("preferredVideoQualities", ArgumentSemantic.Copy)]
        NSObject[] PreferredVideoQualities { get; set; }

        // -(void)presentInView:(UIView *)view;
        [Export ("presentInView:")]
        void PresentInView (UIView view);
    }

    partial interface Constants
    {
        // extern NSString *const XCDYouTubeVideoPlayerViewControllerDidReceiveMetadataNotification;
        [Field ("XCDYouTubeVideoPlayerViewControllerDidReceiveMetadataNotification")]
        NSString XCDYouTubeVideoPlayerViewControllerDidReceiveMetadataNotification { get; }

        // extern NSString *const XCDMetadataKeyTitle;
        [Field ("XCDMetadataKeyTitle")]
        NSString XCDMetadataKeyTitle { get; }

        // extern NSString *const XCDMetadataKeySmallThumbnailURL;
        [Field ("XCDMetadataKeySmallThumbnailURL")]
        NSString XCDMetadataKeySmallThumbnailURL { get; }

        // extern NSString *const XCDMetadataKeyMediumThumbnailURL;
        [Field ("XCDMetadataKeyMediumThumbnailURL")]
        NSString XCDMetadataKeyMediumThumbnailURL { get; }

        // extern NSString *const XCDMetadataKeyLargeThumbnailURL;
        [Field ("XCDMetadataKeyLargeThumbnailURL")]
        NSString XCDMetadataKeyLargeThumbnailURL { get; }
    }

}

