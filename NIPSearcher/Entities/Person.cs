namespace NipSearcher.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public Subject? Subject { get; set; }

        public string? CompanyName { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Pesel { get; set; }
        public string? Nip { get; set; }


        public override string ToString()
        {
            return $"Name:{FirstName} {LastName}, Id: {PersonId}";
        }
    }
}
