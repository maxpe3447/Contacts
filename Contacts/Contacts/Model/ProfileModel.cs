using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contacts.Model
{
    public class ProfileModel : IEntityBase
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public byte[] ProfileImage { get; set; }
        public string NickName { get; set; }
        public string Name{ get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}
