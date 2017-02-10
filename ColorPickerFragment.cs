using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace ColorPaletteGenerator
{
    public class ColorPickerFragment : Fragment
    {
        View ColorPreview;

        TextView ColorHexPreview;
        TextView ColorRGBPreview;
        TextView RedPreview;
        TextView GreenPreview;
        TextView BluePreview;

        SeekBar RedBar;
        SeekBar BlueBar;
        SeekBar GreenBar;

        int[] colorRGB = new int[3];

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.color_picker, container, false);

            ColorPreview = rootView.FindViewById<View>(Resource.Id.colorPreview);

            ColorHexPreview = rootView.FindViewById<TextView>(Resource.Id.colorHexPreview);
            ColorRGBPreview = rootView.FindViewById<TextView>(Resource.Id.colorRGBPreview);
            RedPreview = rootView.FindViewById<TextView>(Resource.Id.redPreview);
            GreenPreview = rootView.FindViewById<TextView>(Resource.Id.greenPreview);
            BluePreview = rootView.FindViewById<TextView>(Resource.Id.bluePreview);

            RedBar = rootView.FindViewById<SeekBar>(Resource.Id.seekBarRed);
            BlueBar = rootView.FindViewById<SeekBar>(Resource.Id.seekBarBlue);
            GreenBar = rootView.FindViewById<SeekBar>(Resource.Id.seekBarGreen);

            RedBar.Max = 255;
            BlueBar.Max = 255;
            GreenBar.Max = 255;

            RedBar.ProgressChanged += delegate
            {
                colorRGB[0] = RedBar.Progress;
                RedPreview.SetTextColor(Color.Rgb(colorRGB[0], 0, 0));
                UpdateViews(colorRGB);
            };

            GreenBar.ProgressChanged += delegate
            {
                colorRGB[1] = GreenBar.Progress;
                GreenPreview.SetTextColor(Color.Rgb(0, colorRGB[1], 0));
                UpdateViews(colorRGB);
            };

            BlueBar.ProgressChanged += delegate
            {
                colorRGB[2] = BlueBar.Progress;
                BluePreview.SetTextColor(Color.Rgb(0, 0, colorRGB[2]));
                UpdateViews(colorRGB);
            };

            

            return rootView;
        }
        private void UpdateViews(int[] colors)
        {
            Color color = Color.Rgb(colors[0], colors[1], colors[2]);
            StringBuilder textRGB = new StringBuilder();
            textRGB.Append(colors[0]);
            textRGB.Append(", ");
            textRGB.Append(colors[1]);
            textRGB.Append(", ");
            textRGB.Append(colors[2]);
            textRGB.Append(", ");

            StringBuilder textHex = new StringBuilder();
            textHex.Append("#");
            textHex.Append(colors[0].ToString("X"));
            textHex.Append(colors[1].ToString("X"));
            textHex.Append(colors[2].ToString("X"));

            ColorRGBPreview.Text = textRGB.ToString();
            ColorHexPreview.Text = textHex.ToString();

            ColorPreview.SetBackgroundColor(color);
        }
    }
}