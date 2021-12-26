using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.Sign
{
    public interface ISignInService 
    {
        bool IsExist(UserModel userModel);
        int GetId(UserModel userModel);
    }
}
