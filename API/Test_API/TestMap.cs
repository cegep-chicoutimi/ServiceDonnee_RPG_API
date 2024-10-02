﻿using RPG_API.Controllers;
using RPG_API.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }
}