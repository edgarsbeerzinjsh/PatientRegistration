using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class DoctorService : DbService<Doctor>, IDoctorService
    {
        public DoctorService(INETApiDbContext context): base(context)
        {
        }

        public List<Patient> GetAllPatients(int id)
        {
            return _dbContext.DoctorsPatients
                .Where(dp => dp.DoctorId == id)
                .Select(dp => _dbContext.Patients.FirstOrDefault(p => p.Id == dp.PatientId))
                .ToList();
        }
    }
}
