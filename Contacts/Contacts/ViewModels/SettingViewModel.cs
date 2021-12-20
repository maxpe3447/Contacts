﻿using Contacts.Model;
using Contacts.Services.Setting;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel(INavigationService navigationService,
                                ISettingsManager settingsManager)
            :base(navigationService)
        {
            Title = "Setting";

            LangList = new List<string> {"English", "Українська" };

            SaveAndOutCommand = new Command(SaveAndOut);

            this.settingsManager = settingsManager;

            DarkThem = settingsManager.DarkThem;
            LighThem = settingsManager.LightThem;
        }

        #region -- Command --
        public ICommand SaveAndOutCommand { get; }

        private void SaveAndOut()
        {
            if (SortByName)
            {
                profileModels = new ObservableCollection<ProfileModel>(profileModels.OrderBy(x => x.Name));
            }
            else if(SortByNickName)
            {
                profileModels = new ObservableCollection<ProfileModel>(profileModels.OrderBy(x => x.NickName));
            }
            else if (SortByDate)
            {
                profileModels = new ObservableCollection<ProfileModel>(profileModels.OrderBy(x => x.Date));
            }

            if (DarkThem)
            {
                settingsManager.SetDarkThem();
            }
            else if (LighThem)
            {
                settingsManager.SetLightThem();
            }

            NavigationParameters keyValues = new NavigationParameters();

            

            keyValues.Add("ProfileList", profileModels);

            NavigationService.GoBackAsync(keyValues);
        }
        #endregion

        #region -- Properties --
        private bool sortByName;
        public bool SortByName
        {
            get { return sortByName; }
            set { SetProperty(ref sortByName, value); }
        }

        private bool sortByNickName;
        public bool SortByNickName
        {
            get { return sortByNickName; }
            set { SetProperty(ref sortByNickName, value); }
        }

        private bool sortByDate;
        public bool SortByDate
        {
            get { return sortByDate; }
            set { SetProperty(ref sortByDate, value); }
        }

        private bool lighThem;
        public bool LighThem
        {
            get { return lighThem; }
            set 
            {
                if (DarkThem) DarkThem = !DarkThem;
                SetProperty(ref lighThem, value);
            }
        }

        private bool darkThem;
        public bool DarkThem
        {
            get { return darkThem; }
            set 
            {
                if (LighThem) LighThem = !LighThem;
                SetProperty(ref darkThem, value);               
            }
        }

        private List<string> langList;
        public List<string> LangList
        {
            get => langList;
            set => SetProperty(ref langList, value);
        }

        private string selectedLang;
        public string SelectedLang
        {
            get => selectedLang;
            set => SetProperty(ref selectedLang, value);
        }

        public string BackgroundColor
        {
            get { return settingsManager.BackgroundColor; }
        }
        #endregion

        #region -- Override --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("ProfileList"))
            {
                profileModels = parameters.GetValue<ObservableCollection<ProfileModel>>("ProfileList");
            }
        }
        #endregion

        #region -- Private --
        private ObservableCollection<ProfileModel> profileModels;

        private ISettingsManager settingsManager;
        #endregion
    }
}
