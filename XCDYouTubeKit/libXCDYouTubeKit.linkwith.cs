using System;
using ObjCRuntime;

[assembly: LinkWith ("libXCDYouTubeKit.a", LinkTarget.Arm64|LinkTarget.Simulator|LinkTarget.Simulator64, SmartLink = true, ForceLoad = true, Frameworks = "UIKit CoreVideo CoreMedia AVFoundation Foundation CoreGraphics JavaScriptCore")]
