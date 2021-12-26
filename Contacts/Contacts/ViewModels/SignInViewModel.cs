using Contacts.Services.Repository;
using Contacts.Services.Setting;
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
        public SignInViewModel(INavigationService navigationService,
                               ISettingsManager settingsManager,
                               ISignInService signInService)
            : base(navigationService)
        {
            NavigateToCommand = new Command(NavigateToSugnUpPage);
            SignInClick = new Command(SignInReset);

            this.settingsManager = settingsManager;
            this.signInService = signInService;

            BackgroundColor = settingsManager.BackgroundColor;

            SetLanguage();
        }
        #region -- Properties --

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

        private string backgroundColor;
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty(ref backgroundColor, value); }
        }

        private string loginPlaceholder;
        public string LoginPlaceholder
        {
            get { return loginPlaceholder; }
            set { SetProperty(ref loginPlaceholder, value); }
        }

        private string passwordPlaceholder;
        public string PasswordPlaceholder
        {
            get { return passwordPlaceholder; }
            set { SetProperty(ref passwordPlaceholder, value); }
        }

        private string bSignIn;
        public string BSignIn
        {
            get { return bSignIn; }
            set { SetProperty(ref bSignIn, value); }
        }
        private string lSignUp;
        public string LSignUp
        {
            get { return lSignUp; }
            set { SetProperty(ref lSignUp, value); }
        }
        
        #endregion

        #region -- Command --
        public ICommand NavigateToCommand { get; }
        public ICommand SignInClick { get; }

        public async void NavigateToSugnUpPage()
        {
            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contactbook.db3"));
            await NavigationService.NavigateAsync("SignUp");
        }

        private async void SignInReset()
        {
            //SignInService signIn = new SignInService();
            var userModel = new Model.UserModel { Login = Login, Password = userPassword };
            if (signInService.IsExist(userModel))
            {
                userModel.Id = signInService.GetId(userModel);

                NavigationParameters keyValues = new NavigationParameters();

                keyValues.Add("AuthorId", userModel.Id);

                await NavigationService.NavigateAsync("MainList", keyValues);
                return;
            }

            bool result = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(TextMsg, HederMsg, "Ok", " ");

            if (result)
            {
                Login = string.Empty;
                UserPassword = string.Empty;
            }
        }
        #endregion

        #region -- Override --
        public override void OnNavigatedTo(INavigationParameters parameters) 
        {
            if (parameters.ContainsKey("Login"))
            {
                Login = parameters.GetValue<string>("Login");
                UserPassword = parameters.GetValue<string>("Password");
            }

            BackgroundColor = settingsManager.BackgroundColor;
            SetLanguage();
        }
        #endregion

        #region -- Private -- 
        private string TextMsg { get; set; }
        private string HederMsg { get; set; }

        private ISignInService signInService;
        private ISettingsManager settingsManager;
        private void SetLanguage()
        {
            switch (settingsManager.Language)
            {
                case "English":
                    LoginPlaceholder = LanguageEn.PlaceholderLogin;
                    PasswordPlaceholder = LanguageEn.PlaceholderPassword;
                    Title = LanguageEn.HederSignIn;
                    BSignIn = LanguageEn.ButtonSignIn;
                    LSignUp = LanguageEn.RefSignUp;
                    TextMsg = LanguageEn.AlertTextInvalidPOL;
                    HederMsg = LanguageEn.AlertHederInvalidPOL;
                    break;
                case "Українська":
                    LoginPlaceholder = LanguageUa.PlaceholderLogin;
                    PasswordPlaceholder = LanguageUa.PlaceholderPassword;
                    Title = LanguageUa.HederSignIn;
                    BSignIn = LanguageUa.ButtonSignIn;
                    LSignUp = LanguageUa.RefSignUp;
                    TextMsg = LanguageUa.AlertTextInvalidPOL;
                    HederMsg = LanguageUa.AlertHederInvalidPOL;
                    break;
            }
        }
        #endregion
    }
}
