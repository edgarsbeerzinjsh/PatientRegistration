namespace NETApi.Core.Models
{
    public class DoctorPatient
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set;}

        public DoctorPatient(int doctorId, int patientId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
