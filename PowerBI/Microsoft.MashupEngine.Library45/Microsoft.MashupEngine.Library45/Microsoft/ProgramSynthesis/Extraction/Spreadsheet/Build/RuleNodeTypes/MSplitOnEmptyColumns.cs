using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E3D RID: 3645
	public struct MSplitOnEmptyColumns : IProgramNodeBuilder, IEquatable<MSplitOnEmptyColumns>
	{
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060061AA RID: 25002 RVA: 0x0013F736 File Offset: 0x0013D936
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x0013F73E File Offset: 0x0013D93E
		private MSplitOnEmptyColumns(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x0013F747 File Offset: 0x0013D947
		public static MSplitOnEmptyColumns CreateUnsafe(ProgramNode node)
		{
			return new MSplitOnEmptyColumns(node);
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x0013F750 File Offset: 0x0013D950
		public static MSplitOnEmptyColumns? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MSplitOnEmptyColumns)
			{
				return null;
			}
			return new MSplitOnEmptyColumns?(MSplitOnEmptyColumns.CreateUnsafe(node));
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x0013F785 File Offset: 0x0013D985
		public MSplitOnEmptyColumns(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MSplitOnEmptyColumns.BuildASTNode(value0.Node);
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x0013F7A4 File Offset: 0x0013D9A4
		public static implicit operator mSection(MSplitOnEmptyColumns arg)
		{
			return mSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060061B0 RID: 25008 RVA: 0x0013F7B2 File Offset: 0x0013D9B2
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x0013F7C6 File Offset: 0x0013D9C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x0013F7DC File Offset: 0x0013D9DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x0013F806 File Offset: 0x0013DA06
		public bool Equals(MSplitOnEmptyColumns other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE7 RID: 11239
		private ProgramNode _node;
	}
}
