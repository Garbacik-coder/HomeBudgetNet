namespace mgrNET.Store
{
    public class UpdateSpendingParams
    {
        public string name;

        public decimal value;

        public string category;

        public DateTime date;

        public UpdateSpendingParams(string name, decimal value, string category, DateTime date)
        {
            // this.id = id;
            this.name = name;
            this.value = value;
            this.category = category;
            this.date = date;
        }
    }
}