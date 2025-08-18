// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis;

class Lexer(string text)
{
    private int _positon;
    private readonly List<string> _diagnostics = [];

    private char Current => _positon >= text.Length ? '\0' : text[_positon];
    
    public IEnumerable<string> Diagnostics => _diagnostics;

    private void Next()
    {
        _positon++;
    }
    
    public SyntaxToken NextToken()
    {
        if (_positon >= text.Length)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _positon, "\0", null);
        }
        
        if (char.IsDigit(Current))
        {
            var start = _positon;

            while (char.IsDigit(Current))
                Next();
            
            var length = _positon - start;
            var text1 = text.Substring(start, length);
            if (!int.TryParse(text1, out var value))
                _diagnostics.Add($"The number {text} isn't valid Int32.");
            
            return new SyntaxToken(SyntaxKind.NumberToken, start, text1, value);
        }

        if (char.IsWhiteSpace(Current))
        {
            var start = _positon;

            while (char.IsWhiteSpace(Current))
                Next();
            
            var length = _positon - start;
            var text1 = text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text1, null);
        }

        switch (Current)
        {
            case '+':
                return new SyntaxToken(SyntaxKind.PlusToken, _positon++, "+", null);
            case '-':
                return new SyntaxToken(SyntaxKind.MinusToken, _positon++, "-", null);
            case '*':
                return new SyntaxToken(SyntaxKind.StarToken, _positon++, "*", null);
            case '/':
                return new SyntaxToken(SyntaxKind.SlashToken, _positon++, "/", null);
            case '(':
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _positon++, "(", null);
            case ')':
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _positon++, ")", null);
        }
        
        _diagnostics.Add($"ERROR: bad character input: '{Current}'");
        return new SyntaxToken(SyntaxKind.BadToken, _positon++, text.Substring(_positon - 1, 1), null);
    }
}