using Entities.Concrete.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI.CustomValidation
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PasswordContainsUsername", Description = "Şifre alanı kullanıcı adını içeremez" });
            }

            string[] a = new[]
            {
                "123456",
                "234567",
                "345678",
                "456789"
            };

            foreach (var dizi in a)
            {
                if (password.ToLower().Contains(dizi))
                {
                    errors.Add(new IdentityError() { Code = "PasswordContains1234", Description = "Şifre ardışık sayı içeremez" });
                    break;
                }
            }

            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
