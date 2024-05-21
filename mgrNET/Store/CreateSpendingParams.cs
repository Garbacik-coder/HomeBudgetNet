namespace mgrNET.Store;

public class CreateSpendingParams
{
    public int id;

    public string name;

    public decimal value;

    public string category;

    public DateTime date;

    public CreateSpendingParams(int id, string name, decimal value, string category, DateTime date)
    {
        this.id = id;
        this.name = name;
        this.value = value;
        this.category = category;
        this.date = date;
    }
}

