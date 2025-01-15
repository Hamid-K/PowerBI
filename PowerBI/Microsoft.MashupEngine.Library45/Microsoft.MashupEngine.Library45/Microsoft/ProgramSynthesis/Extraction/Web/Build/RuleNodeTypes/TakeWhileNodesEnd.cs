using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001051 RID: 4177
	public struct TakeWhileNodesEnd : IProgramNodeBuilder, IEquatable<TakeWhileNodesEnd>
	{
		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06007BE8 RID: 31720 RVA: 0x001A3F76 File Offset: 0x001A2176
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x001A3F7E File Offset: 0x001A217E
		private TakeWhileNodesEnd(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x001A3F87 File Offset: 0x001A2187
		public static TakeWhileNodesEnd CreateUnsafe(ProgramNode node)
		{
			return new TakeWhileNodesEnd(node);
		}

		// Token: 0x06007BEB RID: 31723 RVA: 0x001A3F90 File Offset: 0x001A2190
		public static TakeWhileNodesEnd? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TakeWhileNodesEnd)
			{
				return null;
			}
			return new TakeWhileNodesEnd?(TakeWhileNodesEnd.CreateUnsafe(node));
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x001A3FC5 File Offset: 0x001A21C5
		public TakeWhileNodesEnd(GrammarBuilders g, leafFExpr value0, regionStartSiblings value1)
		{
			this._node = g.Rule.TakeWhileNodesEnd.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x001A3FF7 File Offset: 0x001A21F7
		public static implicit operator selectionEnd(TakeWhileNodesEnd arg)
		{
			return selectionEnd.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06007BEE RID: 31726 RVA: 0x001A4005 File Offset: 0x001A2205
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06007BEF RID: 31727 RVA: 0x001A4020 File Offset: 0x001A2220
		public regionStartSiblings regionStartSiblings
		{
			get
			{
				return regionStartSiblings.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BF0 RID: 31728 RVA: 0x001A4034 File Offset: 0x001A2234
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BF1 RID: 31729 RVA: 0x001A4048 File Offset: 0x001A2248
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BF2 RID: 31730 RVA: 0x001A4072 File Offset: 0x001A2272
		public bool Equals(TakeWhileNodesEnd other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400336A RID: 13162
		private ProgramNode _node;
	}
}
