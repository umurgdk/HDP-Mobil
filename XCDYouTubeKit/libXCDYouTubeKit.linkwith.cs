using System;
using ObjCRuntime;

[assembly: LinkWith ("libXCDYouTubeKit.a", LinkTarget.ArmV7 | LinkTarget.Arm64, SmartLink = true, ForceLoad = true, Frameworks = "UIKit CoreVideo CoreMedia AVFoundation Foundation CoreGraphics JavaScriptCore")]
