public class Scanner
{
    public delegate void CodesHandler(string codes);
    public event CodesHandler? CodesEvent;

    public void Scan()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome");
        Console.WriteLine("Products exist for the following codes: A-G");
        Console.WriteLine("Example of a code sequence that works: ABCDEFGABCDEFG");
        Console.WriteLine("Enter a code sequence below:");
        Console.WriteLine();
        
        string? allCodes = Console.ReadLine();

        if (allCodes != null)
        {
            string remainingCodes = allCodes;
            string codes = "";
            while (remainingCodes.Length > 0)
            {
                codes = codes + remainingCodes[0].ToString();
                remainingCodes = remainingCodes.Substring(1);

                System.Threading.Thread.Sleep(500);
                Console.WriteLine();
                CodesEvent?.Invoke(codes);
            }
        }
    }
}
