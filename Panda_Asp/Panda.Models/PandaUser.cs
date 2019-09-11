using Microsoft.AspNetCore.Identity;

namespace Panda.Models
{
    public class PandaUser : IdentityUser
    {
        public UsersRore Role { get; set; }
    }
}
