using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using RPG_API.Controllers;
using RPG_API.Data.Context;
using RPG_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Test_API
{
    [TestClass]
    public class TestCharacter
    {
        private APIContext context;
        private CharacterController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new CharacterController(context);
        }
        [TestMethod]
        public async Task TestGetId()
        {
            ActionResult<Character> actionResult = await controller.Get(1);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Character c = result.Value as Character;
            c.Should().NotBeNull();
            c.Name.Should().Be("Arthur");
            c.Armor.Should().Be(15);
            c.Damage.Should().Be(20);
            c.Lives.Should().Be(3);
            c.Xp.Should().Be(100);
            c.ClassId.Should().Be(1);
        }
        [TestMethod]
        public async Task TestGetNotFound()
        {
            ActionResult<Character> actionResult = await controller.Get(4);

            // Gets the object from the action result
            NotFoundResult? result = actionResult.Result as NotFoundResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            // Makes sure we got the 404 code
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task TestGetAll()
        {
            ActionResult<List<Character>> actionResult = await controller.GetAll();

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            result.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            // Makes sure we got a success status code
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            List<Character> fruits = result.Value as List<Character>;

            fruits.Should().NotBeNull();
            fruits.Count().Should().Be(2);
        }
        [TestMethod]
        public async Task TestGetName()
        {
            ActionResult<Character> actionResult = await controller.GetByName("Merlin");

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Character c = result.Value as Character;
            c.Should().NotBeNull();
            c.Name.Should().Be("Merlin");
            c.Armor.Should().Be(5);
            c.Damage.Should().Be(25);
            c.Lives.Should().Be(2);
            c.Xp.Should().Be(120);
            c.ClassId.Should().Be(2);
        }
        [TestMethod]
        public async Task TestGetNameNotFound()
        {
            ActionResult<Character> actionResult = await controller.GetByName("Morgana");

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            // Makes sure we got the 404 code
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task TestGetClass()
        {
            ActionResult<Character> actionResult = await controller.GetByClass(2);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Character c = result.Value as Character;
            c.Should().NotBeNull();
            c.Name.Should().Be("Merlin");
            c.Armor.Should().Be(5);
            c.Damage.Should().Be(25);
            c.Lives.Should().Be(2);
            c.Xp.Should().Be(120);
            c.ClassId.Should().Be(2);
        }
        [TestMethod]
        public async Task TestGetClassNotFound()
        {
            ActionResult<Character> actionResult = await controller.GetByClass(6);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            // Makes sure we got the 404 code
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        //Create
        [TestMethod]
        public async Task TestCreate()
        {
            Character c = new Character { Name = "Guenièvre", Armor = 5, Damage = 30, Lives = 2, Xp = 150, ClassId = 1 };
            ActionResult<Character> actionResult = await controller.Create(c);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task TestCreateNot()
        {
            Character c = new Character { Name = "Guenièvre" };
            ActionResult<Character> actionResult = await controller.Create(c);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        //updatexp
        [TestMethod]
        public async Task TestUpdateXp()
        {
            
        }
        //updatedamage
        [TestMethod]
        public async Task TestUpdateDamage()
        {
            
        }
        //updatearmor
        [TestMethod]
        public async Task TestUpdateArmor()
        {
            
        }
        //updatelives
        [TestMethod]
        public async Task TestUpdateLives()
        {
            
        }
        //updatexp
        [TestMethod]
        public async Task TestUpdateXpNot()
        {

        }
        [TestMethod]
        public async Task TestUpdateDamageNot()
        {

        }
        [TestMethod]
        public async Task TestUpdateArmorNot()
        {

        }
        [TestMethod]
        public async Task TestUpdateLivesNot()
        {

        }
        //AddItem
        [TestMethod]
        public async Task TestAddItem()
        {
            IActionResult actionResult = await controller.AddItemToInventory(1, 1);

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
           
            // Gets the object from the action result
           actionResult.Should().Be((int)HttpStatusCode.OK);

        }
        //NOt
        //TODO: BadRequest
        //AddQuest
        [TestMethod]
        public async Task TestAddQuest()
        {
            //IActionResult actionResult = await controller.AddItemToInventory(1, 1);

            //// Makes sure the result is not null
            //actionResult.Should().NotBeNull();

            //// Gets the object from the action result
            //actionResult.Should().Be((int)HttpStatusCode.OK);

        }
        //NOt
        //Delete
        //NOt
        //deleteItem
        //NOt
        //deleteQuest
        //NOt


        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }

}
