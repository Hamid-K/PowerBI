using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x0200120F RID: 4623
	internal class TokensCollector : ProgramNodeVisitor<IEnumerable<IToken>>
	{
		// Token: 0x06008B4D RID: 35661 RVA: 0x001D2CBF File Offset: 0x001D0EBF
		public TokensCollector(GrammarBuilders build)
		{
			this._build = build;
		}

		// Token: 0x06008B4E RID: 35662 RVA: 0x001D2CD0 File Offset: 0x001D0ED0
		public override IEnumerable<IToken> VisitNonterminal(NonterminalNode node)
		{
			if (this._build.Node.IsRule.EndOf(node))
			{
				return Enumerable.Empty<IToken>();
			}
			SuffixAfterTokenMatch? suffixAfterTokenMatch = this._build.Node.AsRule.SuffixAfterTokenMatch(node);
			if (suffixAfterTokenMatch != null)
			{
				return suffixAfterTokenMatch.Value.token.Value.Yield<IToken>();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x001D2D3D File Offset: 0x001D0F3D
		public override IEnumerable<IToken> VisitLet(LetNode node)
		{
			if (this._build.Node.IsRule.LetSplit(node))
			{
				return this.VisitChildrenOf(node);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06008B50 RID: 35664 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override IEnumerable<IToken> VisitLambda(LambdaNode node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B51 RID: 35665 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override IEnumerable<IToken> VisitLiteral(LiteralNode node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override IEnumerable<IToken> VisitVariable(VariableNode node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override IEnumerable<IToken> VisitHole(Hole node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B54 RID: 35668 RVA: 0x001D2D6B File Offset: 0x001D0F6B
		private IEnumerable<IToken> VisitChildrenOf(ProgramNode node)
		{
			return node.Children.SelectMany((ProgramNode n) => n.AcceptVisitor<IEnumerable<IToken>>(this));
		}

		// Token: 0x040038D7 RID: 14551
		private readonly GrammarBuilders _build;
	}
}
