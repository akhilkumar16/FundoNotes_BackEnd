using Microsoft.EntityFrameworkCore;
using RepositoryLayer.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.context
{
    public class FundoContext : DbContext // context class is used to store the Value and Send.
    {
        public FundoContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> UserTables { get; set; } // holds result from the database.

        public DbSet<Notes> Notestables { get; set; }
        public DbSet<Collaborator> Colltables { get; set; }
    }
}