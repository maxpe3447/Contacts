using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.Sign
{
    interface ISign
    {
        bool IsExist(UserModel userModel);
    }
}
