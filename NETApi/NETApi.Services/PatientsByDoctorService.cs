using Microsoft.EntityFrameworkCore;
using NETApi.Core.Exceptions;
using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class PatientsByDoctorService :  DbService<Patient>, IPatientsByDoctorService
    {
        protected readonly IDoctorService _doctorService;
        protected readonly IPatientService _patientService;

        public PatientsByDoctorService(
            INetApiDbContext context,
            IDoctorService doctorService,
            IPatientService patientService) : base(context)
        {
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public Patient AddExistingPatientToDoctor(int patientId, int doctorId)
        {
            IsDoctorIdValid(doctorId);

            if (!_patientService.IsPatientIdInDb(patientId))
            {
                throw new PatientWithThisIdDoesNotExistsException();
            }

            var existingPatient = GetKnownPatient(patientId);

            return UpdatePatientWithNewDoctor(existingPatient, doctorId);
        }

        public Patient AddPatientToDoctor(Patient patient, int doctorId)
        {
            IsDoctorIdValid(doctorId);

            var newPatient = GetKnownPatient(patient);
            if (newPatient == null)
            {
                newPatient = Create(patient);
            }

            return UpdatePatientWithNewDoctor(newPatient, doctorId);
        }

        public List<Patient> GetAllPatientsByDoctor(int id)
        {
            IsDoctorIdValid(id);

            var patients = _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .Where(p => p.DoctorPatient.Any(dp => dp.DoctorId == id))
                .ToList();

            return patients;
        }

        private void IsDoctorIdValid(int id)
        {
            if (!_doctorService.IsDoctorIdInDb(id))
            {
                throw new DoctorWithThisIdDoesNotExistsException();
            }
        }

        private bool IsPatientAlreadyDoctorPatient(Patient patient, DoctorPatient doctorPatient)
        {
            return patient.DoctorPatient.Any(dp =>
                dp.DoctorId == doctorPatient.DoctorId &&
                dp.PatientId == doctorPatient.PatientId);
        }

        private Patient GetKnownPatient(Patient patient)
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

        private Patient GetKnownPatient(int patientId)
        {
            return _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .FirstOrDefault(p => p.Id == patientId);
        }

        private Patient UpdatePatientWithNewDoctor(Patient patient, int doctorId)
        {
            var newDoctorPatient = new DoctorPatient(doctorId, patient.Id);
            if (IsPatientAlreadyDoctorPatient(patient, newDoctorPatient))
            {
                throw new PatientAlreadyDoctorsPatientException();
            }

            patient.DoctorPatient.Add(newDoctorPatient);

            Update(patient);

            return patient;
        }
    }
}
