using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaViveros.API.Models;
using SistemaViveros.Common.Models;
using SistemaViveros.Domain.Models;
using System;
namespace SistemaViveros.API.Helpers
{
    public class UsersHelper : IDisposable
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static DataContext db = new DataContext();

        public static Response CreateUserASP(UserRequest userRequest)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var oldUserASP = userManager.FindByEmail(userRequest.Email);

                if (oldUserASP != null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "001. Usuario ya existe",
                    };

                }

                var userASP = new ApplicationUser
                {
                    Email = userRequest.Email,
                    UserName = userRequest.Email
                };

                var result = userManager.Create(userASP, userRequest.Password);
                
                return new Response
                {
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.ToString(),

                };
            }
        }

        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }
}