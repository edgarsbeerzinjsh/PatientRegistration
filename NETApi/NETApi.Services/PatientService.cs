using Microsoft.EntityFrameworkCore;
using NETApi.Core.Exceptions;
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

        public Patient AddExistingPatientToDoctor(int patientId, int doctorId)
        {
            var existingPatient = GetKnownPatient(patientId);
            if (existingPatient == null)
            {
                throw new PatientWithThisIdDoesNotExistsException();
            }

            //var newDoctorPatient = new DoctorPatient(doctorId, patientId);
            //if (IsPatientAlreadyDoctorPatient(existingPatient, newDoctorPatient))
            //{
            //    throw new PatientAlreadyDoctorsPatientException();
            //}

            //existingPatient.DoctorPatient.Add(newDoctorPatient);

            //Update(existingPatient);

            return UpdatePatientWithNewDoctor(existingPatient, doctorId);
        }

        public Patient AddPatientToDoctor(Patient patient, int doctorId)
        {
            var newPatient = GetKnownPatient(patient);
            if (newPatient == null)
            {
                newPatient = Create(patient);
            }

            //var newDoctorPatient = new DoctorPatient(doctorId, newPatient.Id);
            //if (IsPatientAlreadyDoctorPatient(newPatient, newDoctorPatient))
            //{
            //    throw new PatientAlreadyDoctorsPatientException();
            //}
            
            //newPatient.DoctorPatient.Add(newDoctorPatient);

            //Update(newPatient);

            return UpdatePatientWithNewDoctor(newPatient, doctorId);
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
                    p.Telephone == patient.Telephone &&
                    p.Address == patient.Address);
        }

        private Patient GetKnownPatient(int patientId)
        {
            return _dbContext.Patients
                .Include(p => p.DoctorPatient)
                .FirstOrDefault(p => p.Id == patientId);
        }

        private bool IsPatientAlreadyDoctorPatient(Patient patient, DoctorPatient doctorPatient)
        {
            return patient.DoctorPatient.Any(dp =>
                dp.DoctorId == doctorPatient.DoctorId &&
                dp.PatientId == doctorPatient.PatientId);
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
