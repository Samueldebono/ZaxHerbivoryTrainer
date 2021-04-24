using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Models;
using ZaxHerbivoryTrainer.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;

namespace API.ZaxHerbivoryTrainer.Entities
{
    public class ZaxHerbivoryTrainerContext : DbContext
    {
        public ZaxHerbivoryTrainerContext()
        {
        }

        public ZaxHerbivoryTrainerContext(DbContextOptions<ZaxHerbivoryTrainerContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<UserEmails> UserEmails { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<UsersGuess> UsersGuess { get; set; }
        public DbSet<AuthUsers> AuthUsers { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
       
    }
}
