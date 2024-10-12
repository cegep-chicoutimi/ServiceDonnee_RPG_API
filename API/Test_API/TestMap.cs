using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TestMap
    {
        private APIContext context;
        private MapController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new MapController(context);
        }
        [TestMethod]
        public async Task TestGet()
        {
            ActionResult<Map> actionResult = await controller.Get(1);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Map m = result.Value as Map;
            m.Should().NotBeNull();
            m.CharacterId.Should().Be(1);
            m.ImageUrl.Should().Be("map1.png");

        }
        [TestMethod]
        public async Task TestGetNotFound()
        {
            ActionResult<Map> actionResult = await controller.Get(5);

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
            ActionResult<List<Map>> actionResult = await controller.GetAll();

            ObjectResult? result = actionResult.Result as ObjectResult;

            result.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            List<Map> maps = result.Value as List<Map>;

            maps.Should().NotBeNull();
            maps.Count().Should().Be(2);
        }
        [TestMethod]
        public async Task TestCreate()
        {
            Map m = new Map { CharacterId = 1, ImageUrl = "map3.png" };
            ActionResult<Map> actionResult = await controller.Create(m);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task TestCreateNot()
        {
            Map m = new Map { ImageUrl = "NotValide" };
            ActionResult<Map> actionResult = await controller.Create(m);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task TestUpdate()
        {
            Map m = new Map {Id=2, CharacterId = 2, ImageUrl = "map2.png" };
            IActionResult actionResult = await controller.Update(2,m);

            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<OkResult>();
            (actionResult as OkResult).StatusCode.Should().Be(200);
        }
        [TestMethod]
        public async Task TestUpdateNot()
        {
            
            Map m = new Map { CharacterId = 2, ImageUrl = "map2.png" };
            IActionResult actionResult = await controller.Update(1, m);

            actionResult.Should().NotBeNull();


            actionResult.Should().BeAssignableTo<BadRequestResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task TestDelete()
        {
            ActionResult<Map> actionResult = await controller.Delete(1);

            actionResult.Should().NotBeNull();

            actionResult.Result.Should().BeOfType<OkResult>();
            ((OkResult)actionResult.Result).StatusCode.Should().Be(200);
        }
        [TestMethod]
        public async Task TestDeleteNot()
        {
           ActionResult<Map> actionResult = await controller.Delete(5);

            actionResult.Should().NotBeNull();

            actionResult.Result.Should().BeOfType<NotFoundResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }
}
