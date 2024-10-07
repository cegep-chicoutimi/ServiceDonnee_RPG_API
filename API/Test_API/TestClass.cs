using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using RPG_API.Controllers;
using RPG_API.Data.Context;
using RPG_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Test_API
{
    [TestClass]
    public class TestClass
    {
        private APIContext context;
        private ClassController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new ClassController(context);
        }

        [TestMethod]
        public async Task TestGetId()
        {
            ActionResult<Class> actionResult = await controller.Get(1);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Class cl = result.Value as Class;
            cl.Should().NotBeNull();
            cl.Name.Should().Be("barbare");
            cl.BoostAttack.Should().Be(2);
            cl.BoostDefence.Should().Be(2);
        }

        [TestMethod]
        public async Task TestGetId_NotFound()
        {
            ActionResult<Class> actionResult = await controller.Get(999);

            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestGetByName()
        {
            ActionResult<Class> actionResult = await controller.GetByName("barbare");

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Class cl = result.Value as Class;
            cl.Should().NotBeNull();
            cl.Id.Should().Be(1);
            cl.Name.Should().Be("barbare");
        }

        [TestMethod]
        public async Task TestGetByName_NotFound()
        {
            ActionResult<Class> actionResult = await controller.GetByName("nonexistent");

            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestGetAll()
        {
            ActionResult<List<Class>> actionResult = await controller.GetAll();

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            List<Class> classes = result.Value as List<Class>;
            classes.Should().NotBeNull();
            classes.Should().HaveCountGreaterThan(0);
        }

     

        [TestMethod]
        public async Task TestSearchByName()
        {
            ActionResult<PaginatedList<Class>> actionResult = await controller.SearchItemsByName("b", "bar", 1, 10);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            PaginatedList<Class> paginatedClasses = result.Value as PaginatedList<Class>;
            paginatedClasses.Should().NotBeNull();
            paginatedClasses.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public async Task TestSearchByName_NotFound()
        {
            ActionResult<PaginatedList<Class>> actionResult = await controller.SearchItemsByName("z", "zzz", 1, 10);

            actionResult.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            Class updatedClass = new Class { Id = 1, Name = "Updated Barbare" };
            IActionResult actionResult = await controller.Update(1, updatedClass);

            actionResult.Should().BeOfType<OkResult>();

            // Verifier si udpate
            ActionResult<Class> getResult = await controller.Get(1);
            ObjectResult? getObjectResult = getResult.Result as ObjectResult;
            Class updatedClassFromDb = getObjectResult.Value as Class;
            updatedClassFromDb.Name.Should().Be("Updated Barbare");
        }

        [TestMethod]
        public async Task TestUpdate_NotFound()
        {
            Class updatedClass = new Class { Id = 999, Name = "Nonexistent Class" };
            IActionResult actionResult = await controller.Update(999, updatedClass);

            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestUpdate_BadRequest()
        {
            Class updatedClass = new Class { Id = 2, Name = "Mismatched ID" };
            IActionResult actionResult = await controller.Update(1, updatedClass);

            actionResult.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task TestCreate()
        {
            Class newClass = new Class { Name = "New Class", BoostAttack = 3, BoostDefence = 1 };
            ActionResult<Class> actionResult = await controller.Create(newClass);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Class createdClass = result.Value as Class;
            createdClass.Should().NotBeNull();
            createdClass.Id.Should().BeGreaterThan(0);
            createdClass.Name.Should().Be("New Class");
        }

        [TestMethod]
        public async Task TestCreate_BadRequest()
        {
            
            Class invalidClass = new Class { Name = "", BoostAttack = 3, BoostDefence = 1 };
            ActionResult<Class> actionResult = await controller.Create(invalidClass);

            actionResult.Result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task TestDelete()
        {
            IActionResult actionResult = await controller.Delete(1);

            actionResult.Should().BeOfType<OkObjectResult>();

            // verifier si delete
            ActionResult<Class> getResult = await controller.Get(1);
            getResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestDelete_NotFound()
        {
            IActionResult actionResult = await controller.Delete(999);

            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }
}