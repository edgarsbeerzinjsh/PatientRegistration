using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IPatientService : IDbService<Patient>
    {
        List<Patient> GetAllPatientsByDoctor(int id);
        List<Patient> GetAllPatientsFullList();
        Patient AddPatientToDoctor(Patient patient, int doctorId);
        Patient AddExistingPatientToDoctor(int patientId, int doctorId);
    }
}
