using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IPatientService : IDbService<Patient>
    {
        Patient GetKnownPatient(Patient patient);
        bool IsPatientIdInDb(int patientId);
    }
}
