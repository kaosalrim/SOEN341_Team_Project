using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.DTOs;
using API.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; //to create fakes for interfaces and classes as needed
using API.Classes;

namespace WEB_Q_A.Tests
{
    [TestClass]
    public class MemberSinceTest
    {

        [TestMethod]
        public Task AssignDate_DateTakesInValues_RetrurnDate()
        {
            // Arrange
            var memberSince = new MemberSince();

            // Act
            var result = memberSince.Days = 10;


            // Assert
            Assert.IsNotNull(result);
            return Task.CompletedTask;
        }
    }
}
