namespace NETApi.Models
{
    public class DoctorDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string EMail { get; set; }
        public string Specialization { get; set; }
        public List<int>? PatientsId { get; set; } = new List<int>();
    }
}
