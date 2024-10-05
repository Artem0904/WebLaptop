using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class TelegramBotDbContext : DbContext
    {
        public DbSet<BotUser> BotUsers { get; set; }

        public TelegramBotDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var str = @"Host=ep-little-voice-a5zcydzn.us-east-2.aws.neon.tech;Database=TelegramBotDb;User Id=artemdb_owner;Password=TiZtIB09fjdm;";
            optionsBuilder.UseNpgsql(str);
        }
    }
}
