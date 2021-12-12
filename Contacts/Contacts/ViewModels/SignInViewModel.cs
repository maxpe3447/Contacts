using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public ICommand NavigateToCommand { get; }
        public ICommand SignInClick { get; private set; }
        public SignInViewModel(INavigationService navigationService)
            :base (navigationService)
        {
            Title = "Users SignIn";

            NavigateToCommand = new Command(NavigateToSugnUpPage);
            SignInClick = new Command(SignInReset);
        }

        private string login;
        public string Login
        {
            get { return login; }
            set { SetProperty(ref login, value); }
        }

        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set { SetProperty(ref userPassword, value); }
        }

        public async void NavigateToSugnUpPage() => await NavigationService.NavigateAsync("/SignUpView", useModalNavigation: true);

        public async void SignInReset()
        {
            //bool result = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Invalid login or password!", "Alert", "Ok", " ");

            //if (result)
            //{
                Login = string.Empty;
                UserPassword = string.Empty;
            //}
        }
    }
}
