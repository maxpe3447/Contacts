using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Model
{
    public interface IUserBase
    {
        string Login { get; set; }
        string Password { get; set; }
    }
}
