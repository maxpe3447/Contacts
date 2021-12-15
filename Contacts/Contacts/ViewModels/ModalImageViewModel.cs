using Contacts.Services.Image;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    public class ModalImageViewModel : ViewModelBase
    {
        public ModalImageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }

        private ImageSource imageProfile;
        public ImageSource ImageProfile
        {
            get { return imageProfile; }
            set { SetProperty(ref imageProfile, value); }

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("byteImage"))
            {
                var byteImage = parameters.GetValue<byte[]>("byteImage");

                ImageProfile = ImageSource.FromStream(() => ImageService.BytesToStream(byteImage));
            }
        }
    }
}
