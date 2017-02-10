using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System;
using Android.Graphics;
using Android.Views;

namespace ColorPaletteGenerator
{
    [Activity(Label = "ColorPaletteGenerator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        PaletteFragment palette;
        ColorPickerFragment picker;
        FrameLayout paletteLayout;
        FrameLayout pickerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            InputMethodManager inputManager = (InputMethodManager)Application.Context.GetSystemService(InputMethodService);

            pickerLayout = FindViewById<FrameLayout>(Resource.Id.colorPicker);
            paletteLayout = FindViewById<FrameLayout>(Resource.Id.paletteContainer);

            palette = new PaletteFragment();
            picker = new ColorPickerFragment();

            var ft = FragmentManager.BeginTransaction();
            ft.AddToBackStack(null);
            ft.Add(Resource.Id.paletteContainer, palette, "palette_fragment");
            ft.Add(Resource.Id.colorPicker, picker, "picker_fragment");
            ft.Commit();

            HidePicker();

            paletteLayout.Click += delegate
                {
                    HidePicker();
                };
        }
        public void ShowPicker()
        {
            pickerLayout.Visibility = ViewStates.Visible;
        }
        public void HidePicker()
        {
            pickerLayout.Visibility = ViewStates.Gone;
        }
    }
}

