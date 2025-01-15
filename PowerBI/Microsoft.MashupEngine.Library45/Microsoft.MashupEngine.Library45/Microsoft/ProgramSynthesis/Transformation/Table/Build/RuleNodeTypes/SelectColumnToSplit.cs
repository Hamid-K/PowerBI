using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AB1 RID: 6833
	public struct SelectColumnToSplit : IProgramNodeBuilder, IEquatable<SelectColumnToSplit>
	{
		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x0600E1BF RID: 57791 RVA: 0x0030040E File Offset: 0x002FE60E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1C0 RID: 57792 RVA: 0x00300416 File Offset: 0x002FE616
		private SelectColumnToSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1C1 RID: 57793 RVA: 0x0030041F File Offset: 0x002FE61F
		public static SelectColumnToSplit CreateUnsafe(ProgramNode node)
		{
			return new SelectColumnToSplit(node);
		}

		// Token: 0x0600E1C2 RID: 57794 RVA: 0x00300428 File Offset: 0x002FE628
		public static SelectColumnToSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectColumnToSplit)
			{
				return null;
			}
			return new SelectColumnToSplit?(SelectColumnToSplit.CreateUnsafe(node));
		}

		// Token: 0x0600E1C3 RID: 57795 RVA: 0x0030045D File Offset: 0x002FE65D
		public SelectColumnToSplit(GrammarBuilders g, inputTable value0, sourceColumnName value1)
		{
			this._node = g.Rule.SelectColumnToSplit.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600E1C4 RID: 57796 RVA: 0x00300483 File Offset: 0x002FE683
		public static implicit operator columnToSplit(SelectColumnToSplit arg)
		{
			return columnToSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x0600E1C5 RID: 57797 RVA: 0x00300491 File Offset: 0x002FE691
		public inputTable inputTable
		{
			get
			{
				return inputTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x0600E1C6 RID: 57798 RVA: 0x003004A5 File Offset: 0x002FE6A5
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600E1C7 RID: 57799 RVA: 0x003004B9 File Offset: 0x002FE6B9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1C8 RID: 57800 RVA: 0x003004CC File Offset: 0x002FE6CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1C9 RID: 57801 RVA: 0x003004F6 File Offset: 0x002FE6F6
		public bool Equals(SelectColumnToSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005570 RID: 21872
		private ProgramNode _node;
	}
}
