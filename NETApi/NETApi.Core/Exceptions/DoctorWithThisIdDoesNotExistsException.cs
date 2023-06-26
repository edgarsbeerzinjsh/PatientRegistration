namespace NETApi.Core.Exceptions
{
    public class DoctorWithThisIdDoesNotExistsException : Exception
    {
        public DoctorWithThisIdDoesNotExistsException() : base("Doctor with this Id does not exist.")
        {
        }
    }
}
