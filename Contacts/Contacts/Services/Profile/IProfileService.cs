using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Profile
{
    public interface IProfileService
    {
        List<ProfileModel> GetAll();
        void InsertProfile(ProfileModel profileModel);
        void DeleteProfile(ProfileModel profileModel);
    }
}
