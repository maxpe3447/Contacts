﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        ICommand CreateAccountCommand { get; }
        public SignUpViewModel(Prism.Navigation.INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Users SignUp";
            CreateAccountCommand = new Command(AccountCreate);
        }

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
        public void AccountCreate()
        {

            //TODO: create account

        }
        public bool Valid()
        {
            if (Login.Length < 4 || Login.Length > 16)
            {
                //UserDialogs.Instance.Alert("Your login must be more than 4 characters and less than 16 characters!", "Invalid login");
                return false;
            }
            if (char.IsDigit(Login[0]))
            {
                //UserDialogs.Instance.Alert("login must not start with a number!", "Invalid login");
                return false;
            }
            if (userPassword.Length < 8 || userPassword.Length > 16)
            {
                //UserDialogs.Instance.Alert("Your password must be more than 8 characters and less than 16 characters!", "Invalid password");
                return false;
            }
            if (userPassword != confirmUserPassword)
            {
                //UserDialogs.Instance.Alert("Your password mismatch!", "Invalid password");
                return false;
            }
            //TODO: is_exist in db
            return true;
        }
        private void SignUpButtonUnlock() =>
            SignUpIsEnable = !(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(UserPassword) || string.IsNullOrEmpty(ConfirmUserPassword));

    }
}
   