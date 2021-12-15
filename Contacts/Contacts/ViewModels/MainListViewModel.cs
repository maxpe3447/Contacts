using Acr.UserDialogs;
using Contacts.Model;
using Contacts.Services.Image;
using Contacts.Services.Profile;
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
    public class MainListViewModel : ViewModelBase
    {
        public IProfileService ProfileService {get;}
        public MainListViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main List";

            LogOutCommand = new Command(LogOut);
            AddProfileCommand = new Command(NavigateToAdd);
            EditProfileCommand = new Command(NavigateToEdit);
            DeleteProfileCommand = new Command(DeleteProfile);

            ProfileService = new ProfileService();
 
            AuthorId = -1;
        }

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

        private void AddNewCollection()
        {
            var lst = ProfileService.GetAll(AuthorId);
            foreach (var profile in lst)
            {
                if (profile.Image == null)
                    profile.ProfileImage = ImageSource.FromResource("Contacts.human.png");
                else
                {
                    var stream = ImageService.BytesToStream(profile.Image);

                    profile.ProfileImage = ImageSource.FromStream(()=>stream);

                }
            }

            ProfileList = new ObservableCollection<ProfileModel>(lst);
        }

        #region -- Override -- 
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            AuthorId = parameters.ContainsKey("AuthorId") ? parameters.GetValue<int>("AuthorId") : AuthorId;

            AddNewCollection();
        }
        #endregion




        #region -- Command --

        public ICommand LogOutCommand { get; }
        public ICommand AddProfileCommand { get; }
        public ICommand EditProfileCommand { get; }
        public ICommand DeleteProfileCommand { get; }
        private async void LogOut()
        {
            await NavigationService.GoBackAsync();
        }

        private async void NavigateToAdd()
        {
            
            NavigationParameters keyValues = new NavigationParameters();

            keyValues.Add("Title", "AddProfile");
            keyValues.Add("ToolBarButton", "A");
            keyValues.Add("ForAdd", "A");
            keyValues.Add("AuthorId", AuthorId);

            await NavigationService.NavigateAsync("AddEditProfile", keyValues);
        }

        private async void NavigateToEdit(object profileObj)
        {

            var currentProfile =profileObj as ProfileModel;

            NavigationParameters keyValues = new NavigationParameters();

            keyValues.Add("Title", "EditProfile");
            keyValues.Add("ToolBarButton", "E");
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
        #endregion
        private int AuthorId { get; set; }
    }
}
