namespace NETApi.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string EMail { get; set; }
    }
}
