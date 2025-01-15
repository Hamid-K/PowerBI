using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008DF RID: 2271
	public abstract class ProgramNodeVisitor<TResult, TArgs>
	{
		// Token: 0x060030FF RID: 12543
		public abstract TResult VisitNonterminal(NonterminalNode node, TArgs args);

		// Token: 0x06003100 RID: 12544
		public abstract TResult VisitLet(LetNode node, TArgs args);

		// Token: 0x06003101 RID: 12545
		public abstract TResult VisitLambda(LambdaNode node, TArgs args);

		// Token: 0x06003102 RID: 12546
		public abstract TResult VisitLiteral(LiteralNode node, TArgs args);

		// Token: 0x06003103 RID: 12547
		public abstract TResult VisitVariable(VariableNode node, TArgs args);

		// Token: 0x06003104 RID: 12548
		public abstract TResult VisitHole(Hole node, TArgs args);
	}
}
