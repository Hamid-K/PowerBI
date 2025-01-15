using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001053 RID: 4179
	public struct LeafFilter3 : IProgramNodeBuilder, IEquatable<LeafFilter3>
	{
		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06007BFE RID: 31742 RVA: 0x001A4196 File Offset: 0x001A2396
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BFF RID: 31743 RVA: 0x001A419E File Offset: 0x001A239E
		private LeafFilter3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x001A41A7 File Offset: 0x001A23A7
		public static LeafFilter3 CreateUnsafe(ProgramNode node)
		{
			return new LeafFilter3(node);
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x001A41B0 File Offset: 0x001A23B0
		public static LeafFilter3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafFilter3)
			{
				return null;
			}
			return new LeafFilter3?(LeafFilter3.CreateUnsafe(node));
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x001A41E5 File Offset: 0x001A23E5
		public LeafFilter3(GrammarBuilders g, leafFExpr value0, selection6 value1)
		{
			this._node = g.Rule.LeafFilter3.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007C03 RID: 31747 RVA: 0x001A4217 File Offset: 0x001A2417
		public static implicit operator filterSelection3(LeafFilter3 arg)
		{
			return filterSelection3.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06007C04 RID: 31748 RVA: 0x001A4225 File Offset: 0x001A2425
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06007C05 RID: 31749 RVA: 0x001A4240 File Offset: 0x001A2440
		public selection6 selection6
		{
			get
			{
				return selection6.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x001A4254 File Offset: 0x001A2454
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x001A4268 File Offset: 0x001A2468
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x001A4292 File Offset: 0x001A2492
		public bool Equals(LeafFilter3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400336C RID: 13164
		private ProgramNode _node;
	}
}
