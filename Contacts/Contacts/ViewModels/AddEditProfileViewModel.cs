using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Model;
using Contacts.Services.Profile;

namespace Contacts.ViewModels
{
    public class AddEditProfileViewModel : ViewModelBase
    {
        public ICommand AddOrUpdate { get; } 
        public AddEditProfileViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            AddOrUpdate = new Command(AddUpdate);
        }

        private string toolBarButton;
        public string ToolBarButton
        {
            get { return toolBarButton; }
            set { SetProperty(ref toolBarButton, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string nickName;
        public string NickName
        {
            get { return nickName; }
            set { SetProperty(ref nickName, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private bool forAdd;
        private bool ForAdd
        {
            get => forAdd;
            set => forAdd = value;
        }

        ProfileModel profileModel;
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            profileModel = new ProfileModel
            {
                AuthorId = parameters.GetValue<int>("AuthorId")
            };
            Title = parameters.ContainsKey("Title") ? parameters.GetValue<string>("Title") : "Profile";
            ToolBarButton = parameters.ContainsKey("ToolBarButton") ? parameters.GetValue<string>("ToolBarButton") : "NaN";
            ForAdd = parameters.ContainsKey("ForAdd");
        }

        private void AddUpdate()
        {
            if (ForAdd)
            {

                profileModel.Name = Name;
                profileModel.NickName = NickName;
                profileModel.Description = Description;
                profileModel.Date = DateTime.Now.ToString();
                    //TODO IMAGE
                
                ProfileService profileService = new ProfileService();
                profileService.InsertProfile(profileModel);
            }
        }
    }
}
