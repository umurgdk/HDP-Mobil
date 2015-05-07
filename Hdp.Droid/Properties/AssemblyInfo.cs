using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle ("Hdp.Droid")]
[assembly: AssemblyDescription ("")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("")]
[assembly: AssemblyProduct ("")]
[assembly: AssemblyCopyright ("Umur Gedik")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("tr-TR")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion ("1.0.0")]

// This will prevent other apps on the device from receiving GCM messages for this app
// It is crucial that the package name does not start with an uppercase letter - this is forbidden by Android.
[assembly: Permission(Name = "org.hdp.hdp.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "org.hdp.hdp.permission.C2D_MESSAGE")]

// Gives the app permission to register and receive messages.
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

// This permission is necessary only for Android 4.0.3 and below.
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]

// Need to access the internet for GCM
[assembly: UsesPermission(Name = "android.permission.INTERNET")]

// Needed to keep the processor from sleeping when a message arrives
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

[assembly: UsesPermission(Name = "android.permission.RECEIVE_BOOT_COMPLETED")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

