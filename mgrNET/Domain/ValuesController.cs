namespace mgrNET.Domain
{
    public class Spending
    {
        private int id;

        private string name;

        private decimal value;

        private string category;

        private DateTime date;
        private Spending spending;

        public Spending(int id, string name, decimal value, string category, DateTime date)
        {
            this.id = id;
            this.name = name;
            this.value = value;
            this.category = category;
            this.date = date;
        }

        public Spending(Spending spending)
        {
            this.spending = spending;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public decimal getValue()
        {
            return value;
        }

        public void setValue(decimal value)
        {
            this.value = value;
        }

        public String getCategory()
        {
            return category;
        }

        public void setCategory(String category)
        {
            this.category = category;
        }

        public DateTime getDate()
        {
            return date;
        }

        public void setDate(DateTime date)
        {
            this.date = date;
        }
    }
}