using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kit.Web.Controllers.Model
{
    #region LoginRequest
    public class LoginRequest : IValidatableObject
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirmation { get; set; }
        [Required]
        public bool RememberMe { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.Equals(Password, PasswordConfirmation) == false)
            {
                yield return new ValidationResult("Password confirmation is different than Password",
                    new[] { nameof(PasswordConfirmation) });
            }
        }
    }
    #endregion

}
