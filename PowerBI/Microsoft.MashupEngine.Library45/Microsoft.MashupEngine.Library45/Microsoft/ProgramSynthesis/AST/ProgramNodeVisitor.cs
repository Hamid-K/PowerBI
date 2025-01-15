using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008DE RID: 2270
	public abstract class ProgramNodeVisitor<T>
	{
		// Token: 0x060030F8 RID: 12536
		public abstract T VisitNonterminal(NonterminalNode node);

		// Token: 0x060030F9 RID: 12537
		public abstract T VisitLet(LetNode node);

		// Token: 0x060030FA RID: 12538
		public abstract T VisitLambda(LambdaNode node);

		// Token: 0x060030FB RID: 12539
		public abstract T VisitLiteral(LiteralNode node);

		// Token: 0x060030FC RID: 12540
		public abstract T VisitVariable(VariableNode node);

		// Token: 0x060030FD RID: 12541
		public abstract T VisitHole(Hole node);
	}
}
