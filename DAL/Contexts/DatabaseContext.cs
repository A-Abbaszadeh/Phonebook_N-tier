using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class DatabaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-ABNJIE3\MSSQLSERVER2019; Initial Catalog=ContactDb; Integrated Security=true;");
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
