using Acr.UserDialogs;
using Contacts.Model;
using Contacts.Services.Image;
using Contacts.Services.Profile;
using Contacts.Services.Setting;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class MainListViewModel : ViewModelBase
    {
        public IProfileService ProfileService {get;}
        public MainListViewModel(INavigationService navigationService,
                                 ISettingsManager settingsManager)
            : base(navigationService)
        {
            Title = "Main List";

            LogOutCommand = new Command(LogOut);
            AddProfileCommand = new Command(NavigateToAdd);
            EditProfileCommand = new Command(NavigateToEdit);
            DeleteProfileCommand = new Command(DeleteProfile);
            SettingCommand = new Command(Setting);

            ProfileService = new ProfileService();
 
            AuthorId = -1;

            this.settingsManager = settingsManager;
            BackgroundColor = settingsManager.BackgroundColor;
        }

        #region -- Properties --
        private ObservableCollection<ProfileModel> profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get { return profileList; }
            set { SetProperty(ref profileList, value); }
        }

        private object deleteParam;
        public object DeleteParam
        {
            get { return deleteParam; }
            set { SetProperty(ref deleteParam, value); }

        }

        private string noProfiles;
        public string NoProfiles
        {
            get { return noProfiles; }
            set { SetProperty(ref noProfiles, value); }
        }

        private object selectedProfile;
        public object SelectedProfile
        {
            get { return selectedProfile; }
            set
            {
                SetProperty(ref selectedProfile, value);

                NavigationParameters keyValues = new NavigationParameters();
                keyValues.Add("byteImage", (SelectedProfile as ProfileModel).Image);
                NavigationService.NavigateAsync("ProfileImage", parameters: keyValues, useModalNavigation: true);
            }
        }
        private string backgroundColor;
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty(ref backgroundColor, value); }
        }
        #endregion

        #region -- Command --

        public ICommand LogOutCommand { get; }
        public ICommand AddProfileCommand { get; }
        public ICommand EditProfileCommand { get; }
        public ICommand DeleteProfileCommand { get; }
        public ICommand SettingCommand { get; }
        private async void LogOut()
        {
            await NavigationService.GoBackAsync();
        }

        private async void NavigateToAdd()
        {
            
            NavigationParameters keyValues = new NavigationParameters();

            keyValues.Add("Title", "AddProfile");
            keyValues.Add("ForAdd", true);
            keyValues.Add("AuthorId", AuthorId);

            await NavigationService.NavigateAsync("AddEditProfile", keyValues);
        }

        private async void NavigateToEdit(object profileObj)
        {

            var currentProfile = profileObj as ProfileModel;

            NavigationParameters keyValues = new NavigationParameters();

            keyValues.Add("Title", "EditProfile");
            keyValues.Add("ForEdd", "E");
            keyValues.Add("AuthorId", AuthorId);
            keyValues.Add("ProfileModel", currentProfile);

            await NavigationService.NavigateAsync("AddEditProfile", keyValues);
        }
        private async void DeleteProfile(object profileObj)
        {
            var confirmConfig = new ConfirmConfig()
            {
                Message = "Do you want to delete this profile?",
                OkText = "Yes",
                CancelText = "Cancel"
            };

            var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
            if (confirm)
            {
                new ProfileService().DeleteProfile(profileObj as ProfileModel);

                AddNewCollection();
            }
        }

        private async void Setting()
        {
            NavigationParameters keyValues = new NavigationParameters();

            keyValues.Add("ProfileList", ProfileList);

            await NavigationService.NavigateAsync("Setting", keyValues);
        }
        #endregion

        #region -- Override -- 
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            AuthorId = parameters.ContainsKey("AuthorId") ? parameters.GetValue<int>("AuthorId") : AuthorId;
            if (parameters.ContainsKey("ProfileList"))
            {
                ProfileList = parameters.GetValue<ObservableCollection<ProfileModel>>("ProfileList");
                SetProfilesImage(ProfileList);
            }
            else
            {
                AddNewCollection();
            }
            BackgroundColor = settingsManager.BackgroundColor;
        }

        #endregion

        #region -- Private --

        private ISettingsManager settingsManager;
        private int AuthorId { get; set; }
        private void AddNewCollection()
        {
            ProfileList = new ObservableCollection<ProfileModel>(ProfileService.GetAll(AuthorId));
            SetProfilesImage(ProfileList);

            NoProfiles = ((profileList?.Count ?? 0) == 0) ? "No profiles added." : "";
        }
        private void SetProfilesImage(IEnumerable<ProfileModel> profiles)
        {
            foreach (var profile in profiles)
            {
                if (profile.Image == null)
                    profile.ProfileImage = ImageSource.FromFile("human.png");
                else
                {
                    var stream = ImageService.BytesToStream(profile.Image);

                    profile.ProfileImage = ImageSource.FromStream(() => stream);
                }
            }
        }
        #endregion
    }
}
