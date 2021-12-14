using Contacts.Model;
using Contacts.Services.Profile;
using Contacts.Services.Repository;
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
        public ICommand LogOutCommand { get; }
        public ICommand AddProfileCommand { get; }
        public IProfileService ProfileService {get;}
        public MainListViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main List";
            LogOutCommand = new Command(LogOut);
            AddProfileCommand = new Command(NavigateToAdd);
            ProfileService = new ProfileService();

            AuthorId = -1;
        }

        private ObservableCollection<ProfileModel> profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get { return profileList; }
            set { SetProperty(ref profileList, value); }
        }

        private async void LogOut()
        {
            await NavigationService.GoBackAsync();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            AuthorId = parameters.ContainsKey("AuthorId") ? parameters.GetValue<int>("AuthorId") : AuthorId;

            ProfileList = new ObservableCollection<ProfileModel>(ProfileService.GetAll(AuthorId));
        }

        private int AuthorId { get; set; }

        private async void NavigateToAdd()
        {
            NavigationParameters keyValues = new NavigationParameters();

            keyValues.Add("Title", "AddProfile");
            keyValues.Add("ToolBarButton", "A");
            keyValues.Add("ForAdd", "A");
            keyValues.Add("AuthorId", AuthorId); //TODO ID

            await NavigationService.NavigateAsync("AddEditProfile", keyValues);
        }
    }
}
