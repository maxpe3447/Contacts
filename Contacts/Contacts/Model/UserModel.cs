using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Model
{
    public class UserModel : IEntityBase, IUserBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get ; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
