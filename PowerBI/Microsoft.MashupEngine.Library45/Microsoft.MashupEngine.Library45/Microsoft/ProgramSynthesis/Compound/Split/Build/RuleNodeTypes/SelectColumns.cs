using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200093C RID: 2364
	public struct SelectColumns : IProgramNodeBuilder, IEquatable<SelectColumns>
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000ACF0E File Offset: 0x000AB10E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000ACF16 File Offset: 0x000AB116
		private SelectColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000ACF1F File Offset: 0x000AB11F
		public static SelectColumns CreateUnsafe(ProgramNode node)
		{
			return new SelectColumns(node);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000ACF28 File Offset: 0x000AB128
		public static SelectColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectColumns)
			{
				return null;
			}
			return new SelectColumns?(SelectColumns.CreateUnsafe(node));
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000ACF5D File Offset: 0x000AB15D
		public SelectColumns(GrammarBuilders g, columnList value0, splitRecords value1)
		{
			this._node = g.Rule.SelectColumns.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000ACF83 File Offset: 0x000AB183
		public static implicit operator splitRecordsSelect(SelectColumns arg)
		{
			return splitRecordsSelect.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000ACF91 File Offset: 0x000AB191
		public columnList columnList
		{
			get
			{
				return columnList.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000ACFA5 File Offset: 0x000AB1A5
		public splitRecords splitRecords
		{
			get
			{
				return splitRecords.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000ACFB9 File Offset: 0x000AB1B9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000ACFCC File Offset: 0x000AB1CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000ACFF6 File Offset: 0x000AB1F6
		public bool Equals(SelectColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A5C RID: 6748
		private ProgramNode _node;
	}
}
