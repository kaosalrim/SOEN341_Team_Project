using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>{
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser{
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
        }

        public static async Task SeedQuestions(DataContext dataContext){
            if (await dataContext.Questions.AnyAsync()) return;

            var questionData = await System.IO.File.ReadAllTextAsync("Data/QuestionSeedData.json");
            var questions = JsonSerializer.Deserialize<List<Question>>(questionData);
            if (questions == null) return;

            await dataContext.Questions.AddRangeAsync(questions);

            await dataContext.SaveChangesAsync();
        }

        public static async Task SeedAnswers(DataContext dataContext){
            if (await dataContext.Answers.AnyAsync()) return;

            var answerData = await System.IO.File.ReadAllTextAsync("Data/AnswerSeedData.json");
            var answers = JsonSerializer.Deserialize<List<Answer>>(answerData);
            if (answers == null) return;

            await dataContext.Answers.AddRangeAsync(answers);

            await dataContext.SaveChangesAsync();
        }
    }
}