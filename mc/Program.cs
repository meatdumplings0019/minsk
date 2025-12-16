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
            
            var lexer =  new Lexer(line);
            while (true)
            {
                var token = lexer.NextToken();
                if (token.Kind == SyntaxKind.EndOfFileToken)
                    break;
                
                Console.Write($"{token.Kind}: '{token.Text}'");
                if (token.Value != null)
                    Console.Write($" {token.Value}");
                
                Console.WriteLine();
            }
        }
    }
}

enum SyntaxKind
{
    NumberToken,
    WhiteSpaceToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,
    BadToken,
    EndOfFileToken
}

class SyntaxToken(SyntaxKind kind, int position, string text, object? value)
{
    public SyntaxKind Kind { get; } = kind;
    public int Position { get; } = position;
    public string Text { get; } = text;
    public object? Value { get; } = value;
}

class Lexer(string text)
{
    private int _position;
    private readonly string _text = text;

    private char Current => _position >= _text.Length ? '\0' : _text[_position];

    private void Next() => _position++;

    public SyntaxToken NextToken()
    {
        if (_position >= _text.Length)
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
        
        if (char.IsDigit(Current))
        {
            var start = _position;

            while (char.IsDigit(Current))
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            int.TryParse(text, out var value);
            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }

        if (char.IsWhiteSpace(Current))
        {
            var start = _position;

            while (char.IsWhiteSpace(Current))
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, null);
        }

        return Current switch
        {
            '+' => new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null),
            '-' => new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null),
            '*' => new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null),
            '/' => new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null),
            '(' => new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null),
            ')' => new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null),
            _ => new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null)
        };
    }
}

class Parser
{
    private readonly SyntaxToken[] _tokens;
    private int _position;
    
    public Parser(string text)
    {
        var tokens = new List<SyntaxToken>();
        
        var lexer = new Lexer(text);
        SyntaxToken token;
        do
        {
            token = lexer.NextToken();
            
            if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                token.Kind != SyntaxKind.BadToken)
                tokens.Add(token);
            
        } while(token.Kind != SyntaxKind.EndOfFileToken);
        
        _tokens = tokens.ToArray();
    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        return index >= _tokens.Length ? _tokens[^1] : _tokens[index];
    }
    
    private SyntaxToken Current => Peek(0);
}
