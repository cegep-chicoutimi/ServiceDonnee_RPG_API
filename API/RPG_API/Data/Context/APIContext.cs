using Microsoft.EntityFrameworkCore;
using RPG_API.Models;
using RPG_API.Models.Base;

namespace RPG_API.Data.Context
{
    public class APIContext: DbContext
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<Monster> Monster { get; set; }
        public DbSet<Map> Map { get; set; }
        public DbSet<Quest> Quest { get;set; }
        public DbSet<Tile> Tile { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Class> Class { get; set; }

        public APIContext(DbContextOptions<APIContext> options): base(options) { }
    }
}
