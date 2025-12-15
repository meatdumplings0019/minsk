namespace mc;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                return;
        }
    }
}

class SyntaxToken
{
    
}

class Lexer(string text)
{
    private readonly string _text = text;
    private int _position;
}