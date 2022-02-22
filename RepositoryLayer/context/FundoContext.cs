using Microsoft.EntityFrameworkCore;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.context
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> UserTables { get; set; }
    }
}