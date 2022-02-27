using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace CameraXamarin.Droid
{
  [Application(Debuggable = true)]
  public class MainApplication : Application
  {

    public MainApplication(IntPtr handle , JniHandleOwnership transe)
      :base(handle,transe)
    {

    }

    public override void OnCreate()
    {
      base.OnCreate();
      CrossCurrentActivity.Current.Init(this);
    }
  }
}
