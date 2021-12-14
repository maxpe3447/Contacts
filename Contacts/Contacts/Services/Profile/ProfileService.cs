using Contacts.Model;
using Contacts.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Contacts.Services.Profile
{
    class ProfileService : IProfileService
    {
        IRepository repository;
        public ProfileService()
        {
            this.repository = new Repository.Repository();
        }

        public void DeleteProfile(ProfileModel profileModel)
        {
            throw new NotImplementedException();
        }

        public List<ProfileModel> GetAll(int AuthorId)
        {
            return  repository.GetAllAsync<ProfileModel>().Result.Where(x=>x.AuthorId == AuthorId).ToList();
        }

        public void InsertProfile(ProfileModel profileModel)
        {
            repository.InsertAsync<ProfileModel>(profileModel);
        }
    }
}
