using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SupermarketListUI.Controllers;
using System.Collections.Generic;

namespace SupermarketListUIUnitTests
{
    public class SupermarketListControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SupermarketListController_InIndexEndpoint_ShouldReturnData()
        {
            //Arrange
            SupermarketListUI.Data.SupermarketListDbContext dbContext = new SupermarketListUI.Data.SupermarketListDbContext();
            SupermarketListController controller = new SupermarketListController(dbContext);
            
            //Act
            var result = controller.Index();
            
            //Assert
            var actionResult = result as ViewResult;
            Assert.IsNotNull(actionResult);

            var supermarketList = (List<SupermarketListUI.Models.SupermarketList>)actionResult.Model;
            Assert.Greater(supermarketList.Count, 0);
        }
    }
}