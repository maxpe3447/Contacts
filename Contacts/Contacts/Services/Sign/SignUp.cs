﻿using Contacts.Model;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Sign
{
    class SignUp
    {
        private Repository.Repository repository;
        public SignUp()
        {
            repository = new Repository.Repository();
        }
        public Task<int> InsertUser(UserModel userModel)
        {
            return repository.InsertAsync<UserModel>(userModel);
        }
        public bool IsExist(UserModel userModel)
        {
            return (GetUsers()?.Where(x => x.Login == userModel.Login).FirstOrDefault()?.Id ?? 0) !=0;
        }
        private List<UserModel> GetUsers()
        {
            var lst = repository.GetAllAsync<UserModel>().Result;
            return lst;
        }
    }
}
