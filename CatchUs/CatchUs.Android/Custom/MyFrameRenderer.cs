using Android.Content;
using Xamarin.Forms;
using CatchUs.Droid.Custom;
using Xamarin.Forms.Platform.Android;
using CatchUs.Custom;

[assembly: ExportRenderer(typeof(AppFrame), typeof(MyFrameRenderer))]
namespace CatchUs.Droid.Custom
{
    class MyFrameRenderer : FrameRenderer
    {
        public MyFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                //ViewGroup.SetBackgroundResource(Resource.Drawable.FrameRenderValue);
            }
        }
    }
}