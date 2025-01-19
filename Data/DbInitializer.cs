using Common.Constants;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

	public static class DbInitializer
	{
		public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			await AddRolesAsync(roleManager);
			await AddAdminAsync(userManager, roleManager);
		}
		private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			foreach (var role in Enum.GetValues<UserRoles>())
			{
				if (!await roleManager.RoleExistsAsync(role.ToString()))
				{
					_ = roleManager.CreateAsync(new IdentityRole
					{
						Name = role.ToString()
					}).Result;
				}
			}
		}
		private static async Task AddAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{

			if (await userManager.FindByEmailAsync("admin@app.com") is null)
			{

				var user = new User
				{
					UserName = "admin@app.com",
					Email = "admin@app.com",
				};

				var result = await userManager.CreateAsync(user, "Salam123!");
				if (!result.Succeeded)
					throw new Exception("Admin elave edile bilmedi");

				var role = await roleManager.FindByNameAsync("Admin");
				if (role?.Name is null)
					throw new Exception("Admin rolu tapilmadi");

				var addToResult = await userManager.AddToRoleAsync(user, role.Name);
				if (!addToResult.Succeeded)
					throw new Exception("Admine rol elave etmek mumkun olmadi");
			}
		}
	}
}
