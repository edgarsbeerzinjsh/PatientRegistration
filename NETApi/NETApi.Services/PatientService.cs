using Microsoft.EntityFrameworkCore;
using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class PatientService : DbService<Patient>, IPatientService
    {
        public PatientService(INETApiDbContext context) : base(context)
        {
        }

        public List<Patient> GetAllPatientsByDoctor(int id)
        {
            var patients = _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .Where(p => p.DoctorPatient.Any(dp => dp.DoctorId == id))
                .ToList();

            return patients;
        }

        public List<Patient> GetAllPatientsFullList()
        {
            var patients = _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .ToList();

            return patients;
        }

        public Patient AddPatientToDoctor(Patient patient, int doctorId)
        {
            var newPatient = Create(patient);
            var newDoctorPatient = new DoctorPatient(doctorId, newPatient.Id);
            newPatient.DoctorPatient.Add(newDoctorPatient);

            Update(newPatient);

            return newPatient;
        }
    }
}
