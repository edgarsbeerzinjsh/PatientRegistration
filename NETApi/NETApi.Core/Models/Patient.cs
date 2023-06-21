namespace NETApi.Core.Models
{
    public class Patient : Entity
    {
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public List<DoctorPatient> DoctorPatient { get; set; } = new List<DoctorPatient>();
    }
}
