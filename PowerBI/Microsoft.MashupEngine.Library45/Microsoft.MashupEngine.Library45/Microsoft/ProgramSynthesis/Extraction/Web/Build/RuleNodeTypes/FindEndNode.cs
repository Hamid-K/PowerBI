using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200104C RID: 4172
	public struct FindEndNode : IProgramNodeBuilder, IEquatable<FindEndNode>
	{
		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06007BB1 RID: 31665 RVA: 0x001A3A36 File Offset: 0x001A1C36
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BB2 RID: 31666 RVA: 0x001A3A3E File Offset: 0x001A1C3E
		private FindEndNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BB3 RID: 31667 RVA: 0x001A3A47 File Offset: 0x001A1C47
		public static FindEndNode CreateUnsafe(ProgramNode node)
		{
			return new FindEndNode(node);
		}

		// Token: 0x06007BB4 RID: 31668 RVA: 0x001A3A50 File Offset: 0x001A1C50
		public static FindEndNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FindEndNode)
			{
				return null;
			}
			return new FindEndNode?(FindEndNode.CreateUnsafe(node));
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x001A3A85 File Offset: 0x001A1C85
		public FindEndNode(GrammarBuilders g, mapRegionInSequence value0, selection value1)
		{
			this._node = g.Rule.FindEndNode.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x001A3AB7 File Offset: 0x001A1CB7
		public static implicit operator regionSequence(FindEndNode arg)
		{
			return regionSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06007BB7 RID: 31671 RVA: 0x001A3AC5 File Offset: 0x001A1CC5
		public mapRegionInSequence mapRegionInSequence
		{
			get
			{
				return mapRegionInSequence.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06007BB8 RID: 31672 RVA: 0x001A3AE0 File Offset: 0x001A1CE0
		public selection selection
		{
			get
			{
				return selection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x001A3AF4 File Offset: 0x001A1CF4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x001A3B08 File Offset: 0x001A1D08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x001A3B32 File Offset: 0x001A1D32
		public bool Equals(FindEndNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003365 RID: 13157
		private ProgramNode _node;
	}
}
