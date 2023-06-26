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

        public List<Patient> GetAllPatientsFullList()
        {
            var patients = _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .ToList();

            return patients;
        }
    }
}
