using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NETApi.Core.Models;
using NETApi.Data;
using NETApi.Services;

namespace NETApi.Tests
{
    public class PatientServiceTests
    {
        private NetApiDbContext _context;
        private PatientService _patientService;
        private DbService<Patient> _dbPatient;

        [SetUp]
        public void Setup()
        {
            TestDbSetup();
            _dbPatient = new DbService<Patient>(_context);
            _dbPatient.Create(defaultPatient);
            _patientService = new PatientService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void IsPatientInDb_newId_ReturnedFalse()
        {
            var newId = 3;

            var isPatientInDb = _patientService.IsPatientIdInDb(newId);

            isPatientInDb.Should().BeFalse();
        }

        [Test]
        public void IsPatientInDb_usedId_ReturnedTrue()
        {
            var newId = 1;

            var isPatientInDb = _patientService.IsPatientIdInDb(newId);

            isPatientInDb.Should().BeTrue();
        }

        [Test]
        public void GetAllPatientsFullList_AddOneMorePatient_ReturnedListCountMustBe2()
        {
            _dbPatient.Create(defaultNewPatient);

            var patientList = _patientService.GetAllPatientsFullList();

            patientList.Count.Should().Be(2);
            patientList[0].DoctorPatient.Should().NotBeNull();
        }

        private void TestDbSetup()
        {
            var options = new DbContextOptionsBuilder<NetApiDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new NetApiDbContext(options);

            _context.Database.EnsureCreated();
        }

        private Patient defaultPatient = new Patient
        {
            Id = 1,
            Name = "John",
            Surname = "Johnson",
            EMail = "john@hospital.com",
            Telephone = "+33312345678",
            BirthDate = new DateTime(2000, 1, 1),
            DoctorPatient =
            {
                new DoctorPatient(11, 1),
                new DoctorPatient(12, 1)
            }
        };

        private Patient defaultNewPatient = new Patient
        {
            Id = 2,
            Name = "Andy",
            Surname = "Buzzer",
            EMail = "andy@home.com",
            Telephone = "+33312345678",
            BirthDate = new DateTime(2000, 1, 1),
            DoctorPatient =
            {
                new DoctorPatient(11, 2)
            }
        };
    }
}
