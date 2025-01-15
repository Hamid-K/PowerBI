using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001050 RID: 4176
	public struct FilterNodesEnd : IProgramNodeBuilder, IEquatable<FilterNodesEnd>
	{
		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06007BDD RID: 31709 RVA: 0x001A3E66 File Offset: 0x001A2066
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x001A3E6E File Offset: 0x001A206E
		private FilterNodesEnd(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x001A3E77 File Offset: 0x001A2077
		public static FilterNodesEnd CreateUnsafe(ProgramNode node)
		{
			return new FilterNodesEnd(node);
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x001A3E80 File Offset: 0x001A2080
		public static FilterNodesEnd? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FilterNodesEnd)
			{
				return null;
			}
			return new FilterNodesEnd?(FilterNodesEnd.CreateUnsafe(node));
		}

		// Token: 0x06007BE1 RID: 31713 RVA: 0x001A3EB5 File Offset: 0x001A20B5
		public FilterNodesEnd(GrammarBuilders g, leafFExpr value0, regionStartSiblings value1)
		{
			this._node = g.Rule.FilterNodesEnd.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BE2 RID: 31714 RVA: 0x001A3EE7 File Offset: 0x001A20E7
		public static implicit operator selectionEnd(FilterNodesEnd arg)
		{
			return selectionEnd.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06007BE3 RID: 31715 RVA: 0x001A3EF5 File Offset: 0x001A20F5
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06007BE4 RID: 31716 RVA: 0x001A3F10 File Offset: 0x001A2110
		public regionStartSiblings regionStartSiblings
		{
			get
			{
				return regionStartSiblings.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BE5 RID: 31717 RVA: 0x001A3F24 File Offset: 0x001A2124
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BE6 RID: 31718 RVA: 0x001A3F38 File Offset: 0x001A2138
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BE7 RID: 31719 RVA: 0x001A3F62 File Offset: 0x001A2162
		public bool Equals(FilterNodesEnd other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003369 RID: 13161
		private ProgramNode _node;
	}
}
