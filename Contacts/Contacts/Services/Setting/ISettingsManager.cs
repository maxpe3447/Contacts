using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contacts.Services.Setting
{
    public interface ISettingsManager
    {
        string BackgroundColor { get; set; }
        bool DarkThem { get; set; }
        bool LightThem { get; set; }

        void SetLightThem();
        void SetDarkThem();
    }
}
