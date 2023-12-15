public static class Checkout
{
    public static void Execute()
    {
        Scanner scanner = new Scanner();

        List<Product> products = DevelopmentData.Setup();

        ExpensiveCalculator expensiveCalculator = new ExpensiveCalculator(products);
        Scanner.CodesHandler expensiveCalculatorDelegate = expensiveCalculator.Execute;
        scanner.CodesEvent += expensiveCalculatorDelegate;

        CheapCalculator cheapCalculator = new CheapCalculator(products);
        Scanner.CodesHandler cheapCalculatorDelegate = cheapCalculator.Execute;
        scanner.CodesEvent += cheapCalculatorDelegate;

        scanner.Scan();
    }
}
