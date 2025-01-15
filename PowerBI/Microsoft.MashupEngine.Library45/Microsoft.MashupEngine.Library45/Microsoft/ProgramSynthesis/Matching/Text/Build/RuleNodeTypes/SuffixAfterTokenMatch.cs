using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E2 RID: 4578
	public struct SuffixAfterTokenMatch : IProgramNodeBuilder, IEquatable<SuffixAfterTokenMatch>
	{
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x06008980 RID: 35200 RVA: 0x001CF3E2 File Offset: 0x001CD5E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008981 RID: 35201 RVA: 0x001CF3EA File Offset: 0x001CD5EA
		private SuffixAfterTokenMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x001CF3F3 File Offset: 0x001CD5F3
		public static SuffixAfterTokenMatch CreateUnsafe(ProgramNode node)
		{
			return new SuffixAfterTokenMatch(node);
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x001CF3FC File Offset: 0x001CD5FC
		public static SuffixAfterTokenMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SuffixAfterTokenMatch)
			{
				return null;
			}
			return new SuffixAfterTokenMatch?(SuffixAfterTokenMatch.CreateUnsafe(node));
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x001CF431 File Offset: 0x001CD631
		public SuffixAfterTokenMatch(GrammarBuilders g, sRegion value0, token value1)
		{
			this._node = g.Rule.SuffixAfterTokenMatch.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06008985 RID: 35205 RVA: 0x001CF457 File Offset: 0x001CD657
		public static implicit operator _LetB0(SuffixAfterTokenMatch arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x06008986 RID: 35206 RVA: 0x001CF465 File Offset: 0x001CD665
		public sRegion sRegion
		{
			get
			{
				return sRegion.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06008987 RID: 35207 RVA: 0x001CF479 File Offset: 0x001CD679
		public token token
		{
			get
			{
				return token.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06008988 RID: 35208 RVA: 0x001CF48D File Offset: 0x001CD68D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008989 RID: 35209 RVA: 0x001CF4A0 File Offset: 0x001CD6A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600898A RID: 35210 RVA: 0x001CF4CA File Offset: 0x001CD6CA
		public bool Equals(SuffixAfterTokenMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003896 RID: 14486
		private ProgramNode _node;
	}
}
