using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x02001210 RID: 4624
	public class MatchingLabelCollector : ProgramNodeVisitor<MatchingLabel>
	{
		// Token: 0x06008B56 RID: 35670 RVA: 0x001D2D8D File Offset: 0x001D0F8D
		public MatchingLabelCollector(GrammarBuilders build)
		{
			this._build = build;
		}

		// Token: 0x06008B57 RID: 35671 RVA: 0x001D2D9C File Offset: 0x001D0F9C
		public override MatchingLabel VisitNonterminal(NonterminalNode node)
		{
			if (this._build.Node.IsRule.IsNull(node))
			{
				return MatchingLabel.NullMatch;
			}
			if (this._build.Node.IsRule.EndOf(node))
			{
				return MatchingLabel.Tokens(ImmutableList<IToken>.Empty);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06008B58 RID: 35672 RVA: 0x001D2DEF File Offset: 0x001D0FEF
		public override MatchingLabel VisitLet(LetNode node)
		{
			if (this._build.Node.IsRule.LetSplit(node))
			{
				return MatchingLabel.Tokens(node.AcceptVisitor<IEnumerable<IToken>>(new TokensCollector(this._build)).ToImmutableList<IToken>());
			}
			throw new NotImplementedException();
		}

		// Token: 0x06008B59 RID: 35673 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override MatchingLabel VisitLambda(LambdaNode node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B5A RID: 35674 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override MatchingLabel VisitLiteral(LiteralNode node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override MatchingLabel VisitVariable(VariableNode node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06008B5C RID: 35676 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public override MatchingLabel VisitHole(Hole node)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040038D8 RID: 14552
		private readonly GrammarBuilders _build;
	}
}
