using API.Controllers;
using API.DTOs;
using API.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; //to create fakes for interfaces and classes as needed
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WEB_Q_A.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public async Task SetupFakeUserRepo_InitializeUsersController_GetUsers()
        {

            List<UserDto> list = new List<UserDto>();

            //adds 3 users to the list
            for (int i = 0; i < 3; i++)
            {
                UserDto user = new()
                {
                    Username = "user" + i
                };
                list.Add(user);
            }

            var fakeUserRepository = new Mock<IUserRepository>();
            fakeUserRepository.Setup(x => x.GetMembersAsync()).ReturnsAsync(list);

            var userList = await fakeUserRepository.Object.GetMembersAsync();
            Assert.IsNotNull(userList); //here the result is not nullified
            Assert.AreEqual(list, userList);

            var usersController = new UsersController(fakeUserRepository.Object);
            var task = await usersController.GetUsers();
            Assert.IsNotNull(task.Value); //from the user controller the list is null...
            //somehow the result is lost with the Ok(users) methods

        }

        [TestMethod]
        public async Task SetupFakeUserRepo_InitializeUsersConroller_GetUser()
        {
            UserDto user = new()
            {
                Username = "user1",
                FirstName = "Yo!"
            };

            //create fake user repo and setup to return user when GetMemberAsync(username) is called
            var fakeUserRepository = new Mock<IUserRepository>();
            fakeUserRepository.Setup(x => x.GetMemberAsync("user1")).ReturnsAsync(user);

            //initialize users controller with fake user repo, then call method GetUser(username)
            var usersController = new UsersController(fakeUserRepository.Object);
            var task = await usersController.GetUser("user1");
            var resultUser = task.Value;

            //check if the resulting user object is null
            Assert.IsNotNull(resultUser);
            Console.WriteLine("Username: " + resultUser.Username);
            Console.WriteLine("Username: " + resultUser.FirstName);



        }
    }
}
