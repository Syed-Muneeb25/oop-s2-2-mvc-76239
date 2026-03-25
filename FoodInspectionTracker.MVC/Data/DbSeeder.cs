using Microsoft.AspNetCore.Identity;

namespace FoodInspectionTracker.MVC.Data
{

    public static class DbSeeder
{
    public static async Task SeedRolesAndUsersAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Admin", "Inspector", "Viewer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        await EnsureUser(userManager, "admin@gmail.com", "Admin@123", "Admin");
        await EnsureUser(userManager, "inspector@gmail.com", "Inspector@123", "Inspector");
        await EnsureUser(userManager, "viewer@gmail.com", "Viewer@123", "Viewer");
    }

    private static async Task EnsureUser(UserManager<IdentityUser> um, string email, string password, string role)
    {
        var user = await um.FindByEmailAsync(email);
        if (user == null)
        {
            user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            await um.CreateAsync(user, password);
        }
        if (!await um.IsInRoleAsync(user, role))
            await um.AddToRoleAsync(user, role);
    }
}

}
