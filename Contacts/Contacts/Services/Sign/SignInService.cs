using Contacts.Model;
using Contacts.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contacts.Services.Sign
{
    class SignInService : ISign
    {
        private Repository.Repository repository;
        public SignInService()
        {
            repository = new Repository.Repository(); ;
        }
        public bool IsExist(UserModel userModel)
        {
            return (GetUsers()?.Where(x => x.Login == userModel.Login && x.Password == userModel.Password).FirstOrDefault()?.Id ?? 0) != 0;
        }
        private List<UserModel> GetUsers()
        {
            var lst = repository.GetAllAsync<UserModel>().Result;
            return lst;
        }
    }
}
