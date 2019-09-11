using SIS.MvcFramework.Attributes.Validation;

namespace SULS.Web.ViewModels.Users
{
    public class UserLoginInputModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Username must be between5 and 20 symbols long.")]
        public string Username { get; set; }


        [RequiredSis]
        [StringLengthSis(6, 20, "Password must be between5 and 20 symbols long.")]
        public string Password { get; set; }
    }
}
