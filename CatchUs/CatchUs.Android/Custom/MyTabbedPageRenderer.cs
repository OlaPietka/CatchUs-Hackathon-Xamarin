using Android.Content;
using Android.Support.Design.Internal;
using Android.Views;
using CatchUs.Droid.Custom;
using CatchUs.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(AppTabbedPage), typeof(MyTabbedPageRenderer))]
namespace CatchUs.Droid.Custom
{
    public class MyTabbedPageRenderer : TabbedPageRenderer
    {
        public MyTabbedPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (ViewGroup != null && ViewGroup.ChildCount > 0)
            {
                BottomNavigationMenuView bottomNavigationMenuView = FindChildOfType<BottomNavigationMenuView>(ViewGroup);

                if (bottomNavigationMenuView != null)
                {
                    var shiftMode = bottomNavigationMenuView.Class.GetDeclaredField("mShiftingMode");

                    shiftMode.Accessible = true;
                    shiftMode.SetBoolean(bottomNavigationMenuView, false);
                    shiftMode.Accessible = false;
                    shiftMode.Dispose();

                    for (var i = 0; i < bottomNavigationMenuView.ChildCount; i++)
                    {
                        var item = bottomNavigationMenuView.GetChildAt(i) as BottomNavigationItemView;
                        if (item == null) continue;

                        item.SetShiftingMode(false);
                        item.SetPadding(0, 26, 0, 0);
                        item.SetChecked(item.ItemData.IsChecked);
                    }

                    if (bottomNavigationMenuView.ChildCount > 0) bottomNavigationMenuView.UpdateMenuView();
                }
            }
        }

        private T FindChildOfType<T>(ViewGroup viewGroup) where T : Android.Views.View
        {
            if (viewGroup == null || viewGroup.ChildCount == 0) return null;

            for (var i = 0; i < viewGroup.ChildCount; i++)
            {
                var child = viewGroup.GetChildAt(i);

                var typedChild = child as T;
                if (typedChild != null) return typedChild;

                if (!(child is ViewGroup)) continue;

                var result = FindChildOfType<T>(child as ViewGroup);

                if (result != null) return result;
            }
            return null;
        }
    }
}
