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
using Microsoft.EntityFrameworkCore.Update;
using RPG_API.Models.Base;

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

            ObjectResult? result = actionResult.Result as ObjectResult;

            result.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            List<Character> characters = result.Value as List<Character>;

            characters.Should().NotBeNull();
            characters.Count().Should().Be(2);
        }
        [TestMethod]
        public async Task TestGetName()
        {
            ActionResult<Character> actionResult = await controller.GetByName("Merlin");

            ObjectResult? result = actionResult.Result as ObjectResult;

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

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task TestGetClass()
        {
            ActionResult<Character> actionResult = await controller.GetByClass(2);

            ObjectResult? result = actionResult.Result as ObjectResult;


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

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        //Create
        [TestMethod]
        public async Task TestCreate()
        {
            Character c = new Character { Name = "Guenièvre", Armor = 5, Damage = 30, Lives = 2, Xp = 150, ClassId = 1 };
            ActionResult<Character> actionResult = await controller.Create(c);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task TestCreateNot()
        {
            Character c = new Character { Name = "NotValide" };
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
            IActionResult actionResult = await controller.UpdateXP(1, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
        //updatedamage
        [TestMethod]
        public async Task TestUpdateDamage()
        {
            IActionResult actionResult = await controller.UpdateDamage(1, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
        //updatearmor
        [TestMethod]
        public async Task TestUpdateArmor()
        {
            IActionResult actionResult = await controller.UpdateArmor(1, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
        //updatelives
        [TestMethod]
        public async Task TestUpdateLives()
        {
            IActionResult actionResult = await controller.UpdateLives(1, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
        //updatexp
        [TestMethod]
        public async Task TestUpdateXpNot()
        {
            IActionResult actionResult = await controller.UpdateXP(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }
        [TestMethod]
        public async Task TestUpdateDamageNot()
        {
            IActionResult actionResult = await controller.UpdateDamage(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }
        [TestMethod]
        public async Task TestUpdateArmorNot()
        {
            IActionResult actionResult = await controller.UpdateArmor(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }
        [TestMethod]
        public async Task TestUpdateLivesNot()
        {
            IActionResult actionResult = await controller.UpdateLives(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }
        //AddItem
        [TestMethod]
        public async Task TestAddItem()
        {
            IActionResult actionResult = await controller.AddItemToInventory(1, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
        //AddItemNot
        [TestMethod]
        public async Task TestAddItemNot()
        {
            IActionResult actionResult = await controller.AddItemToInventory(1000, 1);

            actionResult.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>()
              .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }
        //AddQuest
        [TestMethod]
        public async Task TestAddQuest()
        {
            IActionResult actionResult = await controller.AddQuest(1, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
           .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        //AddQuestNot
        [TestMethod]
        public async Task TestAddQuestNot()
        {
            IActionResult actionResult = await controller.AddQuest(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            IActionResult actionResult = await controller.Delete(1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task TestDeleteItem()
        {

            Item i = new Item { Name = "Potion", BoostAttack = 0, BoostDefence = 2, HealthRestoration = 10, Type = TypeItem.consumable };

            context.AddRange(i);
            context.SaveChanges();
            Character ch = new Character { Name = "Lena", Inventory = new List<Item> { i }, Equipment = new List<JonctionItemCharacter>(), Armor = 15, Damage = 20, Lives = 3, Xp = 100, ClassId = 1, Quests = new List<Quest>() };
            
            context.AddRange(ch);
            context.SaveChanges();
            IActionResult actionResult = await controller.DeleteItemFromInventory(ch.Id, i.Id);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
             .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task TestDeleteQuest()
        {


            Quest q = new Quest { Title = "Tuer le dragon", Description = "Vaincre le dragon pour sauver le village", Reward = 500, ItemId = 1 };
            context.AddRange(q);
            context.SaveChanges();
            Character ch = new Character { Name = "Lena", Inventory = new List<Item> (), Equipment = new List<JonctionItemCharacter>(), Armor = 15, Damage = 20, Lives = 3, Xp = 100, ClassId = 1, Quests = new List<Quest> { q} };

            context.AddRange(ch);
            context.SaveChanges();
            IActionResult actionResult = await controller.DeleteQuest(ch.Id, q.Id);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task TestDeleteNot()
        {
            IActionResult actionResult = await controller.Delete(1000);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task TestDeleteItemNot()
        {

            IActionResult actionResult = await controller.DeleteItemFromInventory(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public async Task TestDeleteQuestNot()
        {
            IActionResult actionResult = await controller.DeleteQuest(1000, 1);

            actionResult.Should().NotBeNull();

            actionResult.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }

}
