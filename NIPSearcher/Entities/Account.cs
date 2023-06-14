namespace NipSearcher.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public string? Number { get; set; }

        public override string ToString()
        {
            return $"Number: {Number}, Id: {AccountId}";
        }
    }
}
