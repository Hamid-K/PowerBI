using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001052 RID: 4178
	public struct LeafFilter2 : IProgramNodeBuilder, IEquatable<LeafFilter2>
	{
		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06007BF3 RID: 31731 RVA: 0x001A4086 File Offset: 0x001A2286
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x001A408E File Offset: 0x001A228E
		private LeafFilter2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x001A4097 File Offset: 0x001A2297
		public static LeafFilter2 CreateUnsafe(ProgramNode node)
		{
			return new LeafFilter2(node);
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x001A40A0 File Offset: 0x001A22A0
		public static LeafFilter2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafFilter2)
			{
				return null;
			}
			return new LeafFilter2?(LeafFilter2.CreateUnsafe(node));
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x001A40D5 File Offset: 0x001A22D5
		public LeafFilter2(GrammarBuilders g, leafFExpr value0, selection4 value1)
		{
			this._node = g.Rule.LeafFilter2.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x001A4107 File Offset: 0x001A2307
		public static implicit operator filterSelection2(LeafFilter2 arg)
		{
			return filterSelection2.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06007BF9 RID: 31737 RVA: 0x001A4115 File Offset: 0x001A2315
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06007BFA RID: 31738 RVA: 0x001A4130 File Offset: 0x001A2330
		public selection4 selection4
		{
			get
			{
				return selection4.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x001A4144 File Offset: 0x001A2344
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x001A4158 File Offset: 0x001A2358
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x001A4182 File Offset: 0x001A2382
		public bool Equals(LeafFilter2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400336B RID: 13163
		private ProgramNode _node;
	}
}
