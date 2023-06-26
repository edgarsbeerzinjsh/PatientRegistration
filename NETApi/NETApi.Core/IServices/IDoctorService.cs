using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IDoctorService : IDbService<Doctor>
    {
        bool IsDoctorIdInDb(int doctorId);
    }
}
