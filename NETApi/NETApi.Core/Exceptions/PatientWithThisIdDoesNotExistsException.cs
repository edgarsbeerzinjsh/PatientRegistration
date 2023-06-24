namespace NETApi.Core.Exceptions
{
    public class PatientWithThisIdDoesNotExistsException : Exception
    {
        public PatientWithThisIdDoesNotExistsException() : base("Patient with this Id does not exist.")
        {
        }
    }
}
