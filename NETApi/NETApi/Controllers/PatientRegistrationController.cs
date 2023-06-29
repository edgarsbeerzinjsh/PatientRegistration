using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Models;

namespace NETApi.Controllers
{
    //[Route("[controller]")]
    [ApiController]
    public class PatientRegistrationController : ControllerBase
    {
        private readonly IDoctorService _dbDoctor;
        private readonly IPatientService _dbPatient;
        private readonly IPatientsByDoctorService _patientsByDoctorService;
        private readonly IMapper _mapper;

        public PatientRegistrationController(
            IDoctorService dbDoctor, 
            IPatientService dbPatient,
            IPatientsByDoctorService patientsByDoctorService,
            IMapper mapper)
        {
            _dbDoctor = dbDoctor;
            _dbPatient = dbPatient;
            _patientsByDoctorService = patientsByDoctorService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("createDoctor")]
        public IActionResult AddDoctor(DoctorDto doctor)
        {
            var newDoctor = _dbDoctor.Create(_mapper.Map<Doctor>(doctor));

            return Created("", newDoctor);
        }

        [HttpDelete]
        [Route("deleteDoctor/{id}")]
        public IActionResult DeleteDoctorById(int id)
        {
            var existingDoctor = _dbDoctor.Read(id);
            if (existingDoctor == null)
            {
                return BadRequest("Id does not match any doctor");
            }

            _dbDoctor.Remove(existingDoctor);

            return Ok("Doctor deleted");
        }

        [HttpDelete]
        [Route("deleteAllDoctors")]
        public IActionResult DeleteAllDoctors()
        {
            _dbDoctor.RemoveAll();

            return Ok("All doctors deleted");
        }

        [HttpPost]
        [Route("createPatient")]
        public IActionResult AddPatient(PatientDto patient)
        {
            var newPatient = _dbPatient.Create(_mapper.Map<Patient>(patient));

            return Created("", newPatient);
        }

        [HttpDelete]
        [Route("deleteAllPatients")]
        public IActionResult DeleteAllPatients()
        {
            _dbPatient.RemoveAll();

            return Ok("All patients deleted");
        }

        [HttpDelete]
        [Route("deletePatient")]
        public IActionResult DeletePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);

            var existingPatient = _dbPatient.GetKnownPatient(patient);
            if (existingPatient == null)
            {
                return BadRequest("Matching patient not found");
            }

            _dbPatient.Remove(existingPatient);

            return Ok("Patient deleted");
        }

        [HttpDelete]
        [Route("deletePatient/{id}")]
        public IActionResult DeletePatientById(int id)
        {
            var existingPatient = _dbPatient.Read(id);
            if (existingPatient == null)
            {
                return BadRequest("Id does not match any patient");
            }

            _dbPatient.Remove(existingPatient);

            return Ok("Patient deleted");
        }

        [HttpPut]
        [Route("addPatient/{doctorId}")]
        public IActionResult AddPatientToDoctor(PatientDto patientDto, int doctorId)
        {
            try
            {
                var patient = _mapper.Map<Patient>(patientDto);
                _patientsByDoctorService.AddPatientToDoctor(patient, doctorId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Patient added to doctor");
        }

        [HttpPut]
        [Route("addPatient/{doctorId}/{patientId}")]
        public IActionResult AddExistingPatientToDoctor(int patientId, int doctorId)
        {
            try
            {
                _patientsByDoctorService.AddExistingPatientToDoctor(patientId, doctorId);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Patient added to doctor");
        }

        [HttpGet]
        [Route("patients/{doctorId}")]
        public IActionResult GetDoctorsPatients(int doctorId)
        {
            List<Patient> patients;

            try
            {
                patients = _patientsByDoctorService.GetAllPatientsByDoctor(doctorId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(_mapper.Map<List<PatientDto>>(patients));
        }

        [HttpGet]
        [Route("patients/all")]
        public IActionResult GetAllPatients()
        {
            var patients = _dbPatient.GetAll();

            return Ok(_mapper.Map<List<PatientDto>>(patients));
        }
    }
}
