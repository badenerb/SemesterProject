using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using SemesterProject.Entities;
using SemesterProject.Data;

namespace SemesterProject.Data
{
    public class SemesterProjectDbContext : DbContext
    {
        public SemesterProjectDbContext(DbContextOptions<SemesterProjectDbContext> options) : base(options)
        {
        }

        public DbSet<ProductMaster> ProductMaster{ get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
    }
}
