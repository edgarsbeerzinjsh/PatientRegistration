using Microsoft.AspNetCore.Mvc;
using NETApi.Core.IServices;
using NETApi.Core.Models;

namespace NETApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRegistrationController : ControllerBase
    {
        private readonly IDoctorService _dbDoctor;
        private readonly IDbService<Patient> _dbPatient;

        public PatientRegistrationController(IDoctorService dbDoctor, IDbService<Patient> dbPatient)
        {
            _dbDoctor = dbDoctor;
            _dbPatient = dbPatient;
        }

        //[HttpPut]
        //[Route("createDoctors")]
        //public IActionResult AddPatientToDoctor(Doctor doctor)
        //{
        //    _dbDoctor.Create(doctor);

        //    return Ok("Doctor added");
        //}

        [HttpPut]
        [Route("{doctorId}")]
        public IActionResult AddPatientToDoctor(Patient patient, int doctorId)
        {
            var newPatient = _dbPatient.Create(patient);
            var doctor = _dbDoctor.Read(doctorId);

            var newDoctorPatient = new DoctorPatient(doctorId, newPatient.Id);
            

            doctor.DoctorPatients.Add(newDoctorPatient);
            newPatient.DoctorPatient.Add(newDoctorPatient);

            _dbDoctor.Update(doctor);
            _dbPatient.Update(newPatient);
            //_dbDoctor.Create(doctor);

            return Ok("Patient added");
        }

        [HttpGet]
        [Route("{doctorId}")]
        public IActionResult GetDoctorsPatients(int doctorId)
        {
            var doctorPatients = _dbDoctor.GetAllPatients(doctorId);

            return Ok(doctorPatients);
        }
    }
}
