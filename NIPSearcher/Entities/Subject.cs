namespace NipSearcher.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }

        public string? Name { get; set; }

        public string? Nip { get; set; }

        public string? StatusVat { get; set; }
        public string? Regon { get; set; }

        public string? Pesel { get; set; }

        public string? Krs { get; set; }

        public string? ResidenceAddress { get; set; }

        public string? WorkingAddress { get; set; }

        public virtual ICollection<Person> Representatives { get; set; }

        public string? RestorationDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public bool HasVirtualAccounts { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, VAT:{StatusVat}, NIP:{Nip}, Id: {SubjectId} \n   Accounts:\n      {string.Join("\n    ", Accounts)}\n   Persons:\n      {string.Join("\n    ", Representatives)}\n";
        }

       public Subject()
        {
            Accounts = new List<Account>();
            Representatives = new List<Person>();
        }
    }
}
