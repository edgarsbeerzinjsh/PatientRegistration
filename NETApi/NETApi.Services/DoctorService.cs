using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class DoctorService : DbService<Doctor>, IDoctorService
    {
        public DoctorService(INETApiDbContext context): base(context)
        {
        }
    }
}
