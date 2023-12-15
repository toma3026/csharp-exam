public class ProductCategory
{
    public int id { get; set; }
    public string name { get; set; }

    public ProductCategory(string name)
    {
        this.name = name;
    }
}
