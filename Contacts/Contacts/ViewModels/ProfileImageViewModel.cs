using Contacts.Services.Image;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Prism.Services;
using Rg.Plugins.Popup.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class ProfileImageViewModel : BindableBase, INavigationAware, IConfirmNavigationAsync
    {
        private INavigationService navigationService;
        public ProfileImageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;

            GoBackCommand = new Command(GoBack);
        }
        private ImageSource imageProfile;
        public ImageSource ImageProfile
        {
            get { return imageProfile; }
            set { SetProperty(ref imageProfile, value); }

        }
        public async Task<bool> CanNavigateAsync(INavigationParameters parameters)
        {
            return true;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        public ICommand GoBackCommand { get; }
        private async void  GoBack()
        {
            await navigationService.GoBackAsync();
        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("byteImage"))
            {
                var byteImage = parameters.GetValue<byte[]>("byteImage");
                if (byteImage != null)
                {
                    ImageProfile = ImageSource.FromStream(() => ImageService.BytesToStream(byteImage));
                }
                else
                {
                    ImageProfile = ImageSource.FromFile("human.png");
                }
            }
        }
    }
}
