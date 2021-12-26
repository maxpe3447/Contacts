using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.Services.Setting
{
    public class SettingsManager : ISettingsManager
    {
        public string BackgroundColor
        { 
            get => Preferences.Get(nameof(BackgroundColor), "#fafafa");
            set => Preferences.Set(nameof(BackgroundColor), value); 
        }
        public bool DarkThem { 
            get => Preferences.Get(nameof(DarkThem), false); 
            set => Preferences.Set(nameof(DarkThem), value); 
        }
        public bool LightThem { 
            get => Preferences.Get(nameof(LightThem), true);
            set => Preferences.Set(nameof(LightThem), value); 
        }
        public string Language
        {
            get => Preferences.Get(nameof(Language), "English");
            set => Preferences.Set(nameof(Language), value);
        }
        public void SetDarkThem()
        {
            DarkThem = true;
            LightThem = false;

            BackgroundColor = "#9e9e9e";
        }

        public void SetLightThem()
        {
            LightThem = true;
            DarkThem = false;

            BackgroundColor = "#fafafa";
        }
    }
}
