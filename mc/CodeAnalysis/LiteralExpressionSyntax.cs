// ReSharper disable once CheckNamespace
namespace Minsk.CodeAnalysis;

public sealed class LiteralExpressionSyntax(SyntaxToken literalToken) : ExpressionSyntax
{
    public SyntaxToken LiteralToken { get; } = literalToken;
    public override SyntaxKind Kind => SyntaxKind.NumberExpression;
    
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}