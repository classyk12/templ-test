using Microsoft.EntityFrameworkCore;
using tmplltest.Core.DataModels;
using tmpltest.Utilities;

namespace tmplltest.Core.Context
{
    public class JokeDbContext : DbContext
    {
        public DbSet<JokeDataObject> Jokes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseCred.GetConnectionString());
        }
    }
}


 