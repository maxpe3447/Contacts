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
using Acr.UserDialogs;
using Xamarin.Essentials;
using System.IO;
using Contacts.Services.Image;
using Contacts.Services.Setting;

namespace Contacts.ViewModels
{
    public class AddEditProfileViewModel : ViewModelBase
    {
        public AddEditProfileViewModel(INavigationService navigationService,
                                       ISettingsManager settingsManager)
            : base(navigationService)
        {
            AddOrUpdate = new Command(AddUpdate);
            ImageSetCommand = new Command(ImageSet);

            this.settingsManager = settingsManager;

            SetLanguage();
        }
        #region -- Properties --
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

        private string nickPlaceholder;
        public string NickPlaceholder
        {
            get { return nickPlaceholder; }
            set { SetProperty(ref nickPlaceholder, value); }
        }

        private string namePlaceholder;
        public string NamePlaceholder
        {
            get { return namePlaceholder; }
            set { SetProperty(ref namePlaceholder, value); }
        }
        private string descriptionPlaceholder;
        public string DescriptionPlaceholder
        {
            get { return descriptionPlaceholder; }
            set { SetProperty(ref descriptionPlaceholder, value); }
        }

        public string Path { get; set;}

        private ImageSource photo;
        public ImageSource Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }
        public string BackgroundColor
        {
            get { return settingsManager.BackgroundColor; }
        }
        #endregion

        #region -- Override --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            if (!(ForAdd = parameters.ContainsKey("ForAdd")))
            {
                profileModel = parameters.GetValue<ProfileModel>("ProfileModel");

                Name = profileModel.Name;
                NickName = profileModel.NickName;
                Description = profileModel.Description;

                if (profileModel.Image != null)
                {
                    var stream = ImageService.BytesToStream(profileModel.Image);
                    Photo = profileModel.ProfileImage = ImageSource.FromStream(() => stream);
                }
            }
            else
            {
                profileModel = new ProfileModel
                {
                    AuthorId = parameters.GetValue<int>("AuthorId")
                };
                Path = "human.png";
                Photo = ImageSource.FromFile(Path);
                

            }

            SetTittle();
        }
        #endregion

        #region -- Command --
        public ICommand AddOrUpdate { get; }
        public ICommand ImageSetCommand { get; }
        private async void AddUpdate()
        {
            ProfileService profileService = new ProfileService();

            profileModel.Name = Name;
            profileModel.NickName = NickName;
            profileModel.Description = Description;
            profileModel.Date = DateTime.Now.ToString("dd.MM.yyyy h:mm tt");


            if (Path != "human.png")
                profileModel.Image = File.ReadAllBytes(Path);

            if (ForAdd)
            {
                profileService.InsertProfile(profileModel);
            }
            else
            {
                await profileService.UpdateProfile(profileModel);
            }
        }
        public async void ImageSet()
        {
            var actionResult = await UserDialogs.Instance.ActionSheetAsync(TextAddPhoto, TextCancel, null, null, TextTakeAPhoto, TextGallery);
            if (actionResult == TextTakeAPhoto)
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png",
                });
                Path = photo.FullPath;
                Photo = ImageSource.FromFile(Path);

            }
            else if (actionResult == TextGallery)
            {
                var photo = await MediaPicker.PickPhotoAsync();
                Path = photo.FullPath;
                Photo = ImageSource.FromFile(Path);
            }
        }
        #endregion

        #region -- Private --

        private string TextAddPhoto { get; set; }
        private string TextCancel { get; set; }
        private string TextTakeAPhoto { get; set; }
        private string TextGallery { get; set; }

        ISettingsManager settingsManager;

        private ProfileModel profileModel;

        private void SetLanguage()
        {
            switch (settingsManager.Language)
            {
                case "English":
                    
                    NickPlaceholder = LanguageEn.PlaceholderNickName;
                    NamePlaceholder = LanguageEn.PlaceholderName;
                    DescriptionPlaceholder = LanguageEn.PlaceholderDescription;
                    TextAddPhoto = LanguageEn.ActionSheetTextAddPhoto;
                    TextCancel = LanguageEn.TextCancel;
                    TextTakeAPhoto = LanguageEn.TextTakeAPhoto;
                    TextGallery = LanguageEn.TextGallery;
                   
                    break;
                case "Українська":
                    
                    NickPlaceholder = LanguageUa.PlaceholderNickName;
                    NamePlaceholder = LanguageUa.PlaceholderName;
                    DescriptionPlaceholder = LanguageUa.PlaceholderDescription;
                    TextAddPhoto = LanguageUa.ActionSheetTextAddPhoto;
                    TextCancel = LanguageUa.TextCancel;
                    TextTakeAPhoto = LanguageUa.TextTakeAPhoto;
                    TextGallery = LanguageUa.TextGallery;

                    break;
            }
        }

        private void SetTittle()
        {
            switch (settingsManager.Language)
            {
                case "English":
                    if (ForAdd)
                        Title = LanguageEn.HederAddProfile;
                    else
                        Title = LanguageEn.HederEditProfile;
                    break;

                case "Українська":
                    if (ForAdd)
                        Title = LanguageUa.HederAddProfile;
                    else
                        Title = LanguageUa.HederEditProfile;
                    break;
            }
        }
        #endregion
    }
}
