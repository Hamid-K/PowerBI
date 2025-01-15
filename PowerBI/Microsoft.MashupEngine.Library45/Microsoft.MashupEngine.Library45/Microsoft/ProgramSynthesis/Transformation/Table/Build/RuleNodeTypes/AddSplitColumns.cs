using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AAF RID: 6831
	public struct AddSplitColumns : IProgramNodeBuilder, IEquatable<AddSplitColumns>
	{
		// Token: 0x170025C2 RID: 9666
		// (get) Token: 0x0600E1A7 RID: 57767 RVA: 0x003001DE File Offset: 0x002FE3DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1A8 RID: 57768 RVA: 0x003001E6 File Offset: 0x002FE3E6
		private AddSplitColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1A9 RID: 57769 RVA: 0x003001EF File Offset: 0x002FE3EF
		public static AddSplitColumns CreateUnsafe(ProgramNode node)
		{
			return new AddSplitColumns(node);
		}

		// Token: 0x0600E1AA RID: 57770 RVA: 0x003001F8 File Offset: 0x002FE3F8
		public static AddSplitColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AddSplitColumns)
			{
				return null;
			}
			return new AddSplitColumns?(AddSplitColumns.CreateUnsafe(node));
		}

		// Token: 0x0600E1AB RID: 57771 RVA: 0x0030022D File Offset: 0x002FE42D
		public AddSplitColumns(GrammarBuilders g, table value0, sourceColumnName value1, newColumns value2)
		{
			this._node = g.Rule.AddSplitColumns.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600E1AC RID: 57772 RVA: 0x0030025A File Offset: 0x002FE45A
		public static implicit operator table(AddSplitColumns arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025C3 RID: 9667
		// (get) Token: 0x0600E1AD RID: 57773 RVA: 0x00300268 File Offset: 0x002FE468
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x0600E1AE RID: 57774 RVA: 0x0030027C File Offset: 0x002FE47C
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x0600E1AF RID: 57775 RVA: 0x00300290 File Offset: 0x002FE490
		public newColumns newColumns
		{
			get
			{
				return newColumns.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600E1B0 RID: 57776 RVA: 0x003002A4 File Offset: 0x002FE4A4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1B1 RID: 57777 RVA: 0x003002B8 File Offset: 0x002FE4B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1B2 RID: 57778 RVA: 0x003002E2 File Offset: 0x002FE4E2
		public bool Equals(AddSplitColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400556E RID: 21870
		private ProgramNode _node;
	}
}
