namespace NETApi.Core.Exceptions
{
    public class PatientAlreadyDoctorsPatientException : Exception
    {
        public PatientAlreadyDoctorsPatientException() : base("Patient already is one of doctor patients")
        {
        }
    }
}
