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
    public class PaletteFragment : Fragment
    {
        TextView EnterColor;
        LinearLayout EnterHexLayout;
        LinearLayout[] OtherColors;
        TextView[] OtherHexes;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            var rootView = inflater.Inflate(Resource.Layout.palette_fragment, container, false);

            EnterColor = rootView.FindViewById<TextView>(Resource.Id.enterColorHex);
            EnterHexLayout = rootView.FindViewById<LinearLayout>(Resource.Id.enterMainColorLayout);

            OtherColors = new LinearLayout[] {
                rootView.FindViewById<LinearLayout>(Resource.Id.color1),
                rootView.FindViewById<LinearLayout>(Resource.Id.color2),
                rootView.FindViewById<LinearLayout>(Resource.Id.color3),
                rootView.FindViewById<LinearLayout>(Resource.Id.color4),
                rootView.FindViewById<LinearLayout>(Resource.Id.color5),
                rootView.FindViewById<LinearLayout>(Resource.Id.color6),
                rootView.FindViewById<LinearLayout>(Resource.Id.color7),
                rootView.FindViewById<LinearLayout>(Resource.Id.color8),
                rootView.FindViewById<LinearLayout>(Resource.Id.color9),
                rootView.FindViewById<LinearLayout>(Resource.Id.color10),
                rootView.FindViewById<LinearLayout>(Resource.Id.color11)
            };
            OtherHexes = new TextView[]
            {
                rootView.FindViewById<TextView>(Resource.Id.hex1),
                rootView.FindViewById<TextView>(Resource.Id.hex2),
                rootView.FindViewById<TextView>(Resource.Id.hex3),
                rootView.FindViewById<TextView>(Resource.Id.hex4),
                rootView.FindViewById<TextView>(Resource.Id.hex5),
                rootView.FindViewById<TextView>(Resource.Id.hex6),
                rootView.FindViewById<TextView>(Resource.Id.hex7),
                rootView.FindViewById<TextView>(Resource.Id.hex8),
                rootView.FindViewById<TextView>(Resource.Id.hex9),
                rootView.FindViewById<TextView>(Resource.Id.hex10),
                rootView.FindViewById<TextView>(Resource.Id.hex11)
            };

            EnterColor.TextChanged += delegate
            {
                if (EnterColor.Text.Length == 6)
                {
                    Color MainColor = GetColorFromHex(EnterColor.Text);
                    EnterHexLayout.SetBackgroundColor(MainColor);
                    // Get saturation and brightness.
                    float[] hsbVals = new float[3];
                    Color.ColorToHSV(MainColor, hsbVals);


                    int count = 0;

                    float[] HigherHSV = hsbVals;
                    float[] LowerHSV = hsbVals;

                    float factor = 1f;
                    float factorDark = 0f;

                    foreach (LinearLayout layout in OtherColors)
                    {
                        if (count < 5)
                        {
                            Color newColor = new Color(lighten(MainColor, factor));
                            layout.SetBackgroundColor(newColor);
                            OtherHexes[count].Text = GetHex(newColor);
                            OtherHexes[count].SetTextColor(Color.Black);
                            factor -= 0.20f;
                        }
                        else
                        {
                            Color newColor = new Color(darken(MainColor, factorDark));
                            layout.SetBackgroundColor(newColor);
                            OtherHexes[count].Text = GetHex(newColor);
                            factorDark += 0.19f;
                        }
                        count++;
                    };

                    //   InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(InputMethodService);

                    //   inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                }
            };

            return rootView;
        }
        private Color GetColorFromHex(string text)
        {
            text = "#" + text;
            return Color.ParseColor(text);
        }
        public Color lighten(Color color, double fraction)
        {
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            red = lightenColor(red, fraction);
            green = lightenColor(green, fraction);
            blue = lightenColor(blue, fraction);
            int alpha = color.A;
            return Color.Argb(alpha, red, green, blue);
        }

        public Color darken(Color color, double fraction)
        {
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            red = darkenColor(red, fraction);
            green = darkenColor(green, fraction);
            blue = darkenColor(blue, fraction);
            int alpha = color.A;

            return Color.Argb(alpha, red, green, blue);
        }

        private int darkenColor(int color, double fraction)
        {
            return (int)Math.Max(color - (color * fraction), 0);
        }

        private int lightenColor(int color, double fraction)
        {
            return (int)Math.Min(color + (color * fraction), 255);
        }
        private string GetHex(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}",
                                 color.R,
                                 color.G,
                                 color.B);
        }
    }
}