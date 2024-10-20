using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RPG_API.Controllers;
using RPG_API.Data.Context;
using RPG_API.Models;
using System.ComponentModel;

namespace Test_API
{
    [TestClass]
    public class TestMonster
    {
        private APIContext context;
        private MonsterController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new MonsterController(context);
        }

        [TestMethod]
        public async Task TestGetMonsterById()
        {
            
            ActionResult<Monster> actionResult = await controller.Get(1);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            Monster monster = result.Value as Monster;
            monster.Should().NotBeNull();
            monster.Name.Should().Be("Dragon");
            monster.Armor.Should().Be(50);
            monster.Damage.Should().Be(100);
            monster.Health.Should().Be(200);
            monster.XpGiven.Should().Be(300);
            monster.Difficulty.Should().Be(DifficultyMonster.Hard);
            monster.Category.Should().Be(Category.Dragon);
        }

        [TestMethod]
        public async Task TestGetMonsterById_NotFound()
        {
            ActionResult<Monster> actionResult = await controller.Get(999);

            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestGetByDifficulty()
        {
            ActionResult<PaginatedList<Monster>> actionResult = await controller.GetByDifficulty(1);

            ObjectResult? result = actionResult.Result as ObjectResult;

            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            PaginatedList<Monster> monsters = result.Value as PaginatedList<Monster>;
            monsters.Should().NotBeNull();
            monsters.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public async Task TestGetByCategory()
        {
            ActionResult<PaginatedList<Monster>> actionResult = await controller.GetByCategory(1);

            ObjectResult? result = actionResult.Result as ObjectResult;

          

            PaginatedList<Monster> monsters = result.Value as PaginatedList<Monster>;
            monsters.Should().NotBeNull();
            monsters.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public async Task TestSearchByName_StartsWith()
        {
            // Act
            var actionResult = await controller.SearchByName("", NameStartsBy: "Dra", pageNumber: 1, pageSize: 10);

            // Assert
            var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeAssignableTo<object>().Subject;
            var items = response.GetType().GetProperty("Items").GetValue(response) as List<Monster>;

            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyContain(m => m.Name.StartsWith("Dra", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public async Task TestSearchByName_Contains()
        {
            // Act
            var actionResult = await controller.SearchByName(NameContains: "rag", "", pageNumber: 1, pageSize: 10);

            // Assert
            var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeAssignableTo<object>().Subject;
            var items = response.GetType().GetProperty("Items").GetValue(response) as List<Monster>;

            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyContain(m => m.Name.Contains("rag", StringComparison.OrdinalIgnoreCase));
        }


        [TestMethod]
        public async Task TestSearchByName_ContainsStartsBy()
        {
            // Act
            var actionResult = await controller.SearchByName(NameContains: "rag", NameStartsBy: "Dra", pageNumber: 1, pageSize: 10);

            // Assert
            var okResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeAssignableTo<object>().Subject;
            var items = response.GetType().GetProperty("Items").GetValue(response) as List<Monster>;

            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyContain(m => m.Name.Contains("rag", StringComparison.OrdinalIgnoreCase) && m.Name.StartsWith("Dra", StringComparison.OrdinalIgnoreCase));
        }


        [TestMethod]
        public async Task TestSearchByStats()
        {
       
            var actionResult = await controller.SearchByStats(
                minDamage: 5,
                maxDamage: 20,
                minArmor: 5,
                maxArmor: 20,
                minHealth: 5,
                maxHealth: 50,
                minXpReward: 5,
                maxXpReward: 100,
                pageNumber: 1,
                pageSize: 10);

            
            actionResult.Should().NotBeNull();
            var result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();
            var response = result.Value as object;
            response.Should().NotBeNull();


            var items = response.GetType().GetProperty("Items").GetValue(response) as IEnumerable<Monster>;
            items.Should().NotBeNull();
            items.Should().HaveCountGreaterThan(0);

            foreach (var monster in items)
            {
                monster.Damage.Should().BeInRange(5, 20);
                monster.Armor.Should().BeInRange(5, 20);
                monster.Health.Should().BeInRange(5, 50);
                monster.XpGiven.Should().BeInRange(5, 100);
            }
        }


        [TestMethod]
        public async Task TestSearchByStats_BadRequest()
        {
        
            var actionResult = await controller.SearchByStats(
                minDamage: null,
                maxDamage: null,
                minArmor: null,
                maxArmor: null,
                minHealth: null,
                maxHealth: null,
                minXpReward: null,
                maxXpReward: null,
                pageNumber: 1,
                pageSize: 10);

            actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        }


        [TestMethod]
        public async Task TestUpdateMonster()
        {
      
            int monsterId = 1;
            var updatedMonster = new Monster
            {
                Id = monsterId,
                Name = "Updated Dragon",
                Armor = 60,
                Damage = 110,
                Health = 220,
                XpGiven = 320,
                Difficulty = DifficultyMonster.Boss,
                Category = Category.Dragon
            };

            var result = await controller.Update(monsterId, updatedMonster);
            result.Should().BeOfType<OkResult>();

            // Verifier si updaté
            var getResult = await controller.Get(monsterId);
            var okObjectResult = getResult.Result as OkObjectResult;
            var retrievedMonster = okObjectResult.Value as Monster;

            retrievedMonster.Should().NotBeNull();
            retrievedMonster.Name.Should().Be("Updated Dragon");
            retrievedMonster.Armor.Should().Be(60);
            retrievedMonster.Damage.Should().Be(110);
            retrievedMonster.Health.Should().Be(220);
            retrievedMonster.XpGiven.Should().Be(320);
            retrievedMonster.Difficulty.Should().Be(DifficultyMonster.Boss);
            retrievedMonster.Category.Should().Be(Category.Dragon);
        }


        [TestMethod]
        public async Task TestUpdateMonster_NotFound()
        {
       
            int monsterId = 999;
            var updatedMonster = new Monster
            {
                Id = monsterId,
                Name = "Updated Dragon",
                Armor = 60,
                Damage = 110,
                Health = 220,
                XpGiven = 320,
                Difficulty = DifficultyMonster.Boss,
                Category = Category.Dragon
            };

            var result = await controller.Update(monsterId, updatedMonster);
            result.Should().BeOfType<NotFoundResult>();
        }



        [TestMethod]
        public async Task TestDeleteMonster()
        {
            ActionResult<Monster> actionResult = await controller.Delete(1);

            actionResult.Should().NotBeNull();

            
            ActionResult<Monster> getResult = await controller.Get(1);
            getResult.Result.Should().BeOfType<NotFoundResult>();
        }


        [TestMethod]
        public async Task TestDeleteMonster_NotFound()
        {
     
            ActionResult<Monster> actionResult = await controller.Delete(999);

            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }



        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }
}