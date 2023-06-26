using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IPatientsByDoctorService
    {
        List<Patient> GetAllPatientsByDoctor(int id);
        Patient AddPatientToDoctor(Patient patient, int doctorId);
        Patient AddExistingPatientToDoctor(int patientId, int doctorId);
    }
}
