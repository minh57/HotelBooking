using HotelBooking.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

public static class IdentitySeed
{
    public static void Seed()
    {
        using (var context = new ApplicationDbContext())
        {
            // ROLE
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            // USER
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var adminEmail = "admin@site.com";
            var adminPassword = "Admin@123"; // đổi sau khi login

            var adminUser = userManager.FindByEmail(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var result = userManager.Create(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
        }
    }
}