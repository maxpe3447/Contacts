using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Contacts.Services.Profile
{
    class ProfileService : IProfileService
    {
        private Repository.Repository repository;
        public ProfileService()
        {
            this.repository = new Repository.Repository();
        }

        public async void DeleteProfile(ProfileModel profile)
        {
            await repository.DeleteAsync(profile);
        }

        public List<ProfileModel> GetAll(int AuthorId)
        {
            List< ProfileModel> lst =  repository.GetAllAsync<ProfileModel>().Result;
            return lst.Where(x => x.AuthorId == AuthorId).ToList();
        }

        public int InsertProfile(ProfileModel profile)
        {
            return repository.InsertAsync<ProfileModel>(profile).Result;
        }

        public  Task<int> UpdateProfile(ProfileModel profile)
        {
            return repository.UpdateAsync<ProfileModel>(profile);
        }
        /////////Temp
        public void Delete()
        {
            repository.Delete<ProfileModel>();
        }
    }
}
