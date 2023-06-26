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

        [HttpPut]
        [Route("createDoctors")]
        public IActionResult AddDoctor(Doctor doctor)
        {
            _dbDoctor.Create(doctor);

            return Ok("Doctor added");
        }

        [HttpDelete]
        [Route("DeleteDoctors")]
        public IActionResult DeleteAllDoctors()
        {
            _dbDoctor.RemoveAll();

            return Ok("Doctors deleted");
        }

        [HttpPut]
        [Route("createPatient")]
        public IActionResult AddPatient(Patient patient)
        {
            _dbPatient.Create(patient);

            return Ok("Patient added");
        }

        [HttpDelete]
        [Route("DeletePatients")]
        public IActionResult DeleteAllPatients()
        {
            _dbPatient.RemoveAll();

            return Ok("Patients deleted");
        }

        [HttpPut]
        [Route("{doctorId}")]
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
        [Route("{doctorId}/{patientId}")]
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
        [Route("{doctorId}")]
        public IActionResult GetDoctorsPatients(int doctorId)
        {
            var patients = new List<Patient>();

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
        [Route("allPatients")]
        public IActionResult GetAllPatients()
        {
            var patients = _dbPatient.GetAllPatientsFullList();

            return Ok(_mapper.Map<List<PatientDto>>(patients));
        }
    }
}
