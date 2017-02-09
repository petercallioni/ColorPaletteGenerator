using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System;
using Android.Graphics;

namespace ColorPaletteGenerator
{
    [Activity(Label = "ColorPaletteGenerator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Add(Resource.Id.paletteContainer, new PaletteFragment(), "palette_fragment");
            ft.Commit();
        }
    }
}

