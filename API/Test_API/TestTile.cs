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

namespace Test_API
{
    [TestClass]
    public class TestTile
    {
        private APIContext context;
        private TileController controller;
        private DatabaseHelper databaseHelper;

        [TestInitialize]
        public void Initialize()
        {
            databaseHelper = new DatabaseHelper();
            context = databaseHelper.CreateContext();
            databaseHelper.CreateRPGTables(context);
            controller = new TileController(context);
        }

        [TestMethod]
        public async Task TestGet()
        {
            // Gets the result of the method and converts it to an ObjectResult
            ActionResult<PaginatedList<Tile>> actionResult = await controller.GetAll();
            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            result.Should().NotBeNull();
            actionResult.Should().NotBeNull();

            // Makes sure we got a success status code
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            PaginatedList<Tile> tiles = result.Value as PaginatedList<Tile>;

            tiles.Should().NotBeNull();
            tiles.Count().Should().Be(4);

            // Test an individual tile
            Tile? tile = tiles.FirstOrDefault();
            tile.Should().NotBeNull();
            tile.Id.Should().Be(1);
            tile.X.Should().Be(0);
            tile.Y.Should().Be(0);
            tile.Type.Should().Be(TypeTile.Sand);
        }

        [TestMethod]
        public async Task TestGetId()
        {
            // Gets the result of the GetFruit method and converts it to an ObjectResult
            ActionResult<Tile> actionResult = await controller.Get(1);

            // Gets the object from the action result
            ObjectResult? result = actionResult.Result as ObjectResult;

            // Makes sure the result is not null
            actionResult.Should().NotBeNull();
            result.Should().NotBeNull();

            // Test the tile
            Tile tile = result.Value as Tile;
            tile.Should().NotBeNull();
            tile.Id.Should().Be(1);
            tile.MapId.Should().Be(1);
            tile.Type.Should().Be(TypeTile.Sand);
            tile.X.Should().Be(0);
            tile.Y.Should().Be(0);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            // Create a new tile to update the old one
            Tile tile = new Tile();
            tile.Id = 1;
            tile.MapId = 1;
            tile.X = 5;
            tile.Y = 5;
            tile.Type = TypeTile.Water;

            ActionResult<Tile> actionResult = await controller.Update(1, tile);
            actionResult.Should().NotBeNull();
            ObjectResult? result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task TestAdd()
        {
            // Create a new tile to update the old one
            Tile tile = new Tile();
            tile.Id = 5;
            tile.MapId = 1;
            tile.X = 10;
            tile.Y = 10;
            tile.Type = TypeTile.Grass;

            ActionResult<Tile> actionResult = await controller.Create(tile); 
            actionResult.Should().NotBeNull();
            ObjectResult? result = actionResult.Result as ObjectResult;
            result.Should().NotBeNull();

            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task TestDelete()
        {
            ActionResult<Tile> actionResult = await controller.Delete(4);
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
