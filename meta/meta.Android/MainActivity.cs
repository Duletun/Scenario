using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Graphics;
using System.Threading.Tasks;

using Sharpnado.HorizontalListView.Droid;
using Sharpnado.HorizontalListView.Droid.Renderers.HorizontalList;
using Sharpnado.HorizontalListView.RenderedViews;
using Sharpnado.HorizontalListView.Droid.Helpers;


namespace meta.Droid
{
    [Activity(Label = "meta", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            SharpnadoInitializer.Initialize();

            XamEffects.Droid.Effects.Init();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var darkSurface = Color.ParseColor("#383838");
                Window.SetStatusBarColor(darkSurface);
            }

            Xamarin.Forms.Forms.SetFlags("Brush_Experimental");

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);


            LoadApplication(new App()); 
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}