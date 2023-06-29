using NETApi.Core.Exceptions;
using NETApi.Core.IServices;
using NETApi.Core.Models;

namespace NETApi.Services
{
    public class PatientsByDoctorService :  IPatientsByDoctorService
    {
        protected readonly IDoctorService _doctorService;
        protected readonly IPatientService _patientService;

        public PatientsByDoctorService(IDoctorService doctorService, IPatientService patientService)
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

            var existingPatient = _patientService.Read(patientId);

            return UpdatePatientWithNewDoctor(existingPatient, doctorId);
        }

        public Patient AddPatientToDoctor(Patient patient, int doctorId)
        {
            IsDoctorIdValid(doctorId);

            var newPatient = _patientService.GetKnownPatient(patient);
            if (newPatient == null)
            {
                newPatient = _patientService.Create(patient);
            }

            return UpdatePatientWithNewDoctor(newPatient, doctorId);
        }

        public List<Patient> GetAllPatientsByDoctor(int id)
        {
            IsDoctorIdValid(id);

            var patients = _patientService.GetAll()
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

        private Patient UpdatePatientWithNewDoctor(Patient patient, int doctorId)
        {
            var newDoctorPatient = new DoctorPatient(doctorId, patient.Id);
            if (IsPatientAlreadyDoctorPatient(patient, newDoctorPatient))
            {
                throw new PatientAlreadyDoctorsPatientException();
            }

            patient.DoctorPatient.Add(newDoctorPatient);

            _patientService.Update(patient);

            return patient;
        }
    }
}
