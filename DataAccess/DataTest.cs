using Microsoft.AspNetCore.Identity;
using Domain;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataTest
    {
        public static async Task InsertData(UserManager<User> userManager)
        {
            if (!userManager.Users.Any()){
                var user = new User {
                    FullName="Francisco López", 
                    UserName="Francisco", 
                    Email="franlopez.freelance@gmail.com"
                };
                await userManager.CreateAsync(user, "Unicpass123$");
            }
        }
    }
}