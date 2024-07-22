namespace mgrNET.Requests
{
    public class UpdateSpending
    {
        public string? name { get; set; }

        public decimal value { get; set; }

        public string? category { get; set; }

        public DateTime date { get; set; }
    }
}