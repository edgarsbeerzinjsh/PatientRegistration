using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NETApi.Core.Models;
using NETApi.Data;
using NETApi.Services;

namespace NETApi.Tests
{
    public class DoctorServiceTests
    {
        private NetApiDbContext _context;
        private DoctorService _doctorService;
        private DbService<Doctor> _dbDoctor;

        [SetUp]
        public void Setup()
        {
            TestDbSetup();
            _dbDoctor = new DbService<Doctor>(_context);
            _dbDoctor.Create(defaultDoctor);
            _doctorService = new DoctorService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void IsDoctorInDb_newId_ReturnedFalse()
        {
            var newId = 2;

            var isDoctorInDb = _doctorService.IsDoctorIdInDb(newId);

            isDoctorInDb.Should().BeFalse();
        }

        [Test]
        public void IsDoctorInDb_usedId_ReturnedTrue()
        {
            var newId = 1;

            var isDoctorInDb = _doctorService.IsDoctorIdInDb(newId);

            isDoctorInDb.Should().BeTrue();
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
                new DoctorPatient(1, 11),
                new DoctorPatient(1, 12)
            }
        };
    }
}
