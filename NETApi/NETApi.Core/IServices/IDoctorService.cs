using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IDoctorService : IDbService<Doctor>
    {
        List<Patient> GetAllPatients(int id);
    }
}
