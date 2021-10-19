using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> User{ get; set; }

        public DbSet<QuestionModel> Question{ get; set; }

        public DbSet<AnswerModel> Answer{ get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //=> options.UseSqlite(@"Data Source=D:\Desktop\uni\fall 2021\soen 341\StackOverFlow\StackOverFlowDB");
    }
}