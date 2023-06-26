using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IPatientService : IDbService<Patient>
    {
        List<Patient> GetAllPatientsFullList();
        bool IsPatientIdInDb(int patientId);
    }
}
