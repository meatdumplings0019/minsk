// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis;

class SyntaxToken(SyntaxKind kind, int positon, string? text, object? value) : SyntaxNode
{
    public override SyntaxKind Kind { get; } = kind;
    public int Positon { get; } = positon;
    public string? Text { get; } = text;
    public object? Value { get; } = value;
    
    public override IEnumerable<SyntaxNode> GetChildren() => [];
}