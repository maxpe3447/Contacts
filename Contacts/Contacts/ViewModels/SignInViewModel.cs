using Contacts.Services.Repository;
using Contacts.Services.Sign;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public ICommand NavigateToCommand { get; }
        public ICommand SignInClick { get; }
        public SignInViewModel(INavigationService navigationService)
            : base(navigationService)
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

        public async void NavigateToSugnUpPage()
        {
            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contactbook.db3"));
            await NavigationService.NavigateAsync("SignUp");
        }

        public async void SignInReset()
        {
            SignInService signIn = new SignInService();
            var userModel = new Model.UserModel { Login = Login, Password = userPassword };
            if (signIn.IsExist(userModel))
            {
                userModel.Id = signIn.GetId(userModel);

                NavigationParameters keyValues = new NavigationParameters();

                keyValues.Add("AuthorId", userModel.Id);

                await NavigationService.NavigateAsync("MainList", keyValues);
                return;
            }

            bool result = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Invalid login or password!", "Alert", "Ok", " ");

            if (result)
            {
                Login = string.Empty;
                UserPassword = string.Empty;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters) 
        {
            if (parameters.ContainsKey("Login"))
            {
                Login = parameters.GetValue<string>("Login");
                UserPassword = parameters.GetValue<string>("Password");
            }
            
        }
    }
}
