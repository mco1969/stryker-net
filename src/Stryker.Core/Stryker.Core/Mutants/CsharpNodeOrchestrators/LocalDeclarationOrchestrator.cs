using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Stryker.Abstractions.Mutants.CsharpNodeOrchestrators;

/// <summary>
/// Handle const declarations.
/// </summary>
internal class LocalDeclarationOrchestrator : StatementSpecificOrchestrator<LocalDeclarationStatementSyntax>
{
    // we don't inject mutations here, we want them promoted at block level
    protected override StatementSyntax InjectMutations(LocalDeclarationStatementSyntax sourceNode,
        StatementSyntax targetNode,
        SemanticModel semanticModel,
        MutationContext context) =>
        targetNode;
}
