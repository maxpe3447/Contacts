using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Sign
{
    public interface ISignUpService 
    {
        bool IsExist(UserModel userModel);
        Task<int> InsertUser(UserModel userModel);
        //public void Delete()
    }
}
