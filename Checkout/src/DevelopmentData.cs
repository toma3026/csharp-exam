public static class DevelopmentData
{
    public static List<Product> Setup()
    {
        List<Product> products = new List<Product>();

        ProductCategory productCategory;
        Product product;
        Product multiProduct;

        productCategory = new ProductCategory("Mejeri") {id = 1};

        product = new Product(productCategory, "Mælk") {code = 'A'};
        product.costs[1] = 10.00;
        product.costs[2] = 15.00;
        product.costs[3] = 20.00;
        products.Add(product);

        multiProduct = new Product(productCategory, "Mælkekasse") {code = 'B'};
        multiProduct.costs[1] = 50.00;
        multiProduct.multipackCount = 12;
        multiProduct.multipackProduct = product;
        products.Add(multiProduct);

        product = new Product(productCategory, "Fløde") {code = 'C'};
        product.costs[1] = 20.00;
        product.costs[2] = 30.00;
        products.Add(product);

        productCategory = new ProductCategory("Frost") {id = 2};

        product = new Product(productCategory, "Ærter") {code = 'D'};
        product.costs[1] = 5.00;
        products.Add(product);

        product = new Product(productCategory, "Majs") {code = 'E'};
        product.costs[1] = 12.00;
        product.costs[2] = 24.00;
        products.Add(product);

        productCategory = new ProductCategory("Pant") {id = 3};

        product = new Product(productCategory, "Lille flaske") {code = 'F'};
        product.costs[1] = -1.00;
        products.Add(product);

        product = new Product(productCategory, "Stor flaske") {code = 'G'};
        product.costs[1] = -2.00;
        products.Add(product);

        return products;
    }
}
