using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AB2 RID: 6834
	public struct SplitColumn : IProgramNodeBuilder, IEquatable<SplitColumn>
	{
		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x0600E1CA RID: 57802 RVA: 0x0030050A File Offset: 0x002FE70A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1CB RID: 57803 RVA: 0x00300512 File Offset: 0x002FE712
		private SplitColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1CC RID: 57804 RVA: 0x0030051B File Offset: 0x002FE71B
		public static SplitColumn CreateUnsafe(ProgramNode node)
		{
			return new SplitColumn(node);
		}

		// Token: 0x0600E1CD RID: 57805 RVA: 0x00300524 File Offset: 0x002FE724
		public static SplitColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitColumn)
			{
				return null;
			}
			return new SplitColumn?(SplitColumn.CreateUnsafe(node));
		}

		// Token: 0x0600E1CE RID: 57806 RVA: 0x00300559 File Offset: 0x002FE759
		public SplitColumn(GrammarBuilders g, splitCell value0, columnToSplit value1)
		{
			this._node = g.Rule.SplitColumn.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600E1CF RID: 57807 RVA: 0x0030058B File Offset: 0x002FE78B
		public static implicit operator newColumns(SplitColumn arg)
		{
			return newColumns.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x0600E1D0 RID: 57808 RVA: 0x00300599 File Offset: 0x002FE799
		public splitCell splitCell
		{
			get
			{
				return splitCell.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x0600E1D1 RID: 57809 RVA: 0x003005B4 File Offset: 0x002FE7B4
		public columnToSplit columnToSplit
		{
			get
			{
				return columnToSplit.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600E1D2 RID: 57810 RVA: 0x003005C8 File Offset: 0x002FE7C8
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1D3 RID: 57811 RVA: 0x003005DC File Offset: 0x002FE7DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1D4 RID: 57812 RVA: 0x00300606 File Offset: 0x002FE806
		public bool Equals(SplitColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005571 RID: 21873
		private ProgramNode _node;
	}
}
