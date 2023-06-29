using Microsoft.EntityFrameworkCore;
using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class PatientService : DbService<Patient>, IPatientService
    {
        public PatientService(INetApiDbContext context) : base(context)
        {
        }

        public bool IsPatientIdInDb(int patientId)
        {
            return _dbContext.Patients.Any(p => p.Id == patientId);
        }

        public Patient GetKnownPatient(Patient patient)
        {
            return _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .FirstOrDefault(p =>
                    p.Name == patient.Name &&
                    p.Surname == patient.Surname &&
                    p.BirthDate == patient.BirthDate &&
                    p.EMail == patient.EMail &&
                    p.Telephone == patient.Telephone);
        }
    }
}
