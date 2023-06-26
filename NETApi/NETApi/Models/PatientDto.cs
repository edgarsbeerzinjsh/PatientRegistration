﻿namespace NETApi.Models
{
    public class PatientDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string EMail { get; set; }
        public DateTime BirthDate { get; set; }
        public List<int>? DoctorsId { get; set; } = new List<int>();
    }
}
