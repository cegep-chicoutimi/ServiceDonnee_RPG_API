using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RPG_API.Controllers;
using RPG_API.Data.Context;
using RPG_API.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test_API
{
    [TestClass]
    public class TestItem
    {
        private APIContext context;
        private ItemController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new ItemController(context);
        }
        [TestMethod]
        public async Task TestGetAll()
        {
            // Gets the result of the method and converts it to an ObjectResult
            ActionResult<PaginatedList<Item>> actionResult = await controller.GetAll();
            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            result.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            // Makes sure we got a success status code
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            PaginatedList<Item> i = result.Value as PaginatedList<Item>;

            i.Should().NotBeNull();
            i.Count().Should().Be(3);

        }

        [TestMethod]
        public async Task TestGetId()
        {
            // Gets the result of the GetFruit method and converts it to an ObjectResult
            ActionResult<Item> actionResult = await controller.Get(1);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            new Item { Name = "Épée", BoostAttack = 10, BoostDefence = 5, HealthRestoration = 0, Type = TypeItem.weapon };

            // Test the quest
            Item i = result.Value as Item;
            i.Should().NotBeNull();
            i.Name.Should().Be("Épée");
            i.BoostAttack.Should().Be(10);
            i.BoostDefence.Should().Be(5);
            i.HealthRestoration.Should().Be(0);
            i.Type.Should().Be(TypeItem.weapon);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            
            // Create a new quest to update the old one
            Item i = new Item {Id=1, Name = "Test"};

            IActionResult actionResult = await controller.Update(1, i);
            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task TestAdd()
        {
            // Create a new quest to update the old one
            Item i = new Item { Name = "Couteau", BoostAttack=2, BoostDefence=1, HealthRestoration=5, Type=TypeItem.weapon };
            
            ActionResult<Item> actionResult = await controller.Create(i);
            actionResult.Should().NotBeNull();
            ObjectResult? result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            ActionResult<Item> actionResult = await controller.Delete(2);
            actionResult.Should().NotBeNull();
            ObjectResult? result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(200);
        }

        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }
}
