using System;
using System.Collections.Generic;

public class Product
{
    public char code { get; set; }
    public string name { get; set; }
    public ProductCategory productCategory { get; set; }

    public Dictionary<int,double> costs { get; set; } = new Dictionary<int,double>();

    public int multipackCount { get; set; } = 0;
    public Product? multipackProduct { get; set; } = null;    

    public Product(ProductCategory productCategory, string name)
    {
        this.productCategory = productCategory;
        this.name = name;
    }
}
