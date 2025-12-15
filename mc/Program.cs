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

enum SyntaxKind
{
    
}

class SyntaxToken(SyntaxKind kind, int position, string text)
{
    public SyntaxKind Kind { get; } = kind;
    public int Position { get; } = position;
    public string Text { get; } = text;
}

class Lexer(string text)
{
    private readonly string _text = text;
    private int _position;
}