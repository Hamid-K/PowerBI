using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E3 RID: 4579
	public struct IsNull : IProgramNodeBuilder, IEquatable<IsNull>
	{
		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x0600898B RID: 35211 RVA: 0x001CF4DE File Offset: 0x001CD6DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600898C RID: 35212 RVA: 0x001CF4E6 File Offset: 0x001CD6E6
		private IsNull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600898D RID: 35213 RVA: 0x001CF4EF File Offset: 0x001CD6EF
		public static IsNull CreateUnsafe(ProgramNode node)
		{
			return new IsNull(node);
		}

		// Token: 0x0600898E RID: 35214 RVA: 0x001CF4F8 File Offset: 0x001CD6F8
		public static IsNull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsNull)
			{
				return null;
			}
			return new IsNull?(IsNull.CreateUnsafe(node));
		}

		// Token: 0x0600898F RID: 35215 RVA: 0x001CF52D File Offset: 0x001CD72D
		public IsNull(GrammarBuilders g, sRegion value0)
		{
			this._node = g.Rule.IsNull.BuildASTNode(value0.Node);
		}

		// Token: 0x06008990 RID: 35216 RVA: 0x001CF54C File Offset: 0x001CD74C
		public static implicit operator match(IsNull arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06008991 RID: 35217 RVA: 0x001CF55A File Offset: 0x001CD75A
		public sRegion sRegion
		{
			get
			{
				return sRegion.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06008992 RID: 35218 RVA: 0x001CF56E File Offset: 0x001CD76E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008993 RID: 35219 RVA: 0x001CF584 File Offset: 0x001CD784
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008994 RID: 35220 RVA: 0x001CF5AE File Offset: 0x001CD7AE
		public bool Equals(IsNull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003897 RID: 14487
		private ProgramNode _node;
	}
}
