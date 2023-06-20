namespace NETApi.Core.Models
{
    public class Doctor : Entity
    {
        public string Specialization { get; set; }
        public string License { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
