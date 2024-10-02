using RPG_API.Controllers;
using RPG_API.Data.Context;

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

        [TestCleanup]
        public void Cleanup()
        {
            databaseHelper.DropTestTables(context);
        }
    }
}