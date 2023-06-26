using Microsoft.EntityFrameworkCore;
using NETApi.Core.Models;

namespace NETApi.Data
{
    public class NetApiDbContext : DbContext, INetApiDbContext
    {
        public NetApiDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorPatient> DoctorsPatients { get; set;}
    }
}
