﻿using System.Data.Entity;

namespace LibraryApp.Models
{
    public class LibraryAppContext : DbContext
    {
        public LibraryAppContext() : base("name=LibraryAppContext")
        {
        }

        public DbSet<Writer> Writers { get; set; }
    }
}