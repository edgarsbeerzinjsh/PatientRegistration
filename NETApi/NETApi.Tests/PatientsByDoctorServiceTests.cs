using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NETApi.Core.Exceptions;
using NETApi.Core.Models;
using NETApi.Data;
using NETApi.Services;

namespace NETApi.Tests
{
    public class PatientsByDoctorServiceTests
    {
        private PatientsByDoctorService _patientsByDoctorService;
        private NetApiDbContext _context;
        private DbService<Doctor> _dbDoctor;
        private DbService<Patient> _dbPatient;
        private DoctorService _doctorService;
        private PatientService _patientService;

        [SetUp]
        public void Setup()
        {
            TestDbSetup();
            _dbDoctor = new DbService<Doctor>(_context);
            _dbPatient = new DbService<Patient>(_context);

            _doctorService = new DoctorService(_context);
            _patientService = new PatientService(_context);

            _patientsByDoctorService = new PatientsByDoctorService(_context, _doctorService, _patientService);
            
            _dbDoctor.Create(defaultDoctor);
            _dbPatient.Create(defaultPatient);
            _dbPatient.Create(defaultNewPatient);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void AddExistingPatientToDoctor_newExistingPatient_DoctorAddedToPatient()
        {
            var doctorId = defaultDoctor.Id;
            var patientId = defaultNewPatient.Id;

            var patientWithUpdate = _patientsByDoctorService.AddExistingPatientToDoctor(patientId, doctorId);

            patientWithUpdate.DoctorPatient.Should().Contain(d => d.DoctorId == doctorId);
        }

        [Test]
        public void AddExistingPatientToDoctor_DoctorIdNotValid_ThrowsDoctorWithThisIdNotExistsException()
        {
            var doctorId = 8;
            var patientId = defaultNewPatient.Id;

            Action act = () => _patientsByDoctorService.AddExistingPatientToDoctor(patientId, doctorId);
            act.Should().Throw<DoctorWithThisIdDoesNotExistsException>();
        }

        [Test]
        public void AddExistingPatientToDoctor_PatientIdNotValid_ThrowsPatientWithThisIdNotExistsException()
        {
            var doctorId = defaultDoctor.Id;
            var patientId = 8;

            Action act = () => _patientsByDoctorService.AddExistingPatientToDoctor(patientId, doctorId);
            act.Should().Throw<PatientWithThisIdDoesNotExistsException>();
        }

        [Test]
        public void AddExistingPatientToDoctor_PatientAlreadyHasDoctor_ThrowsPatientAlreadyDoctorsPatientException()
        {
            var doctorId = defaultDoctor.Id;
            var patientId = defaultPatient.Id;

            Action act = () => _patientsByDoctorService.AddExistingPatientToDoctor(patientId, doctorId);
            act.Should().Throw<PatientAlreadyDoctorsPatientException>();
        }

        [Test]
        public void AddPatientToDoctor_DoctorIdNotValid_ThrowsDoctorWithThisIdNotExistsException()
        {
            var doctorId = 8;
            var patient = defaultNewPatient;

            Action act = () => _patientsByDoctorService.AddPatientToDoctor(patient, doctorId);
            act.Should().Throw<DoctorWithThisIdDoesNotExistsException>();
        }

        [Test]
        public void AddPatientToDoctor_PatientAlreadyHasDoctor_ThrowsPatientAlreadyDoctorsPatientException()
        {
            var doctorId = defaultDoctor.Id;
            var patient = defaultPatient;

            Action act = () => _patientsByDoctorService.AddPatientToDoctor(patient, doctorId);
            act.Should().Throw<PatientAlreadyDoctorsPatientException>();
        }

        [Test]
        public void AddPatientToDoctor_NewPatient_AddsPatientToDoctor()
        {
            var doctorId = defaultDoctor.Id;
            var patient = new Patient
            {
                Name = "Paul",
                Surname = "Paulson",
                EMail = "pauly@work.com",
                Telephone = "+33312345678",
                BirthDate = new DateTime(1990, 1, 1),
            };

            var newPatient = _patientsByDoctorService.AddPatientToDoctor(patient, doctorId);

            newPatient.DoctorPatient.Should().Contain(d => d.DoctorId == doctorId);
        }

        [Test, Order(1)]
        public void GetAllPatientsByDoctor_ValidDoctorId_ListWith1PatientReturned()
        {
            var doctorId = defaultDoctor.Id;

            var patientList = _patientsByDoctorService.GetAllPatientsByDoctor(doctorId);

            patientList.Count.Should().Be(1);
        }

        [Test]
        public void GetAllPatientsByDoctor_DoctorIdNotValid_ThrowsDoctorWithThisIdNotExistsException()
        {
            var doctorId = 8;

            Action act = () => _patientsByDoctorService.GetAllPatientsByDoctor(doctorId);
            act.Should().Throw<DoctorWithThisIdDoesNotExistsException>();
        }

        private void TestDbSetup()
        {
            var options = new DbContextOptionsBuilder<NetApiDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new NetApiDbContext(options);

            _context.Database.EnsureCreated();
        }

        private Doctor defaultDoctor = new Doctor
        {
            Id = 1,
            Name = "John",
            Surname = "Johnson",
            EMail = "john@hospital.com",
            Telephone = "+33312345678",
            Specialization = "Hands",
            DoctorPatient =
            {
                new DoctorPatient(1, 11)
            }
        };

        private Patient defaultPatient = new Patient
        {
            Id = 11,
            Name = "Johnny",
            Surname = "Johnson",
            EMail = "johnny@school.com",
            Telephone = "+33312345678",
            BirthDate = new DateTime(2000, 1, 1),
            DoctorPatient =
            {
                new DoctorPatient(1, 1),
            }
        };

        private Patient defaultNewPatient = new Patient
        {
            Id = 12,
            Name = "Andy",
            Surname = "Buzzer",
            EMail = "andy@home.com",
            Telephone = "+33312345678",
            BirthDate = new DateTime(2001, 1, 1),
            DoctorPatient =
            {
                new DoctorPatient(11, 12)
            }
        };
    }
}
