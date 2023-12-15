public class Calculator
{
    public List<Product> supportedProducts;

    public Calculator(List<Product> supportedProducts)
    {
        this.supportedProducts = supportedProducts;
    }

    public List<Product> GetProducts(string codes)
    {
        List<Product> products = new List<Product>();
        foreach (char code in codes)
        {
            Product? product = this.supportedProducts.Find(p => p.code == code);

            if (product != null) {
                products.Add(product);
            }
        }
        return products;
    }

    public List<(Product product, int count)> GetProductsWithCounts(List<Product> products)
    {
        List<(Product,int)> productsWithCounts = new List<(Product,int)>();
        Dictionary<char,int> codesWithCounts = new Dictionary<char,int>();
        foreach (Product product in products)
        {
            if (!codesWithCounts.ContainsKey(product.code)) {
                codesWithCounts[product.code] = 1;
            }
            else {
                codesWithCounts[product.code] += 1;
            }
        }
        foreach (var codeWithCount in codesWithCounts)
        {
            Product? product = this.supportedProducts.Find(
                p => p.code == codeWithCount.Key
            );
            if (product != null)
            {
                productsWithCounts.Add((product, codeWithCount.Value));
            }
        }
        return productsWithCounts;
    }

    public double GetTotalProductCost((Product product, int count) productWithCount)
    {
        double totalProductCost = 0.0;
        int remainingCount = productWithCount.count;
        while (remainingCount > 0)
        {
            int costToCheck = remainingCount;
            while (costToCheck > 1)
            {
                if (productWithCount.product.costs.ContainsKey(costToCheck))
                {
                    break;
                }
                else
                {
                    costToCheck -= 1;
                }
            }
            totalProductCost += productWithCount.product.costs[costToCheck];
            remainingCount -= costToCheck;
        }
        return totalProductCost;
    }

    public List<(Product product, int count, double cost)> GetProductsWithCosts(List<(Product product, int count)> productsWithCounts)
    {
        var productsWithCosts = new List<(Product product, int count, double cost)>();
        foreach (var productWithCount in productsWithCounts)
        {
            double cost = this.GetTotalProductCost(productWithCount);
            productsWithCosts.Add((productWithCount.product, productWithCount.count, cost));
        }
        return productsWithCosts;
    }

    public double GetTotalCost(List<(Product product, int count, double cost)> productsWithCosts)
    {
        double totalCost = 0.0;
        foreach (var productWithCost in productsWithCosts)
        {
            totalCost += productWithCost.cost;
        }
        return totalCost;
    }

    public List<(string categoryName, string productName, int count, double cost)> GetOutputData(List<(Product product, int count, double cost)> productsWithCosts)
    {
        var outputData = new List<(string categoryName, string productName, int count, double cost)>();

        var query1 =
            from p in productsWithCosts
            where p.product.multipackCount == 0
            select p;
        
        foreach (var p in query1)
        {
            outputData.Add((p.product.productCategory.name, p.product.name, p.count, p.cost));
        }

        var query2 =
            from p in productsWithCosts
            where p.product.multipackCount > 0
            where p.product.multipackProduct != null
            select p;
        
        foreach (var p in query2)
        {
            // this if only exists because otherwise there are warnings.
            // the above query should handle it.
            if (p.product.multipackProduct != null)
            {
                (string categoryName, string productName, int count, double cost)? existingP =
                outputData.Find(x => x.productName == p.product.multipackProduct.name);

                if (existingP != null) {
                    outputData.RemoveAll(x => x.productName == p.product.multipackProduct.name);
                }

                int updatedCount = (existingP?.count ?? 0) + (p.count * p.product.multipackCount);
                double updatedCost = (existingP?.cost ?? 0.0) + p.cost;
                outputData.Add((p.product.productCategory.name, p.product.multipackProduct.name, updatedCount, updatedCost));
            }
        }

        return outputData;
    }
}

public class CheapCalculator : Calculator
{
    public CheapCalculator(List<Product> products) : base(products) {}

    public void Execute(string codes)
    {
        var products = this.GetProducts(codes);
        var productsWithCounts = this.GetProductsWithCounts(products);
        var productsWithCosts = this.GetProductsWithCosts(productsWithCounts);

        var totalCost = this.GetTotalCost(productsWithCosts);
        Console.WriteLine("Total: " + totalCost.ToString() + " kr.");
    }
}

public class ExpensiveCalculator : Calculator
{
    public ExpensiveCalculator(List<Product> products) : base(products) {}

    public void Execute(string codes)
    {
        var products = this.GetProducts(codes);
        var productsWithCounts = this.GetProductsWithCounts(products);
        var productsWithCosts = this.GetProductsWithCosts(productsWithCounts);

        var outputData = this.GetOutputData(productsWithCosts);

        var outputQuery =
            from p in outputData
            orderby p.categoryName descending
            select p;

        foreach (var p in outputQuery)
        {
            Console.WriteLine(
                "Varegruppe: " + p.categoryName + ", " +
                "Vare: " + p.productName + ", " +
                "stk: " + p.count.ToString() + ", " +
                "Pris: " + p.cost.ToString() + " kr."
            );
        }
    }
}
