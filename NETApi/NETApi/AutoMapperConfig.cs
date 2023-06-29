using AutoMapper;
using NETApi.Core.Models;
using NETApi.Models;

namespace NETApi
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Patient, PatientDto>()
                    .ForMember(p => p.DoctorsId,
                        opt => 
                            opt.MapFrom(pd => pd.DoctorPatient
                                .Select(dp => dp.DoctorId)));
                cfg.CreateMap<PatientDto, Patient>()
                    .ForMember(p => p.DoctorPatient, opt => opt.Ignore());
                cfg.CreateMap<Doctor, DoctorDto>()
                    .ForMember(d => d.PatientsId,
                        opt =>
                            opt.MapFrom(pd => pd.DoctorPatient
                                .Select(dp => dp.PatientId)));
                cfg.CreateMap<DoctorDto, Doctor>()
                    .ForMember(d => d.DoctorPatient, opt => opt.Ignore());
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
