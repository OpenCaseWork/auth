using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCaseWork.AuthServer
{
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {           
              return Task.FromResult(0);
        }

    }
}
