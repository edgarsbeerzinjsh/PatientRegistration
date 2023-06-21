namespace NETApi.Core.Models
{
    public class Doctor : Entity
    {
        public string Specialization { get; set; }
        public string License { get; set; }
        public List<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
    }
}
