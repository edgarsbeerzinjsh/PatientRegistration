using Microsoft.EntityFrameworkCore;
using NETApi.Core.Models;

namespace NETApi.Data
{
    public class NETApiDBContext : DbContext, INETApiDbContext
    {
        public NETApiDBContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
