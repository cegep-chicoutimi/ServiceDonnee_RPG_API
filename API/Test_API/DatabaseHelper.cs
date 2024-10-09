using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Adapter;
using RPG_API.Data.Context;
using RPG_API.Models.Base;
using RPG_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_API
{
    public class DatabaseHelper
    {
        public APIContext CreateContext()
        {
            DbContextOptionsBuilder<APIContext> builder = new DbContextOptionsBuilder<APIContext>();
            string connectionString =
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetConnectionString("Test") ?? string.Empty;
            //S'assurer d'utiliser une base de données de test

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).EnableSensitiveDataLogging();

            return new APIContext(builder.Options);
        }

        public void CreateRPGTables(APIContext context)
        {
            DropTestTables(context);
            context.Database.EnsureCreated();

            CreateClasses(context);
            CreateItems(context);
            CreateCharacters(context);
            CreateMaps(context);
            CreateMonsters(context);
            CreateQuests(context);
            CreateTiles(context);
        }

        private void CreateClasses(APIContext context)
        {
            Class[] classes = new Class[]
            {
            new Class { Name = "Guerrier", BoostAttack = 10.5, BoostDefence = 7.2 },
            new Class { Name = "Mage", BoostAttack = 15.0, BoostDefence = 3.5 }
            };
            context.AddRange(classes);
            context.SaveChanges();
        }

        private void CreateItems(APIContext context)
        {
            Item[] items = new Item[]
            {
            new Item { Name = "Épée", BoostAttack = 10, BoostDefence = 5, HealthRestoration = 0, Type = TypeItem.weapon },
            new Item { Name = "Bouclier", BoostAttack = 2, BoostDefence = 12, HealthRestoration = 0, Type = TypeItem.armor },
            new Item { Name = "Potion", BoostAttack = 0, BoostDefence = 2, HealthRestoration = 10, Type = TypeItem.consumable }
            };
            context.AddRange(items);
            context.SaveChanges();
        }

        private void CreateCharacters(APIContext context)
        {
            Character[] characters = new Character[]
            {
            new Character { Name = "Arthur", Inventory = new List<Item>(), Equipment = new List<JonctionItemCharacter>(), Armor = 15, Damage = 20, Lives = 3, Xp = 100, ClassId = 1 , Quests = new  List<Quest>()},
            new Character { Name = "Merlin", Inventory = new List<Item>(), Equipment = new List<JonctionItemCharacter>(), Armor = 5, Damage = 25, Lives = 2, Xp = 120, ClassId = 2, Quests = new  List<Quest>() }
            };
            context.AddRange(characters);
            context.SaveChanges();
        }

        private void CreateMaps(APIContext context)
        {
            Map[] maps = new Map[]
            {
            new Map { CharacterId = 1, ImageUrl = "map1.png" },
            new Map { CharacterId = 2, ImageUrl = "map2.png" }
            };
            context.AddRange(maps);
            context.SaveChanges();
        }

        private void CreateMonsters(APIContext context)
        {
            Monster[] monsters = new Monster[]
            {
            new Monster { Name = "Dragon", Armor = 50, Damage = 100, Health = 200, XpGiven = 300, Difficulty = DifficultyMonster.Hard, MapId = 1, Category = Category.Dragon },
            new Monster { Name = "Elf", Armor = 50, Damage = 100, Health = 200, XpGiven = 300, Difficulty = DifficultyMonster.Medium, MapId = 1, Category = Category.Chimera },
            new Monster { Name = "Gobelin", Armor = 10, Damage = 15, Health = 30, XpGiven = 50, Difficulty = DifficultyMonster.Easy, MapId = 2, Category = Category.Demon }
            };
            context.AddRange(monsters);
            context.SaveChanges();
        }

        private void CreateQuests(APIContext context)
        {
            Quest[] quests = new Quest[]
            {
            new Quest { Title = "Tuer le dragon", Description = "Vaincre le dragon pour sauver le village", Reward = 500, ItemId = 1 },
            new Quest { Title = "Trouver l'épée légendaire", Description = "Récupérer l'épée légendaire cachée dans les montagnes", Reward = 300, ItemId = 2 }
            };
            context.AddRange(quests);
            context.SaveChanges();
        }

        private void CreateTiles(APIContext context)
        {
            Tile[] tiles = new Tile[]
            {
            new Tile { X = 0, Y = 0, Type = TypeTile.Sand, MapId = 1 },
            new Tile { X = 1, Y = 0, Type = TypeTile.Grass, MapId = 1 },
            new Tile { X = 0, Y = 1, Type = TypeTile.Mountain, MapId = 2 },
            new Tile { X = 1, Y = 1, Type = TypeTile.Water, MapId = 2 }
            };
            context.AddRange(tiles);
            context.SaveChanges();
        }

        public void DropTestTables(APIContext context)
        {
            context.Database.EnsureDeleted();
        }
    }
}
