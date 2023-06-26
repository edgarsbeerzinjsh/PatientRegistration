using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NETApi.Core.Models;
using NETApi.Data;
using NETApi.Services;

namespace NETApi.Tests
{
    public class DbServiceTests
    {
        private NetApiDbContext _context;
        private DbService<Doctor> _dbDoctor;

        [SetUp]
        public void Setup()
        {
            TestDbSetup();
            _dbDoctor = new DbService<Doctor>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void CreateDoctor_DefaultDoctor_DoctorAddedAndReturned()
        {
            var alreadyDoctorsInDb = _context.Doctors.Count();

            var newDoctor = _dbDoctor.Create(defaultDoctor);

            _context.Doctors.Count().Equals(alreadyDoctorsInDb + 1);
            newDoctor.Should().Be(defaultDoctor);
        }

        [Test]
        public void ReadDoctor_DefaultDoctor_DoctorReturned()
        {
            _dbDoctor.Create(defaultDoctor);

            var readDoctor = _dbDoctor.Read(defaultDoctor.Id);

            readDoctor.Should().Be(defaultDoctor);
        }

        [Test]
        public void UpdateDoctor_DefaultDoctor_UpdatedDoctorReturned()
        {
            _dbDoctor.Create(defaultDoctor);
            defaultDoctor.Name = "Jimmy";

            _dbDoctor.Update(defaultDoctor);
            var newNameForDoctor = _dbDoctor.Read(defaultDoctor.Id);

            newNameForDoctor.Name.Should().Be("Jimmy");
        }

        [Test]
        public void RemoveDoctor_DefaultDoctor_NoDoctorsInDb()
        {
            _dbDoctor.Create(defaultDoctor);
            var doctorInDb = _dbDoctor.Read(defaultDoctor.Id);

            doctorInDb.Should().NotBeNull();

            _dbDoctor.Remove(defaultDoctor);
            doctorInDb = _dbDoctor.Read(defaultDoctor.Id);

            doctorInDb.Should().BeNull();
        }

        [Test]
        public void GetAllDoctors_Add3Doctors_ReturnedDoctorCount3()
        {
            _dbDoctor.Create(defaultDoctor);
            defaultDoctor.Id = 2;
            _dbDoctor.Create(defaultDoctor);
            defaultDoctor.Id = 3;
            _dbDoctor.Create(defaultDoctor);

            var doctorList = _dbDoctor.GetAll();

            doctorList.Count.Should().Be(3);
        }

        [Test]
        public void RemoveAllDoctors_Add3Doctors_AfterRemoveAllNoDoctorsInDb()
        {
            _dbDoctor.Create(defaultDoctor);
            defaultDoctor.Id = 2;
            _dbDoctor.Create(defaultDoctor);
            _context.Doctors.Count().Should().Be(2);

            _dbDoctor.RemoveAll();

            _context.Doctors.Should().BeEmpty();
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