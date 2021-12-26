using Contacts.Model;
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
            LangList = new List<string> {"English", "Українська" };

            SaveAndOutCommand = new Command(SaveAndOut);

            this.settingsManager = settingsManager;

            DarkThem = settingsManager.DarkThem;
            LighThem = settingsManager.LightThem;

            SetLanguage();
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

            settingsManager.Language = SelectedItem;

            NavigationParameters keyValues = new NavigationParameters();
            keyValues.Add("ProfileList", profileModels);

            NavigationService.GoBackAsync(keyValues);
        }
        #endregion

        #region -- Properties --

        private string selectedItem;
        public string SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

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

        private string lSort;
        public string LSort
        {
            get => lSort;
            set => SetProperty(ref lSort, value);
        }

        private string lbyName;
        public string LbyName
        {
            get => lbyName;
            set => SetProperty(ref lbyName, value);
        }

        private string lByNickName;
        public string LByNickName
        {
            get => lByNickName;
            set => SetProperty(ref lByNickName, value);
        }

        private string lByDate;
        public string LByDate
        {
            get => lByDate;
            set => SetProperty(ref lByDate, value);
        }

        private string lThem;
        public string LThem
        {
            get => lThem; 
            set => SetProperty(ref lThem, value);
        }

        private string lLight;
        public string LLight
        {
            get => lLight; 
            set => SetProperty(ref lLight, value);
        }

        private string lDark;
        public string LDark
        {
            get => lDark;
            set => SetProperty(ref lDark, value);
            
        }

        private string lLanguage;
        public string LLanguage
        {
            get => lLanguage;
            set => SetProperty(ref lLanguage, value);

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
            SelectedItem = settingsManager.Language;
        }
        #endregion

        #region -- Private --
        private ObservableCollection<ProfileModel> profileModels;

        private ISettingsManager settingsManager;
        private void SetLanguage()
        {
            switch (settingsManager.Language)
            {
                case "English":
                    Title = LanguageEn.HederMainList;
                    LSort = LanguageEn.LabelSort;
                    LByDate = LanguageEn.LabelByDate;
                    LByNickName = LanguageEn.LabelByNickName;
                    LbyName = LanguageEn.LabelByName;
                    LThem = LanguageEn.LabelThem;
                    LLight = LanguageEn.LabelLight;
                    LDark = LanguageEn.LabelDark;
                    LLanguage = LanguageEn.LabelLanguage;
                    break;
                case "Українська":
                    Title = LanguageUa.HederMainList;
                    LSort = LanguageUa.LabelSort;
                    LByDate = LanguageUa.LabelByDate;
                    LByNickName = LanguageUa.LabelByNickName;
                    LbyName = LanguageUa.LabelByName;
                    LThem = LanguageUa.LabelThem;
                    LLight = LanguageUa.LabelLight;
                    LDark = LanguageUa.LabelDark;
                    LLanguage = LanguageUa.LabelLanguage;

                    break;
            }
        }
        #endregion
    }
}
