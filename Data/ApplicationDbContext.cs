using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStudents.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStudents.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
