using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class DoctorService : DbService<Doctor>, IDoctorService
    {
        public DoctorService(INetApiDbContext context): base(context)
        {
        }

        public bool IsDoctorIdInDb(int doctorId)
        {
            return _dbContext.Doctors.Any(d => d.Id == doctorId);
        }
    }
}
