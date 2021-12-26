using Acr.UserDialogs;
using Contacts.Model;
using Contacts.Services.Setting;
using Contacts.Services.Sign;
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
    public class SignUpViewModel : ViewModelBase
    {
        public SignUpViewModel(INavigationService navigationService,
                               ISettingsManager settingsManager,
                               ISignUpService signUpService)
            :base(navigationService)
        {
            CreateAccountCommand = new Command(AccountCreate);

            this.signUpService = signUpService;
            this.settingsManager = settingsManager;

            SetLanguage();
        }

        #region -- Properties -- 

        private string login;
        public string Login
        {
            get { return login; }
            set { SetProperty(ref login, value); SignUpButtonUnlock(); }
        }

        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set { SetProperty(ref userPassword, value); SignUpButtonUnlock(); }
        }

        private string confirmUserPassword;
        public string ConfirmUserPassword
        {
            get { return confirmUserPassword; }
            set { SetProperty(ref confirmUserPassword, value); SignUpButtonUnlock(); }
        }
        private bool signUpIsEnable;
        public bool SignUpIsEnable
        {
            get { return signUpIsEnable; }
            set { SetProperty(ref signUpIsEnable, value); }
        }

        private string backgroundColor;
        public string BackgroundColor
        {
            get { return settingsManager.BackgroundColor; }
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

        private string bSignUp;
        public string BSignUp
        {
            get { return bSignUp; }
            set { SetProperty(ref bSignUp, value); }
        }
        private string confirmPasswordPlaceholder;
        public string ConfirmPasswordPlaceholder
        {
            get { return confirmPasswordPlaceholder; }
            set { SetProperty(ref confirmPasswordPlaceholder, value); }
        }
        #endregion

        #region -- Command --
        public ICommand CreateAccountCommand { get; }
        public void AccountCreate()
        {
            if (!Valid())
            {
                return;
            }
            var user = new UserModel { Login = Login, Password = UserPassword };
            if (!signUpService.IsExist(user))
            {
                user.Id = signUpService.InsertUser(user).Result;

                NavigationParameters keyValues = new NavigationParameters();

                keyValues.Add("Login", user.Login);
                keyValues.Add("Password", user.Password);
                keyValues.Add("Id", user.Id);


                NavigationService.GoBackAsync(keyValues);
                return;
            }
            UserDialogs.Instance.Alert("This login is exist!");

        }

        #endregion
        
        #region -- Private -- 

        private ISettingsManager settingsManager;
        private ISignUpService signUpService;
        private string HeaderAlertInvalidLogin { get; set; }
        private string HeaderAlertInvalidPassword { get; set; }
        private string TextAlertLemitLogin { get; set; }
        private string TextAlertLoginNum { get; set; }
        private string TextAlertPasswordValid { get; set; }
        private string TextAlertPasswordDifferent { get; set; }
        private void SignUpButtonUnlock() =>
            SignUpIsEnable = !(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(UserPassword) || string.IsNullOrEmpty(ConfirmUserPassword));
        private bool Valid()
        {
            if (string.IsNullOrEmpty(Login) || Login.Length < 4 || Login.Length > 16)
            {
                UserDialogs.Instance.Alert(TextAlertLemitLogin, HeaderAlertInvalidLogin);
                return false;
            }
            if (char.IsDigit(Login[0]))
            {
                UserDialogs.Instance.Alert(TextAlertLoginNum, HeaderAlertInvalidLogin);
                return false;
            }
            if ( string.IsNullOrEmpty(UserPassword) || UserPassword.Length < 8 || UserPassword.Length > 16)
            {
                UserDialogs.Instance.Alert(TextAlertPasswordValid, HeaderAlertInvalidPassword);
                return false;
            }
            if (UserPassword != ConfirmUserPassword)
            {
                UserDialogs.Instance.Alert(TextAlertPasswordDifferent, HeaderAlertInvalidPassword);
                return false;
            }
           
            return true;
        }

        private void SetLanguage()
        {
            switch (settingsManager.Language)
            {
                case "English":
                    LoginPlaceholder = LanguageEn.PlaceholderLogin;
                    PasswordPlaceholder = LanguageEn.PlaceholderPassword;
                    Title = LanguageEn.HederSignUp;
                    BSignUp = LanguageEn.ButtonSignUp;
                    HeaderAlertInvalidLogin = LanguageEn.AlertHeaderInvalidLogin;
                    HeaderAlertInvalidPassword = LanguageEn.AlertHeaderInvalidPassword;
                    TextAlertLemitLogin = LanguageEn.AlertTextLemitLogin;
                    TextAlertLoginNum = LanguageEn.AlertTextLoginNum;
                    TextAlertPasswordValid = LanguageEn.AlertTextPasswordValid;
                    TextAlertPasswordDifferent = LanguageEn.AlertTextPasswordDifferent;
                    ConfirmPasswordPlaceholder = LanguageEn.PlaceholderConfirmPassword;
                    break;
                case "Українська":
                    LoginPlaceholder = LanguageUa.PlaceholderLogin;
                    PasswordPlaceholder = LanguageUa.PlaceholderPassword;
                    Title = LanguageUa.HederSignUp;
                    BSignUp = LanguageUa.ButtonSignUp;
                    HeaderAlertInvalidLogin = LanguageUa.AlertHeaderInvalidLogin;
                    HeaderAlertInvalidPassword = LanguageUa.AlertHeaderInvalidPassword;
                    TextAlertLemitLogin = LanguageUa.AlertTextLemitLogin;
                    TextAlertLoginNum = LanguageUa.AlertTextLoginNum;
                    TextAlertPasswordValid = LanguageUa.AlertTextPasswordValid;
                    TextAlertPasswordDifferent = LanguageUa.AlertTextPasswordDifferent;
                    ConfirmPasswordPlaceholder = LanguageUa.PlaceholderConfirmPassword;
                    break;
            }
        }
        #endregion
    }
}
   