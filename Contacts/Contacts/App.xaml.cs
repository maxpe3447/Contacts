using Contacts.Services.Profile;
using Contacts.Services.Repository;
using Contacts.Services.Setting;
using Contacts.Services.Sign;
using Contacts.ViewModels;
using Contacts.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Contacts
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SignIn");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<ISignInService>(Container.Resolve<SignInService>());
            containerRegistry.RegisterInstance<ISignUpService>(Container.Resolve<SignUpService>());

            //Navigation
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignIn, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUp, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfile, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<ProfileImage, ProfileImageViewModel>();
            containerRegistry.RegisterForNavigation<Setting, SettingViewModel>();
        }
    }
}
