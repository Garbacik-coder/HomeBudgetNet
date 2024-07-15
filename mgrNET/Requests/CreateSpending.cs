namespace mgrNET.Requests
{
    public class CreateSpending
    {
        public int id { get; set; }

        public string? name { get; set; }

        public decimal value { get; set; }

        public string? category { get; set; }

        public DateTime date { get; set; }
    }
}