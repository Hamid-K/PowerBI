using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Visitors
{
	// Token: 0x02001D3B RID: 7483
	internal class BranchCount : ProgramNodeVisitor<int>
	{
		// Token: 0x0600FC5F RID: 64607 RVA: 0x0035DF4B File Offset: 0x0035C14B
		private BranchCount(Grammar grammar)
		{
			this._builder = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x0600FC60 RID: 64608 RVA: 0x0035DF5F File Offset: 0x0035C15F
		public static BranchCount Instance(Grammar grammar)
		{
			return new BranchCount(grammar);
		}

		// Token: 0x0600FC61 RID: 64609 RVA: 0x0035DF68 File Offset: 0x0035C168
		public override int VisitNonterminal(NonterminalNode node)
		{
			IfThenElse ifThenElse;
			if (this._builder.Node.IsRule.IfThenElse(node, out ifThenElse))
			{
				return 1 + ifThenElse.@switch.Node.AcceptVisitor<int>(this);
			}
			SingleBranch singleBranch;
			if (this._builder.Node.IsRule.SingleBranch(node, out singleBranch))
			{
				return 1;
			}
			switch_ite switch_ite;
			if (this._builder.Node.IsRule.switch_ite(node, out switch_ite))
			{
				return switch_ite.ite.Node.AcceptVisitor<int>(this);
			}
			return 0;
		}

		// Token: 0x0600FC62 RID: 64610 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int VisitLet(LetNode node)
		{
			return 0;
		}

		// Token: 0x0600FC63 RID: 64611 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int VisitLambda(LambdaNode node)
		{
			return 0;
		}

		// Token: 0x0600FC64 RID: 64612 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int VisitLiteral(LiteralNode node)
		{
			return 0;
		}

		// Token: 0x0600FC65 RID: 64613 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int VisitVariable(VariableNode node)
		{
			return 0;
		}

		// Token: 0x0600FC66 RID: 64614 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override int VisitHole(Hole node)
		{
			return 0;
		}

		// Token: 0x04005E47 RID: 24135
		private readonly GrammarBuilders _builder;
	}
}
