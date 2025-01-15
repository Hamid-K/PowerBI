using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001054 RID: 4180
	public struct LeafFilter4 : IProgramNodeBuilder, IEquatable<LeafFilter4>
	{
		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06007C09 RID: 31753 RVA: 0x001A42A6 File Offset: 0x001A24A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x001A42AE File Offset: 0x001A24AE
		private LeafFilter4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x001A42B7 File Offset: 0x001A24B7
		public static LeafFilter4 CreateUnsafe(ProgramNode node)
		{
			return new LeafFilter4(node);
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x001A42C0 File Offset: 0x001A24C0
		public static LeafFilter4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafFilter4)
			{
				return null;
			}
			return new LeafFilter4?(LeafFilter4.CreateUnsafe(node));
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x001A42F5 File Offset: 0x001A24F5
		public LeafFilter4(GrammarBuilders g, leafFExpr value0, selection8 value1)
		{
			this._node = g.Rule.LeafFilter4.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x001A4327 File Offset: 0x001A2527
		public static implicit operator filterSelection4(LeafFilter4 arg)
		{
			return filterSelection4.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06007C0F RID: 31759 RVA: 0x001A4335 File Offset: 0x001A2535
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06007C10 RID: 31760 RVA: 0x001A4350 File Offset: 0x001A2550
		public selection8 selection8
		{
			get
			{
				return selection8.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x001A4364 File Offset: 0x001A2564
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x001A4378 File Offset: 0x001A2578
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x001A43A2 File Offset: 0x001A25A2
		public bool Equals(LeafFilter4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400336D RID: 13165
		private ProgramNode _node;
	}
}
