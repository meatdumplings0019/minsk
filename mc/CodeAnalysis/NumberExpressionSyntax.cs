// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis;

sealed class NumberExpressionSyntax(SyntaxToken numberToken) : ExpressionSyntax
{
    public SyntaxToken NumberToken { get; } = numberToken;
    public override SyntaxKind Kind => SyntaxKind.NumberExpression;
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return NumberToken;
    }
}