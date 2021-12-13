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
        public IProfileService ProfileService {get;}
        public MainListViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main List";
            LogOutCommand = new Command(LogOut);
            //ProfileList = new ObservableCollection<ProfileModel>
            //{
            //    new ProfileModel
            //    {
            //        NickName = "Nik name1",
            //        Name = "Name1",
            //        Date = DateTime.Now.ToString(),
            //        //ProfileImage = new Image().
            //    },
            //    new ProfileModel
            //    {
            //        NickName = "Nik name2",
            //        Name = "Name2",
            //        Date = DateTime.Now.ToString()
            //    }
            //};

            ProfileService = new ProfileService();
            ProfileList = new ObservableCollection<ProfileModel>(ProfileService.GetAll());
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
    }
}
