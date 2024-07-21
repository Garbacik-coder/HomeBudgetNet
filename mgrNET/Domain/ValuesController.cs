namespace mgrNET.Domain
{
    public class Spending
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public Spending() { }

        public Spending(int id, string name, decimal value, string category, DateTime date)
        {
            this.Id = id;
            this.Name = name;
            this.Value = value;
            this.Category = category;
            this.Date = date;
        }

        public Spending(Spending spending)
        {
            this.Id = spending.Id;
            this.Name = spending.Name;
            this.Value = spending.Value;
            this.Category = spending.Category;
            this.Date = spending.Date;
        }
    }
}
