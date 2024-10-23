using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RPG_API.Controllers;
using RPG_API.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test_API
{
    [TestClass]
    public class TestQuest
    {
        private APIContext context;
        private QuestController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new QuestController(context);
        }

        [TestMethod]
        public async Task TestGet()
        {
            // Gets the result of the method and converts it to an ObjectResult
            ActionResult<PaginatedList<Quest>> actionResult = await controller.GetAll();
            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            result.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            // Makes sure we got a success status code
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            PaginatedList<Quest> quests = result.Value as PaginatedList<Quest>;

            quests.Should().NotBeNull();
            quests.Count().Should().Be(2);

            // Test an individual quest
            Quest? quest = quests.FirstOrDefault();
            quest.Should().NotBeNull();
            quest.Id.Should().Be(1);
            quest.Reward.Should().Be(500);
            quest.Description.Should().Be("Vaincre le dragon pour sauver le village");
        }

        [TestMethod]
        public async Task TestGetId()
        {
            // Gets the result of the GetFruit method and converts it to an ObjectResult
            ActionResult<Quest> actionResult = await controller.Get(1);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            // Test the quest
            Quest quest = result.Value as Quest;
            quest.Should().NotBeNull();
            quest.Id.Should().Be(1);
            quest.Reward.Should().Be(500);
            quest.Description.Should().Be("Vaincre le dragon pour sauver le village");
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            // Create a new quest to update the old one
            Quest quest = new Quest();
            quest.Id = 1;
            quest.Reward = 300;
            quest.Description = "Update test";
            quest.Title = "Test";


            ActionResult<Quest> actionResult = await controller.Update(1, quest);
            actionResult.Should().NotBeNull();
            ObjectResult? result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task TestAdd()
        {
            // Create a new quest to update the old one
            Quest quest = new Quest();
            quest.Id = 4;
            quest.Reward = 300;
            quest.Description = "Update test";
            quest.Title = "Test";

            ActionResult<Quest> actionResult = await controller.Create(quest);
            actionResult.Should().NotBeNull();
            ObjectResult? result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            ActionResult<Quest> actionResult = await controller.Delete(2);
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
