using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Entitiy
{
    public class UserApp : IdentityUser
    {
        //other properties will come default
        public string City { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
